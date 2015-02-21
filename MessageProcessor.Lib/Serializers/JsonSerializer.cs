using System.IO;
using System.Text;
using MessageProcessor.Lib.Interfaces;
using Newtonsoft.Json;

namespace MessageProcessor.Lib.Serializers
{
    public class JsonSerializer : ISerializer
    {
        public Stream Serialize(IMessage message)
        {
            var fmt = message == null ? null : message.Formatted();
            var json = JsonConvert.SerializeObject(fmt, Formatting.Indented);
            return new MemoryStream(Encoding.UTF8.GetBytes(json));
        }
    }
}
