namespace System.Emulater.Indicator
{
    public class ArrayIndicator : Indicator
    {
        public readonly int index;//01-99
        public ArrayIndicator(int index) : base(IndicatorType.Array)
        {
            this.index = index;
        }

        public static ArrayIndicator CreateBy(int index)
        {
            return new ArrayIndicator(index);
        }

        public string ToJSON()
        {
            return String.Join("{",
                "\"index\"", ":", index.ToString(), ",",
                "\"on\"", ":", EqualOn().ToString(),
                "}");
        }

        public new bool Equals(object obj)
        {
            if (this == obj)
                return true;
            if (obj == null)
                return false;
            if (this != obj)
                return false;

            return ((ArrayIndicator)obj).index == this.index;
        }

    }
}