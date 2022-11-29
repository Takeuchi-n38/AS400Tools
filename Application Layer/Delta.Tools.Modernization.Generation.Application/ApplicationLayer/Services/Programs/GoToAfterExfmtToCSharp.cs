using Delta.Tools.Sources.Items;
using System.Collections.Generic;

namespace Delta.Tools.AS400.Generator.ApplicationLayer.Services.Programs
{
    public class GoToAfterExfmtToCSharp
    {
        public static void GenerateCSharpSourceLines(List<string> CSharpSourceLines, Indent indent, int ExfmtLineNumber, bool IsCommentOut)
        {
            var commentOut = IsCommentOut ? "//" : string.Empty;
            CSharpSourceLines.Add($"{indent}{commentOut}if(GoToAfterExfmt{ExfmtLineNumber:D4})");
            CSharpSourceLines.Add($"{indent}{commentOut}{{");
            CSharpSourceLines.Add($"{indent.Increment()}{commentOut}GoToAfterExfmt{ExfmtLineNumber:D4} = false;");
            CSharpSourceLines.Add($"{indent.Increment()}{commentOut}goto AfterExfmt{ExfmtLineNumber:D4};");
            CSharpSourceLines.Add($"{indent}{commentOut}}}");
        }
    }
}
