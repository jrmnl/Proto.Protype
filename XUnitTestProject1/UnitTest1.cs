using System.IO;
using Google.Protobuf.WellKnownTypes;
using R1.Protobuf.Contract.Address.V1;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var addressBook = new AddressBook();
            var person = new Person
            {
                Email = "fgfg",
                Id = 123,
                LastUpdated = new Timestamp()
                {
                    Seconds = 100500
                },
                Name = "qwerty"
            };
            person.Phones.Add(new Person.Types.PhoneNumber
            {
                Number = "sdfdsfdsf",
                Type = Person.Types.PhoneType.Mobile
            });
            addressBook.People.Add(person);

            var serialized = Serialize(addressBook);

            var addressBookParsed = AddressBook.Parser.ParseFrom(serialized);

            Assert.True(addressBook.Equals(addressBookParsed));
        }

        private static byte[] Serialize(AddressBook addressBook)
        {
            using var memstream = new MemoryStream();
            using var googleStream = new Google.Protobuf.CodedOutputStream(memstream);
            addressBook.WriteTo(googleStream);

            return memstream.ToArray();
        }
    }
}
