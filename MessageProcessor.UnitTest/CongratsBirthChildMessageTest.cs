﻿using System;
using System.IO;
using MessageProcessor.Lib.Interfaces;
using MessageProcessor.Lib.Messages;
using NUnit.Framework;

namespace MessageProcessor.UnitTest
{
    [TestFixture]
    public class CongratsBirthChildMessageTest : TestBase
    {
        [Test]
        public void Message_Is_Correctly_Serialized()
        {
            var text = CreateAndSerializeMessage();
            Assert.IsTrue(text.Contains("<Text>Surprise!</Text>"));
            Assert.IsTrue(text.Contains("<Name>SgBvAGgAbgAgAFMAbQBpAHQAaAA=</Name>"));
            Assert.IsTrue(text.Contains("<Gender>Male</Gender>"));
            Assert.IsTrue(text.Contains("<BabyBirthDay>05 Dec 2014</BabyBirthDay>"));
        }

        [Test]
        public void When_Message_Is_Processed_File_Is_Written()
        {
            var message = ProcessMessage();
            AssertMessageWrittenToFile(message, "BabyBirth");
        }

        protected override IMessage CreateMessage()
        {
            var message = new CongratsBirthChildMessage()
            {
                Name = "John Smith",
                MessageId = Guid.Parse("61B8708C-1EA7-4BC3-83BF-16824FA025E4"),
                Text = "Surprise!",
                BabyBirthDay = new DateTime(2014, 12, 5),
                Gender = Gender.Male,
            };
            return message;
        }
    }
}
