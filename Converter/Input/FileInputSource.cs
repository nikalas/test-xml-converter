using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Converter.Input
{
    internal class FileInputSource(string path) : IInputSource
    {
        public string FilePath { get; } = path;

        public TextReader Open()
        {
            return new StreamReader(FilePath);
        }
    }
}
