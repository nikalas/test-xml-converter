using System.Xml.Linq;

namespace Converter.Formats.XML
{
	internal class XMLExporter : IExporter
	{
		public string Export(List<Domain.Person> people)
		{
			var root = new XElement("people",
				from person in people
				where person.RelatedTo == null
				let children = people
					.Where(child => child.RelatedTo == person)
					.ToList()
				select new XElement("person",
					person.FirstName != null
						? new XElement("firstname", person.FirstName)
						: null,

					person.LastName != null
						? new XElement("lastname", person.LastName)
						: null,

					person.Address != null &&
					(person.Address.Street != null ||
					 person.Address.City != null ||
					 person.Address.Zip != null)
						? new XElement("address",
							person.Address.Street != null
								? new XElement("street", person.Address.Street)
								: null,

							person.Address.City != null
								? new XElement("city", person.Address.City)
								: null,

							person.Address.Zip != null
								? new XElement("zip", person.Address.Zip)
								: null
						)
						: null,

					person.Phone != null &&
					(person.Phone.Landline != null ||
					 person.Phone.Mobile != null)
						? new XElement("phone",
							person.Phone.Landline != null
								? new XElement("landline", person.Phone.Landline)
								: null,

							person.Phone.Mobile != null
								? new XElement("mobile", person.Phone.Mobile)
								: null
						)
						: null,

					children.Count > 0
						? new XElement("family",
							from child in children
							select new XElement("child",
								child.FirstName != null
									? new XElement("name", child.FirstName)
									: null,

								child.BirthYear.HasValue
									? new XElement("born", child.BirthYear.Value)
									: null
							)
						)
						: null
				)
			);

			var tempFilePath = Path.GetTempFileName();
			root.Save(tempFilePath);

			Console.WriteLine($"XML file saved to: {tempFilePath}");

			return root.ToString();
		}
	}
}