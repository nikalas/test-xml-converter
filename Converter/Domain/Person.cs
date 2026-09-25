using System;
using System.Collections.Generic;
using System.Text;

namespace Converter.Domain
{
    internal class Person
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public int? BirthYear { get; init; }
        public Address? Address { get; init; }
        public Phone? Phone { get; init; }
        public Person? RelatedTo { get; init; }

        public string FullName() => $"{FirstName} {LastName}";

        public override string ToString() 
            => $"Person: {FullName()}, BirthYear: {BirthYear}, Address: {Address}, Mobile: {Phone.Mobile}, RelatedTo: {RelatedTo?.FullName()}";
    }
}
