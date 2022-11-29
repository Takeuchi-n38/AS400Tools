using Delta.AS400.DataTypes;
using Delta.AS400.DataTypes.Characters;
using Delta.AS400.Objects;
using Delta.Tools.AS400.Programs.RPGs.Forms.Calculations;
using Delta.Tools.AS400.Programs.RPGs.Forms.Controls;
using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions;
using Delta.Tools.AS400.Programs.RPGs.Forms.Extensions;
using Delta.Tools.AS400.Programs.RPGs.Forms.FileDescriptions;
using Delta.Tools.AS400.Programs.RPGs.Forms.Inputs;
using Delta.Tools.AS400.Programs.RPGs.Forms.Ls;
using Delta.Tools.AS400.Programs.RPGs.Forms.Outputs.Factories;
using Delta.Tools.AS400.Programs.RPGs.Forms.ProgramDatas;
using Delta.Tools.AS400.Programs.RPGs.Lines;
using System;

namespace Delta.Tools.AS400.Programs.RPGs.Forms
{
    public abstract class RPGLineFactory
    {
        abstract protected bool IsRPG4notRPG3 {get;}
        int MaxLengthOfLine => IsRPG4notRPG3 ? RPGLine4.MaxLengthOfLine : RPGLine3.MaxLengthOfLine;

        abstract protected FileDescriptionLineFactory RPGFLineFactory { get; }

        abstract protected IExtensionLineFactory ExtensionLineFactory { get; }

        abstract protected IDefinitionLineFactory DefinitionLineFactory { get; }

        abstract protected IRPGInputLineFactory InputLineFactory { get; }

        CalculationLineFactory RPGCLineFactory { get; } = new CalculationLineFactory();

        abstract protected IOutputLineFactory OutputLineFactory { get; }


        public IRPGLine Create(FormType currentFormType, ObjectID objectIDofSource, string original, int originalLineStartIndex, int originalLineEndIndex)
        {

            if (original.Length < 6) return new ProgramDataLine(original, originalLineStartIndex); //UnKnownRPGLine.Of(original, originalLineStartIndex);

            switch (currentFormType)
            {
                case FormType.Control:
                    return new ControlLine(original, originalLineStartIndex);
                case FormType.FileDescription:
                    return RPGFLineFactory.Create(objectIDofSource, original, originalLineStartIndex, originalLineEndIndex);
                case FormType.L:
                    return new LLine(original, originalLineStartIndex);
                case FormType.Extension:
                    return ExtensionLineFactory.Create(original, originalLineStartIndex);
                case FormType.Definition:
                    return DefinitionLineFactory.Create(original, originalLineStartIndex);
                case FormType.Input:
                    return InputLineFactory.Create(original, originalLineStartIndex);
                case FormType.Calculation:
                    //return RPGCLineFactory.Create(IsRPG4notRPG3,objectIDofSource.Library, $"{original.Substring(0, 5)}C{original.Substring(6)}", originalLineStartIndex, originalLineEndIndex);
                    return RPGCLineFactory.Create(IsRPG4notRPG3,objectIDofSource.Library, original, originalLineStartIndex, originalLineEndIndex);
                case FormType.Output:
                    //return OutputLineFactory.Create($"{original.Substring(0, 5)}O{original.Substring(6)}", originalLineStartIndex);
                    return OutputLineFactory.Create(original, originalLineStartIndex);
                case FormType.ProgramData:
                    var cuttedOriginal = original.TrimEnd().Length < MaxLengthOfLine ? original.TrimEnd(): original.TrimEnd().Substring(0,MaxLengthOfLine).TrimEnd();
                    var originalByteLength= CodePage930.GetByteLength(cuttedOriginal);
                    if(originalByteLength > MaxLengthOfLine)
                    {
                        cuttedOriginal= cuttedOriginal.Substring(0, cuttedOriginal.Length-(originalByteLength - MaxLengthOfLine)).TrimEnd();
                    }
                    return new ProgramDataLine(cuttedOriginal, originalLineStartIndex);
                default:
                    throw new ArgumentException();
            }

        }

    }
}
