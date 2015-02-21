using System.IO;
using System.Linq;
using MessageProcessor.Lib;
using MessageProcessor.Lib.Interfaces;
using NUnit.Framework;

namespace MessageProcessor.UnitTest
{
    public abstract class TestBase
    {
        protected SerializerFactory SerializerFactory = new SerializerFactory();
        protected MessageQueue MessageQueue;

        protected TestBase()
        {
            MessageQueue = new MessageQueue(SerializerFactory);
        }

        protected static void AssertFileWritten(IMessage message, string path)
        {
            Assert.IsTrue(Directory.Exists(path));
            Assert.AreEqual(new DirectoryInfo(path).GetFiles().Count(f => f.Name == message.MessageId.ToString()), 1);
        }

    }
}
