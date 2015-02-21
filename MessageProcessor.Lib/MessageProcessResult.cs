using System;
using MessageProcessor.Lib.Interfaces;

namespace MessageProcessor.Lib
{
    public class MessageProcessResult
    {
        public enum Status
        {
            Success,
            EmptyQueue,
            UnknownMessageType,
            Failure,
        }

        public IMessage Message { get; set; }
        public Status MessageStatus { get; set; }
        public Exception Error { get; set; }
    }
}
