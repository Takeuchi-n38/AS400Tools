using Delta.Tools.Sources.Lines;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.Sources.Statements.Singles.Comments
{
    public class CommentLine : Line, ICommentStatement//TODO
    {

        CommentLine(string value, int startLineIndex) : base(value, startLineIndex)
        {
        }

        public static CommentLine Of(string value, int startLineIndex)
        {
            return new CommentLine(value, startLineIndex);
        }


    }
}
