using Delta.Tools.AS400.Programs.RPGs.Forms.Calculations;
using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions;
using Delta.Tools.AS400.Programs.RPGs.Forms.Extensions;
using Delta.Tools.AS400.Programs.RPGs.Forms.FileDescriptions;
using Delta.Tools.AS400.Programs.RPGs.Forms.Inputs;
using Delta.Tools.AS400.Programs.RPGs.Forms.Outputs.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Programs.RPGs.Forms
{
    public class RPGLineFactory4 : RPGLineFactory
    {
        public static RPGLineFactory Instance = new RPGLineFactory4();

        protected override bool IsRPG4notRPG3 => true;

        protected override FileDescriptionLineFactory RPGFLineFactory { get; } = new FileDescriptionLineFactory4();

        protected override IExtensionLineFactory ExtensionLineFactory => new ExtensionLineFactory4();

        protected override IDefinitionLineFactory DefinitionLineFactory => new DefinitionLineFactory4();

        protected override IRPGInputLineFactory InputLineFactory => new InputLineFactory4();

        protected override IOutputLineFactory OutputLineFactory => new OutputLineFactory4();

    }

}
