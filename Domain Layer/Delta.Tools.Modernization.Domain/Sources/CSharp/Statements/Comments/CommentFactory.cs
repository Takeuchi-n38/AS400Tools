using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.CSharp.Statements.Comments
{
    public class CommentFactory
    {
        public static string[] OriginalLineCommentLines(string[] OriginalLines)
        {
            string[] lines = new string[OriginalLines.Length];
            for (var i = 0; i < OriginalLines.Length; i++)
            {
                lines[i] = $"//{i.ToString("D4")} {OriginalLines[i]}";
            }
            return lines;
        }
    }
}
