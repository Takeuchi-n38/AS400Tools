namespace Domain.Type.Fundamental
{
    public class Range<T>
    {
        protected Range(T aFrom, T aTo)
        {
            this.itsFrom = aFrom;
            this.itsTo = aTo;

        }
        private readonly T itsFrom;
        public T From()
        {
            return itsFrom;
        }

        private readonly T itsTo;
        public T To()
        {
            return itsTo;
        }
    }
}
