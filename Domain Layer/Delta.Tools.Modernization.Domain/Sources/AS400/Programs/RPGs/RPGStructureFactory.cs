using Delta.Tools.AS400.Programs.RPGs;
using Delta.Tools.AS400.Programs.RPGs.Forms;
using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.AS400.Sources;
using System;

namespace Delta.Tools.AS400.Analyzer.RPGs
{
    class RPGStructureFactory
    {
        internal static void CreateRPGLines(Source source, RPGLineFactory _RPGLineFactory, RPGStructure RPGStructure)
        {

            var formType = FormType.Control;
            int originalLineStartIndex = 0;
            var enteredProgramDataLines=false;
            for (var originalLinesIndex = 0; originalLinesIndex < source.OriginalLines.Length; originalLinesIndex++)
            {
                originalLineStartIndex = originalLinesIndex;
                var originalLine = source.OriginalLines[originalLinesIndex];
                //var nextOriginalLine = originalLinesIndex + 1 < source.OriginalLines.Length ? source.OriginalLines[originalLinesIndex + 1] : string.Empty;
                formType = enteredProgramDataLines? FormType.ProgramData:formType.OfNextLine(source.OriginalLines, originalLinesIndex);
            
                if(formType==FormType.ProgramData && !enteredProgramDataLines) enteredProgramDataLines=true;

                if (RPGCommentLineFactory.IsSingleLineComment(originalLine) || originalLine.EndsWith("/EJECT"))
                {
                    RPGStructure.Add(new RPGCommentLine(formType, originalLine, originalLineStartIndex));
                    continue;
                }

                if (originalLinesIndex == source.OriginalLines.Length - 1)
                {
                    RPGStructure.Add(_RPGLineFactory.Create(formType, source.ObjectID, originalLine, originalLinesIndex, originalLinesIndex));
                    break;
                }

                var joinedLine = originalLine.Substring(0, Math.Min(originalLine.Length, 80)).TrimEnd();

                while (originalLinesIndex + 1 < source.OriginalLines.Length)
                {
                    var nextLine = source.OriginalLines[originalLinesIndex + 1];
                    if (!IsJoinee(nextLine, RPGStructure)) break;

                    originalLinesIndex++;

                    joinedLine += " " + nextLine[26..Math.Min(nextLine.Length, 80)].Trim();

                }

                if (joinedLine.Length == 6 && joinedLine.Trim().Length == 1)
                {
                    RPGStructure.Add(new RPGCommentLine(formType, originalLine, originalLineStartIndex));
                }
                else
                {
                    var rpgLine = _RPGLineFactory.Create(formType, source.ObjectID, joinedLine, originalLineStartIndex, originalLinesIndex);
                    RPGStructure.Add(rpgLine);
                }

            }

            RPGStructure.SetCommentCount();
            RPGStructure.ReformBlocks();
        }


        static bool IsJoinee(string originalLine, RPGStructure RPGStructure)
        {
            if (RPGStructure is RPGStructure3) return false;

            if (RPGCommentLineFactory.IsSingleLineComment(originalLine)) return false;

            return FormTypeExtend.IsCaluculationLine(originalLine) && originalLine.Length > 25 && originalLine.Substring(25, 1) == " ";
        }
    }
}
