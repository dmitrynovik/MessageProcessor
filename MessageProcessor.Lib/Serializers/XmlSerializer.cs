using System.IO;
using MessageProcessor.Lib.Interfaces;

namespace MessageProcessor.Lib.Serializers
{
    public class XmlSerializer : ISerializer
    {
        public Stream Serialize(IMessage message)
        {
            var stream = new MemoryStream();

            var serializer = new System.Xml.Serialization.XmlSerializer(message.GetType());
            serializer.Serialize(stream, message == null ? null : message.Formatted());
            return stream;
        }
    }
}
