using Delta.AS400.Objects;
using System;

namespace Delta.Tools.AS400.Sources
{
    public class Source
    {
        public readonly ObjectID ObjectID;

        public readonly string[] OriginalLines;

        public int OriginalStepCount => OriginalLines.Length;

        public bool IsNotFound => OriginalStepCount == 0;

        Source(ObjectID ObjectID, string[] originalLines)
        {
            this.ObjectID = ObjectID;
            OriginalLines = originalLines;
        }
        public static Source Of(ObjectID objectID, string[] originalLines)
        {
            return new Source(objectID, originalLines);
        }

        public static Source NullValue(ObjectID objectID)
        {
            return new Source(objectID, new string[0]);
        }

        public static Source Create(ObjectID objectID, string[]? originalLines)
        {
            return originalLines == null ? NullValue(objectID) : Of(objectID, originalLines);
        }

        public string Description => $"{ObjectID.Library.Partition.Name},{ObjectID.Library.Name},{ObjectID.Name},{OriginalStepCount}";

        //public string[] OriginalLineCommentLines
        //{
        //    get
        //    {
        //        string[] lines = new string[OriginalLines.Length];
        //        for (var i = 0; i < OriginalLines.Length; i++)
        //        {
        //            lines[i] = $"//{i.ToString("D4")} {OriginalLines[i]}";
        //        }
        //        return lines;
        //    }
        //}
    }
}
