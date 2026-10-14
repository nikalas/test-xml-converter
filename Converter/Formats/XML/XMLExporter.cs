using System.Xml.Linq;
namespace Converter.Formats.XML
{
	internal class XMLExporter : IExporter
	{
		public string Export(List<Domain.Person> people)
		{

			// // convert the list of people to XML format, use System.Xml.Linq.XElement to create the XML elements
			// // do not include empty elements, e.g. if a person has no mobile, do not include the mobile element
			// foreach (var person in people)
			// {
			//  Console.WriteLine($"FirstName: [{person.FirstName}], LastName: [{person.LastName}]");
			//  Console.WriteLine($"Zip: [{person.Address?.Zip}]");
			//  Console.WriteLine($"Landline: [{person.Phone?.Landline}]");
			//  Console.WriteLine($"Mobile: [{person.Phone?.Mobile}]");
			// }

			var root = new XElement("people",
				from person in people
					// no relation = top-level person/parent
				where person.RelatedTo == null
				select new XElement("person",
					new XElement("firstname", person.FirstName),
					new XElement("lastname", person.LastName),
					new XElement("address",
						new XElement("street", person.Address?.Street),
						new XElement("city", person.Address?.City),
						new XElement("zip", person.Address?.Zip)
					),
					new XElement("phone",
						new XElement("landline", person.Phone?.Landline),
						new XElement("mobile", person.Phone?.Mobile)
					),
					new XElement("family",
						// grab all children of the current person
						from child in people
						where child.RelatedTo == person
						select new XElement("child",
							new XElement("name", child.FirstName),
							new XElement("born", child.BirthYear)
						)
					)
				)
			);

			// write temp file to disk
			var tempFilePath = System.IO.Path.GetTempFileName();
			root.Save(tempFilePath);
			Console.WriteLine($"XML file saved to: {tempFilePath}");

			return root.ToString();
		}
	}
}