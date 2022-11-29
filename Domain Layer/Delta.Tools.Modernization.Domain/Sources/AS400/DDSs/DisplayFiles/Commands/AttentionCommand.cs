using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.DDSs.DisplayFiles.Commands
{
    public class AttentionCommand : Command
    {
        public AttentionCommand(int number) : base(number)
        {
        }
        public AttentionCommand(int number, Indicator? indicator) : base(number, indicator)
        {
        }

    }
}
