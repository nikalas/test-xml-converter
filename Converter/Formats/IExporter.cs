namespace Converter.Formats
{
	internal interface IExporter
	{
		public string Export(List<Domain.Person> people);
	}
}