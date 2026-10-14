using Converter.Domain;
using Converter.Formats;

namespace Converter
{
    internal class Program
    {
        static readonly string DEFAULT_PATH = "input.txt";
        static void Main(string[] args)
        {

            // Tests.ModelTests.ConfirmModel();
            // return;

            // Tests.ModelTests.ConfirmStructure();
            // return;

            string path = args.Length > 0 ? args[0] : DEFAULT_PATH;
            Console.WriteLine($"Using input file: {path}");

            // get the absolute path of the input file
            if (!File.Exists(path))
            {
                Console.WriteLine($"File not found: {path}");
                return;
            }

            path = Path.GetFullPath(path);
            Console.WriteLine($"Absolute path: {path}");


            using var source = new Input.FileInputSource(path).Open();
            IImporter importer = new Formats.Custom.CustomImporter();
            List<Domain.Person> people = importer.Import(source);
            IExporter exporter = new Formats.XML.XMLExporter();
            var xml = exporter.Export(people);
            // new Output.FileOutputDestination("output.xml").Write(xml);

        }

    }
}
