using System;

namespace MessageProcessor.Lib.Interfaces
{
    public interface IMessage
    {
        Guid MessageId { get; set; }
        MessageType MessageType { get; }
        string Name { get; set; }
        string Text { get; set; }

        SerializerType DefaultSerializerType { get; }
        MessageDestination Destination { get; }
        string Url { get; }

        /// <summary>Formats message when processing.</summary>
        /// <returns>Formatted message used in message processing.</returns>
        object Formatted();
    }
}
