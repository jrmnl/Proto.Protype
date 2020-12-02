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

            using var memstream = new MemoryStream();
            using (var googleStream = new Google.Protobuf.CodedOutputStream(memstream, leaveOpen: true))
            {
                addressBook.WriteTo(googleStream);
            }

            memstream.Seek(0, SeekOrigin.Begin);

            var addressBookParsed = AddressBook.Parser.ParseFrom(memstream.ToArray());

            Assert.True(addressBook.Equals(addressBookParsed));
        }
    }
}
