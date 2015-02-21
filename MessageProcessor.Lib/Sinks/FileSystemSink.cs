using System.IO;
using MessageProcessor.Lib.Interfaces;

namespace MessageProcessor.Lib.Sinks
{
    public class FileSystemSink
    {
        public void WriteToFile(SerializerFactory factory, IMessage message)
        {
            if (!Directory.Exists(message.Location))
                Directory.CreateDirectory(message.Location);

            var serializer = factory.Get(message.DefaultSerializerType);
            using (var fs = File.OpenWrite(Path.Combine(message.Location, message.MessageId.ToString())))
            using (var stream = serializer.Serialize(message))
            {
                if (stream.Seek(0, SeekOrigin.Begin) != 0)
                    throw new IOException("Could not serialize message: serialization stream not seekable");

                stream.CopyTo(fs);
            }
        }
    }
}
