namespace System.Emulater.Indicator
{
    public class HaltIndicator : Indicator
    {

        private readonly int index;//H1-H9

        private HaltIndicator(int index) : base(IndicatorType.Halt)
        {
            this.index = index;
        }

        public static HaltIndicator CreateH1()
        {
            return new HaltIndicator(1);
        }
        public static HaltIndicator CreateH2()
        {
            return new HaltIndicator(2);
        }
        public static HaltIndicator CreateH3()
        {
            return new HaltIndicator(3);
        }
        public static HaltIndicator CreateH4()
        {
            return new HaltIndicator(4);
        }
        public static HaltIndicator CreateH5()
        {
            return new HaltIndicator(5);
        }
        public static HaltIndicator CreateH6()
        {
            return new HaltIndicator(6);
        }
        public static HaltIndicator CreateH7()
        {
            return new HaltIndicator(7);
        }
        public static HaltIndicator CreateH8()
        {
            return new HaltIndicator(8);
        }
        public static HaltIndicator CreateH9()
        {
            return new HaltIndicator(9);
        }

        public new bool Equals(Object obj)
        {
            if (this == obj)
                return true;
            if (obj == null)
                return false;
            if (this != obj)
                return false;

            return ((HaltIndicator)obj).index == this.index;
        }

    }
}