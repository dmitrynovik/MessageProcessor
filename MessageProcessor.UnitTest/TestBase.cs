﻿using System;
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

        protected IMessage ProcessMessage()
        {
            var message = CreateMessage();
            MessageQueue.Enqueue(message);
            MessageQueue.ProcessAll();
            return message;
        }

        protected string CreateAndSerializeMessage()
        {
            var message = CreateMessage();
            using (var stream = SerializerFactory.Get(message.DefaultSerializerType()).Serialize(message))
            {
                using (var reader = new StreamReader(stream))
                {
                    var text = reader.ReadToEnd();
                    Console.WriteLine(text);
                    return text;
                }
            }
        }

        protected abstract IMessage CreateMessage();

        protected static void AssertMessageWrittenToFile(IMessage message, string path)
        {
            Assert.IsTrue(Directory.Exists(path));
            Assert.AreEqual(new DirectoryInfo(path).GetFiles().Count(f => f.Name == message.MessageId.ToString()), 1);
        }

    }
}
