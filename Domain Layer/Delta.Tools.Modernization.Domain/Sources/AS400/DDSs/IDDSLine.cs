using Delta.AS400.DataTypes;
using Delta.AS400.DataTypes.Characters;
using Delta.Tools.Sources.Lines;
using Delta.Tools.Sources.Statements.Singles;
using Delta.Utilities.Extensions.SystemString;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.DDSs
{
    public interface IDDSLine : ILine, ISingleStatement
    {
        bool IsCommentLine => Value.Substring(6, 1) == "*";

        public string TypeOfNameOrSpecification => Value[16].ToString().TrimEnd();

        public string Name => Value[18..28].TrimEnd();
        public bool HaveName => Name != string.Empty;
        public bool IsRecordFormatHeaderLine => TypeOfNameOrSpecification == "R" && HaveName;

        public bool IsVariable => !IsCommentLine && HaveName && !IsRecordFormatHeaderLine;

        string Reference => Value[28].ToString().TrimEnd();

        public bool IsREFFLDLine => TypeOfNameOrSpecification == string.Empty && Reference == "R";

        public string Length => Value.Substring(29, 5).TrimStart();

        public string DataType => Value.Substring(34, 1).Trim();

        public string DecimalPositions => Value.Substring(35, 2).TrimStart();
        public IDataTypeDefinition ToTypeDefinition => DataTypeDefinition.Of(Name, Length, DataType, DecimalPositions,Colhdg);

        public string Usage => Value.Substring(37, 1).TrimEnd();

        public string Line => Value.Substring(38, 3).TrimStart();//no diskFile
        //Position(positions 42 through 44)
        public string Position => Value.Substring(41, 3).TrimStart();//no diskFile

        public bool HavePosition => Position != string.Empty;
        public bool IsOutput => !IsCommentLine && HavePosition;

        public string Keywords => Value.Substring(44).Trim();

        public bool IsConst => Keywords.StartsWith('\'');

        public int LengthOfConst => CodePage930.GetByteLength(Keywords.Replace("'",string.Empty));

        public bool HaveKeywords => Keywords != string.Empty;

        public bool CanJoin => !IsCommentLine && HaveKeywords && Keywords.EndsWith("+");

        public string Colhdg => Keywords.Contains("COLHDG") ? TextClipper.ClipParameter(Keywords,"COLHDG").Replace("'",string.Empty):string.Empty;


    }
}
