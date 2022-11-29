namespace Delta.Tools.Sources.Lines
{
    public static class ILineExtention
    {

        public static string ToOriginalComment(this ILine line) => $"//{line.StartLineIndex:D4} {line.Value}";

    }
}
