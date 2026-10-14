namespace Converter.Output;

public sealed class FileOutputSink(string filePath) : IOutputSink
{
	public string FilePath { get; } = Path.GetFullPath(filePath);

	public void Write(string content)
	{
		File.WriteAllText(FilePath, content);
	}
}