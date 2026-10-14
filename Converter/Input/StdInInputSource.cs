namespace Converter.Input;

public sealed class StdinInputSource : IInputSource
{
	public TextReader Open()
	{
		return Console.In;
	}
}