using Delta.Tools.AS400.Programs.RPGs.Lines;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Outputs
{
    public interface IRPGOutputLine : IRPGLine
    {
        FormType IRPGLine.FormType => FormType.Output;

        public bool IsLineNameLine => !IsCommentLine && Name != string.Empty && (LineNameMark == "E" || LineNameMark == "D" || LineNameMark == "T");

        public bool IsLineItemLine => !IsCommentLine && Name != string.Empty && !IsLineNameLine;

        public bool IsStaticValueLine => !IsCommentLine && Name == string.Empty && EndPositionInLine != string.Empty;

        public bool IsFileNameLine => FileName != string.Empty && !FileName.StartsWith(' ');

        public string LineNameMark { get; }

        public string FileName { get; }
        string UpdateType { get; }

        bool IsForDiskFile => UpdateType == "ADD" || UpdateType == "DEL" || (UpdateType.Trim() == string.Empty && (FileName != "PRINT" && FileName != "QPRINT")) ;

        public string Name { get; }

        public string EditCodes { get; }

        public string EndPositionInLine { get; }

        public string StaticValue { get; }

        public int SkipBefore { get; }

        public int SkipAfter { get; }

        public int SpaceBefore { get; }

        public int SpaceAfter { get; }
    }
}
/*
     OFILEB   D        01 10
     O                         DATA     128
     O                                  108 '    '
     OFILEC   D        01 20
     O                         DATA     128
     O                                  108 '    '
 */
/*
     OQPRINT    E            #HED             03
     O                                           10 'PQEA050'
     O                                           74 'オーダー売上リスト'

 * */