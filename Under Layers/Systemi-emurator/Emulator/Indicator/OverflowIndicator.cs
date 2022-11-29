namespace System.Emulater.Indicator
{
    public class OverflowIndicator : Indicator
    {
        private readonly char index;// OA-OG OV

        private OverflowIndicator(char index) : base(IndicatorType.Overflow)
        {
            this.index = index;
        }

        public static OverflowIndicator CreateBy(char index)
        {
            return new OverflowIndicator(index);
        }

        public new bool Equals(Object obj)
        {
            if (this == obj)
                return true;
            if (obj == null)
                return false;
            if (this != obj)
                return false;

            return ((OverflowIndicator)obj).index == this.index;
        }

    }
}