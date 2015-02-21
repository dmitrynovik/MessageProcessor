using System;
using System.Xml.Serialization;
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
        [XmlIgnore]
        public abstract SerializerType DefaultSerializerType { get; }
        [JsonIgnore]
        [XmlIgnore]
        public abstract MessageDestination Destination { get; }
        [JsonIgnore]
        [XmlIgnore]
        public abstract string Location { get; }

        /// <summary>Default implementation: the message has no formatting and serialized AS IS.</summary>
        public virtual object Formatted() { return this; }
   }
}
