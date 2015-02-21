using System;
using MessageProcessor.Lib.Interfaces;
using Newtonsoft.Json;

namespace MessageProcessor.Lib
{
    public abstract class MessageBase : IMessage
    {
        public Guid MessageId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public abstract MessageType MessageType { get; }

        [JsonIgnore]
        public abstract SerializerType DefaultSerializerType { get; }
        [JsonIgnore]
        public abstract MessageDestination Destination { get; }
        [JsonIgnore]
        public abstract string Url { get; }

        /// <summary>Default implementation: the message has no formatting and serialized AS IS. To be overriden in derived classes.</summary>
        public virtual object Formatted() { return this; }
   }
}
