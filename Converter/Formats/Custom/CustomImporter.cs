namespace Converter.Formats.Custom
{
	internal class CustomImporter : IImporter
	{
		private readonly List<Domain.Person> _people = [];
		private const string DELIMITER = "|";
		public List<Domain.Person> Import(TextReader reader)
		{
			while (reader.ReadLine() is string line)
			{
				if (string.IsNullOrWhiteSpace(line))
				{
					continue;
				}

				var fields = line.Split(DELIMITER);
				char type = fields[0][0]; // Get the first character of the first field to determine the type

				switch (type)
				{
					case 'P':
						// Parse a person entry
						_people.Add(new Domain.Person()
						{
							FirstName = fields.ElementAtOrDefault(1),
							LastName = fields.ElementAtOrDefault(2),
						});
						// Console.WriteLine($"Added person: {_people.Last().FullName()}");
						break;

					case 'A':
						// Parse an address entry
						_people.Last().Address = new Domain.Address
						{
							Street = fields.ElementAtOrDefault(1),
							City = fields.ElementAtOrDefault(2),
							Zip = fields.ElementAtOrDefault(3)
						};
						break;

					case 'T':
						// Parse a phone entry
						_people.Last().Phone = new Domain.Phone
						{
							Landline = fields.ElementAtOrDefault(1),
							Mobile = fields.ElementAtOrDefault(2)
						};
						break;

					case 'F':
						// Parse a family entry
						var family = new Domain.Person()
						{
							FirstName = fields.ElementAtOrDefault(1),
							BirthYear = int.TryParse(fields.ElementAtOrDefault(2), out var birthYear) ? birthYear : null,
						};
						_people.Last().RelatedTo = family;
						break;

					default:
						Console.WriteLine($"Unknown entry type: {line}");
						break;
				}

				// Placeholder implementation to satisfy the interface.
				// Replace with real parsing logic when you have the file format.
				Console.WriteLine($"Read line: {line}");
			}
			return _people;
		}
	}

}