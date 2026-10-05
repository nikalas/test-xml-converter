using System;
using System.Collections.Generic;
using System.Text;

namespace Converter.Domain
{
    internal class Person
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? BirthYear { get; set; }
        public Address? Address { get; set; }
        public Phone? Phone { get; set; }
        public Person? RelatedTo { get; set; }

        public string FullName() => $"{FirstName} {LastName}";

        public override string ToString()
            => $"Person: {FullName()}, BirthYear: {BirthYear}, Address: {Address}, Landline: {Phone?.Landline}, Mobile: {Phone?.Mobile}, RelatedTo: {RelatedTo?.FullName()}";
    }
}
