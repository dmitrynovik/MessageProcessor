using System.IO;
using MessageProcessor.Lib.Interfaces;

namespace MessageProcessor.Lib.Serializers
{
    public class XmlSerializer : ISerializer
    {
        public Stream Serialize(IMessage message)
        {
            var stream = new MemoryStream();
            if (message != null)
            {
                var payload = message.Formatted();
                var serializer = new System.Xml.Serialization.XmlSerializer(payload.GetType());
                serializer.Serialize(stream, payload);
            }
            return stream;
        }
    }
}
