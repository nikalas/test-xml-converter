namespace Converter.Formats
{
	internal interface IImporter
	{
		List<Domain.Person> Import(TextReader reader);
	}
}