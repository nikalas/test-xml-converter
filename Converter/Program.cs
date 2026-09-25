using Converter.Domain;

namespace Converter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var testPerson = new Domain.Person
            {
                FirstName = "John",
                LastName = "Doe",
                BirthYear = 1990,
                Address = new Domain.Address
                {
                    Street = "123 Main St",
                    City = "Anytown",
                    Zip = "12345"
                },
                Phone = new Domain.Phone
                {
                    Landline = "555-1234",
                    Mobile = "555-5678"
                }
            };
            var childPerson = new Domain.Person
            {
                FirstName = "Jane",
                LastName = "Doe",
                BirthYear = 2015,
                Address = new Domain.Address
                {
                    Street = "123 Main St",
                    City = "Anytown",
                    Zip = "12345"
                },
                Phone = new Domain.Phone
                {
                    Landline = "555-1234",
                    Mobile = "555-5678"
                },
                RelatedTo = testPerson
            };
            var siblingPerson = new Domain.Person
            {
                FirstName = "Jack",
                LastName = "Doe",
                BirthYear = 2012,
                Address = new Domain.Address
                {
                    Street = "123 Main St",
                    City = "Anytown",
                },
                Phone = new Domain.Phone
                {
                    Landline = "555-1234",
                },
                RelatedTo = testPerson
            };
            var grandChildPerson = new Domain.Person
            {
                FirstName = "Jill",
                LastName = "Doe",
                BirthYear = 2020,
                Address = new Domain.Address
                {
                    Street = "123 Main St",
                    Zip = "12345"
                },
                Phone = new Domain.Phone
                {
                    Landline = "555-1234",
                    Mobile = "555-5678"
                },
                RelatedTo = childPerson
            };
            var unrelatedPerson = new Domain.Person
            {
                FirstName = "Bob",
                LastName = "Smith",
                Address = new Domain.Address
                {
                    Street = "456 Elm St",
                    City = "Othertown",
                    Zip = "67890"
                },
            };

            // Person class works as expected?
            Console.WriteLine("-------------- person ----------------");
            Console.WriteLine(testPerson);


            // Relationship between persons works as expected?
            Console.WriteLine("---------------- relationship ----------------");
            Console.WriteLine(childPerson.RelatedTo);


            // Relationships in containers works as expected?
            List<Person> personContainer =
            [
                testPerson,
                childPerson,
                siblingPerson,
                grandChildPerson,
                unrelatedPerson
            ];

            // grab people with relation to testperson
            var relatedPerson = personContainer.FirstOrDefault(p => p.RelatedTo == testPerson);
            Console.WriteLine("---------------- related people ----------------");
            Console.WriteLine($"Related person to {testPerson.FullName()}: {relatedPerson?.FullName()}");

            Console.WriteLine("-------------- all relations ----------------");
            List<Person> relatedPeople = [.. personContainer.Where(p => p.RelatedTo == testPerson)];
            foreach (var person in relatedPeople)
            {
                Console.WriteLine($"Person: {person.FullName()}, Related to: {person.RelatedTo?.FullName() ?? "None"}");
            }

        }
    }
}
