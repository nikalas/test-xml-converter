using System.Xml.Linq;

namespace Converter.Formats.XML
{
	internal class XMLExporter : IExporter
	{
		// Create element unless falsy value, in which case return null
		private static XElement? Element(string name, string? value) =>
			string.IsNullOrEmpty(value) ? null : new XElement(name, value);

		// Create element unless falsy value, in which case return null
		private static XElement? Element(string name, int? value) =>
			value.HasValue ? new XElement(name, value.Value) : null;

		// Create container element unless all child elements are null, in which case return null
		private static XElement? Container(string name, params XElement?[] elements)
		{
			var validElements = elements.OfType<XElement>().ToArray();
			return validElements.Length > 0
				? new XElement(name, validElements)
				: null;
		}

		// Export a list of people to XML format
		public string Export(List<Domain.Person> people)
		{
			var root = new XElement("people",
				from person in people
				where person.RelatedTo == null
				select new XElement("person",
					Element("firstname", person.FirstName),
					Element("lastname", person.LastName),

					Container("address",
						Element("street", person.Address?.Street),
						Element("city", person.Address?.City),
						Element("zip", person.Address?.Zip)
					),

					Container("phone",
						Element("mobile", person.Phone?.Mobile),
						Element("landline", person.Phone?.Landline)
					),

					from child in people
					where child.RelatedTo == person
					select Container("family",
						Element("name", child.FirstName),
						Element("born", child.BirthYear),

						Container("address",
							Element("street", child.Address?.Street),
							Element("city", child.Address?.City),
							Element("zip", child.Address?.Zip)
						),

						Container("phone",
							Element("mobile", child.Phone?.Mobile),
							Element("landline", child.Phone?.Landline)
						)
					)
				)
			);

			return root.ToString();
		}
	}
}