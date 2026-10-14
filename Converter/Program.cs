using Converter.Domain;
using Converter.Formats;
using Converter.Input;
// using Converter.Output;

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

            // create components for the conversion pipeline
            IInputSource source = new Input.FileInputSource(path);
            IImporter importer = new Formats.Custom.CustomImporter();
            IExporter exporter = new Formats.XML.XMLExporter();
            // IWriter writer = new Output.FileSink();

            // read input, convert to domain model, and export to XML
            using var input = source.Open();
            List<Domain.Person> people = importer.Import(input);
            string output = exporter.Export(people);
            // string output = writer.Write(result);

            // just write output to file for now
            File.WriteAllText("output.xml", output);
            string outputPath = Path.GetFullPath("output.xml");
            Console.WriteLine($"XML file saved to: {outputPath}");


        }

    }
}
