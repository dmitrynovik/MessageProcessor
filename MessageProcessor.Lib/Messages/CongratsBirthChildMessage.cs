using System;
using System.Text;

namespace MessageProcessor.Lib.Messages
{
    public enum Gender { Male, Female }

    public class CongratsBirthChildMessage : MessageBase
    {
        public Gender Gender { get; set; }
        public DateTime BabyBirthDay { get; set; }

        public override SerializerType DefaultSerializerType() { return SerializerType.Xml; }
        public override string Url() { return "BabyBirth"; }
        public override MessageDestination Destination() { return MessageDestination.Disk; }

        public override object Formatted()
        {
            return new
            {
                MessageId,
                MessageType,
                Name = Convert.ToBase64String(Encoding.Unicode.GetBytes(Name)),
                Text,
                Gender,
                BabyBirthDay = BabyBirthDay.ToString("dd MMM yyyy"),
            };
        }

        public override MessageType MessageType
        {
            get { return MessageType.CongratsChildBirth; }
        }
    }
}
