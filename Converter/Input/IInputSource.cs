using System;
using System.Collections.Generic;
using System.Text;

namespace Converter.Input
{
    internal interface IInputSource
    {
        TextReader Open();
    }
}
