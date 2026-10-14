using System.CommandLine;
using Converter.Domain;
using Converter.Formats;
using Converter.Formats.Custom;
using Converter.Formats.XML;
using Converter.Input;
using Converter.Output;

namespace Converter;

internal class Program
{
    static int Main(string[] args)
    {
        var inputFormatOption = new Option<string>("--in", "-i")
        {
            Description = "Input format: custom or xml"
        };

        var outputFormatOption = new Option<string>("--out", "-o")
        {
            Description = "Output format: xml or custom"
        };

        var inputArgument = new Argument<string?>("input")
        {
            Description = "Input file, or - for stdin",
            Arity = ArgumentArity.ZeroOrOne
        };

        var outputArgument = new Argument<string?>("output")
        {
            Description = "Output file, or - for stdout",
            Arity = ArgumentArity.ZeroOrOne
        };

        var root = new RootCommand("Convert between data formats")
        {
            inputFormatOption,
            outputFormatOption,
            inputArgument,
            outputArgument
        };

        root.SetAction(parseResult =>
        {
            try
            {
                string? inputPath = parseResult.GetValue(inputArgument);
                string? outputPath = parseResult.GetValue(outputArgument);

                string? inputFormat = parseResult.GetValue(inputFormatOption);
                string? outputFormat = parseResult.GetValue(outputFormatOption);

                inputFormat = ResolveFormat(
                    inputFormat, inputPath, "custom");

                outputFormat = ResolveFormat(
                    outputFormat, outputPath, "xml");

                IImporter importer = inputFormat switch
                {
                    "custom" => new CustomImporter(),
                    "line" => new CustomImporter(),
                    "xml" => throw new NotSupportedException(
                        "XML input is not implemented yet."),
                    _ => throw new ArgumentException(
                        $"Unknown input format: {inputFormat}")
                };

                IExporter exporter = outputFormat switch
                {
                    "xml" => new XMLExporter(),
                    "custom" => throw new NotSupportedException(
                        "Custom output is not implemented yet."),
                    _ => throw new ArgumentException(
                        $"Unknown output format: {outputFormat}")
                };

                IInputSource source = inputPath is null or "-"
                    ? new StdinInputSource()
                    : new FileInputSource(inputPath);

                IOutputSink sink = outputPath is null or "-"
                    ? new StdoutOutputSink()
                    : new FileOutputSink(AddExtension(
                        outputPath, outputFormat));

                using TextReader input = source.Open();

                List<Person> people = importer.Import(input);
                string output = exporter.Export(people);

                sink.Write(output);
                return 0;
            }
            catch (Exception ex) when (
                ex is IOException or
                UnauthorizedAccessException or
                ArgumentException or
                NotSupportedException or
                FormatException)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                return 1;
            }
        });

        if (args.Length == 0 && !Console.IsInputRedirected)
            return root.Parse(new[] { "--help" }).Invoke();

        var parseResult = root.Parse(args);
        var inputPath = parseResult.GetValue(inputArgument);

        if (!Console.IsInputRedirected && (inputPath is null or "-"))
            return root.Parse(new[] { "--help" }).Invoke();

        return parseResult.Invoke();
    }

    private static string ResolveFormat(
        string? explicitFormat,
        string? path,
        string defaultFormat)
    {
        if (explicitFormat is not null)
            return explicitFormat.ToLowerInvariant();

        if (path is not null and not "-")
        {
            string extension = Path.GetExtension(path)
                .TrimStart('.')
                .ToLowerInvariant();

            if (extension is "xml" or "custom")
                return extension;
        }

        return defaultFormat;
    }

    private static string AddExtension(string path, string format)
    {
        if (Path.HasExtension(path))
            return path;

        return $"{path}.{format}";
    }
}