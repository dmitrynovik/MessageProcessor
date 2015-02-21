using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MessageProcessor.Lib.Interfaces;
using MessageProcessor.Lib.Sinks;

namespace MessageProcessor.Lib
{
    public class MessageQueue
    {
        private readonly SerializerFactory _serializerFactory;
        private readonly Queue<IMessage> _messageQueue = new Queue<IMessage>();

        public MessageQueue(SerializerFactory serializerFactory)
        {
            if (serializerFactory == null)
                throw new ArgumentException("serializerFactory");

            _serializerFactory = serializerFactory;
        }

        public void Enqueue(IMessage message)
        {
            lock (_messageQueue)
            {
                _messageQueue.Enqueue(message);
            }
        }

        public MessageProcessResult ProcessSingle()
        {
            lock (_messageQueue)
            {
                if (_messageQueue.Any())
                {
                    var message = _messageQueue.Dequeue();
                    return Process(message);
                }
            }
            return new MessageProcessResult() { MessageStatus = MessageProcessResult.Status.EmptyQueue };
        }

        public MessageProcessResult[] ProcessAll()
        {
            Task<MessageProcessResult>[] parallelJobs;
            // 
            // Asynchronously process all messages in the queue:
            // 
            lock (_messageQueue)
            {
                parallelJobs = _messageQueue
                    .Select(message => Task.Factory.StartNew(() => Process(message)))
                    .ToArray();

                _messageQueue.Clear();
            }

            Task.WaitAll(parallelJobs);
            return parallelJobs.Select(_ => _.Result).ToArray();
        }

        /// <summary>Serializes a message (generic implementation) </summary>
        private MessageProcessResult Process(IMessage message)
        {
            try
            {
                switch (message.Destination)
                {
                    case MessageDestination.File:
                        new FileSystemSink().WriteToFile(_serializerFactory, message);
                        return new MessageProcessResult() { Message = message, MessageStatus = MessageProcessResult.Status.Success };
                    default:
                        return new MessageProcessResult() { Message = message, MessageStatus = MessageProcessResult.Status.UnknownMessageType };
                }
            }
            catch (Exception ex)
            {
                return new MessageProcessResult() { Message = message, MessageStatus = MessageProcessResult.Status.Failure, Error = ex };
            }
        }
    }
}
