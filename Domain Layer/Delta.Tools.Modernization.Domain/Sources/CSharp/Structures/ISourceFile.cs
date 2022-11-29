using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.CSharp.Structures
{
    public interface ISourceFile
    {
       // string FilePath { get; }
        string ToSourceContents();
    }
}
