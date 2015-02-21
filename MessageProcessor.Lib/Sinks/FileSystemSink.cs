using System.IO;
using MessageProcessor.Lib.Interfaces;

namespace MessageProcessor.Lib.Sinks
{
    public class FileSystemSink : ISink
    {
        public void Write(SerializerFactory factory, IMessage message)
        {
            var path = message.Url();
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var serializer = factory.Get(message.DefaultSerializerType());
            using (var fs = File.OpenWrite(Path.Combine(message.Url(), message.MessageId.ToString())))
            using (var stream = serializer.Serialize(message))
            {
                if (stream.Seek(0, SeekOrigin.Begin) != 0)
                    throw new IOException("Could not serialize message: serialization stream not seekable");

                stream.CopyTo(fs);
            }
        }
    }
}
