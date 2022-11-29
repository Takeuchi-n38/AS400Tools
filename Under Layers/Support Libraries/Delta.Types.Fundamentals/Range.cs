namespace Delta.Types.Fundamentals
{
    public class Range<T>
    {
        public readonly T From;

        public readonly T To;

        protected Range(T aFrom, T aTo)
        {
            From = aFrom;
            To = aTo;
        }

        public static Range<T> Of(T aFrom, T aTo) => new Range<T>(aFrom,aTo);
    }
}
