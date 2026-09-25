using System;
using System.Collections.Generic;
using System.Text;

namespace Converter.Domain
{
    internal class Address
    {
        public string? Street { get; init; }
        public string? City { get; init; }
        public string? Zip { get; init; }

        public override string ToString()
            => $"{Street}, {City}, {Zip}";
    }
}
