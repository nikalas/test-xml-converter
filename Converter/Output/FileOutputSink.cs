namespace Converter.Output;

public sealed class FileOutputSink(string filePath) : IOutputSink
{
	public void Write(string content)
	{
		File.WriteAllText(filePath, content);
	}
}