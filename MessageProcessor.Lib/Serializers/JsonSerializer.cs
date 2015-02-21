using System.IO;
using System.Text;
using MessageProcessor.Lib.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MessageProcessor.Lib.Serializers
{
    public class JsonSerializer : ISerializer
    {
        public Stream Serialize(IMessage message)
        {
            var fmt = message == null ? null : message.Formatted();
            var json = JsonConvert.SerializeObject(fmt, Formatting.Indented, new StringEnumConverter());
            return new MemoryStream(Encoding.UTF8.GetBytes(json));
        }
    }
}
