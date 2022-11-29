using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.Sources.Statements.Singles;
using System.Collections.Generic;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Dims
{
    public class DimCtdataLine4 : RPGLine4
    {
        public readonly IRPGDefinitionLine line;

        public List<string> programDataValues = new List<string>();
        public void Add(string programJoinedDataValues)
        {
            programDataValues.AddRange(ProgramDataValues(line.DecimalPositions,line.Perrcd,line.ToPosition_Length,programJoinedDataValues));
        }

        public DimCtdataLine4(IRPGDefinitionLine line) : base(line.FormType, line.Value, line.StartLineIndex)
        {
            this.line = line;
        }

        public static List<string> ProgramDataValues(
            string DecimalPositions,string Perrcd,string ToPosition_Length,
            string programJoinedDataValues)
        {
            var IsStringType = DecimalPositions == string.Empty;
            var trimedProgramJoinedDataValues = programJoinedDataValues.TrimEnd();
            if (int.Parse(Perrcd) == 1) return new List<string>() { IsStringType ? $"\"{trimedProgramJoinedDataValues}\"" : trimedProgramJoinedDataValues };

            var values = new List<string>();

            for (int i = 0; i < int.Parse(Perrcd); i++)
            {
                var itemLength = int.Parse(ToPosition_Length);
                var value = IsStringType ? string.Empty : "0";
                if (trimedProgramJoinedDataValues.Length >= i * itemLength + itemLength)
                {
                    value = trimedProgramJoinedDataValues.Substring(i * itemLength, itemLength);
                }
                values.Add(IsStringType ? $"\"{value}\"" : value);
            }

            return values;
        }

        //public string ModernTypeName => VariableFactory.Of((IRPGDefinitionLine4)this).TypeSpelling;

        //internal bool IsStringType => line.DecimalPositions == string.Empty;
    }
}
//0024      D MSG             S             70    DIM(41) CTDATA PERRCD(1)             M_01
//0025      D PFK             S             70    DIM(6) CTDATA PERRCD(1)          
//0026      D DT              S              2  0 DIM(12) CTDATA PERRCD(12)      
/*
桁 意味	備考
6	仕様書コード	E
27-32	テーブル名	TABで始まる任意のテーブル名
33-35	1レコードあたりの項目数	コンパイル時配列・テーブルの場合に使用
36-39	テーブルの項目数	1-9999
40-42	要素の桁長	1-256
44	小数点以下の桁数	数値の場合に指定（0-9）
45	順序（昇順・降順）	A：昇順　D：降順
LOKUP命令などに使用
46-51	テーブル名（交互形式のもう一つ）	TABで始まる任意のテーブル名
52-54	要素の桁長	1-256
56	小数点以下の桁数	数値の場合に指定（0-9）
57	順序（昇順・降順）	A：昇順　D：降順
LOKUP命令に使用
58-74	注釈+*/