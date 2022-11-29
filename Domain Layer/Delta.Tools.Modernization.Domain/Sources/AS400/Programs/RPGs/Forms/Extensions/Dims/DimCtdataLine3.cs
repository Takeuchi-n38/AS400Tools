using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions;
using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Dims;
using Delta.Tools.AS400.Programs.RPGs.Lines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Extensions.Dims
{
    public class DimCtdataLine3: RPGLine3
    {
        public readonly IRPGExtensionLine line;

        public List<string> programDataValues = new List<string>();
        public void Add(string programJoinedDataValues)
        {
            programDataValues.AddRange(ProgramDataValues(line.Perrcd,line.ArrayLength,line.ItemLength,line.DecimalPositions, programJoinedDataValues));
        }

        public DimCtdataLine3(IRPGExtensionLine3 line) : base(line.FormType, line.Value, line.StartLineIndex)
        {
            this.line = line;
        }

        static List<string> ProgramDataValues(
            string Perrcd,string ArrayLength, string ItemLength, string DecimalPositions,
            string programJoinedDataValues)
        {
            var IsStringType = DecimalPositions == string.Empty;
            var trimedProgramJoinedDataValues = programJoinedDataValues.TrimEnd();
            if (int.Parse(ArrayLength) == 1) return new List<string>() { IsStringType ? $"\"{trimedProgramJoinedDataValues}\"" : trimedProgramJoinedDataValues };

            var values = new List<string>();
            if (Perrcd == "1")
            {
                var value = trimedProgramJoinedDataValues;
                if(value==string.Empty&&!IsStringType) value = "0";
                values.Add(IsStringType ? $"\"{value}\"" : value);
            }
            else
            {
                for (int i = 0; i < int.Parse(Perrcd); i++)
                {
                    var itemLength = int.Parse(ItemLength);
                    var value = IsStringType ? string.Empty : "0";
                    if (trimedProgramJoinedDataValues.Length >= i * itemLength + itemLength)
                    {
                        value = trimedProgramJoinedDataValues.Substring(i * itemLength, itemLength);
                    }
                    values.Add(IsStringType ? $"\"{value}\"" : value);
                }
            }

            return values;
        }

    }
}
