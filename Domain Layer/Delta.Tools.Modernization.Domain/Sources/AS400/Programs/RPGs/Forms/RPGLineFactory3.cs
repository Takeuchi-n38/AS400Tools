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
    public class RPGLineFactory3 : RPGLineFactory
    {
        public static RPGLineFactory Instance = new RPGLineFactory3();

        protected override bool IsRPG4notRPG3 => false;

        protected override FileDescriptionLineFactory RPGFLineFactory { get; } = new FileDescriptionLineFactory3();

        protected override IExtensionLineFactory ExtensionLineFactory => new ExtensionLineFactory3();

        protected override IDefinitionLineFactory DefinitionLineFactory => new DefinitionLineFactory3();

        protected override IRPGInputLineFactory InputLineFactory => new InputLineFactory3();

        protected override IOutputLineFactory OutputLineFactory => new OutputLineFactory3();

    }
}
