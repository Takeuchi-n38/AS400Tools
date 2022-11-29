using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.DDSs.DisplayFiles.Commands
{
    public class FunctionCommand : Command
    {
        public FunctionCommand(int number) : base(number)
        {
        }
        public FunctionCommand(int number, Indicator? indicator) : base(number, indicator)
        {
        }

    }
}