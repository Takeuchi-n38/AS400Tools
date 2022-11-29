namespace System.Emulater.Indicator
{
    public class ControlLevelIndicator : Indicator
    {

        public readonly int index;//1-9

        private ControlLevelIndicator(int index) : base(IndicatorType.Array)
        {
            this.index = index;
        }

        private static ControlLevelIndicator CreateBy(int index)
        {
            return new ControlLevelIndicator(index);
        }

        public static ControlLevelIndicator CreateL1()
        {
            return CreateBy(1);
        }

        public static ControlLevelIndicator CreateL2()
        {
            return CreateBy(2);
        }

        public static ControlLevelIndicator CreateL3()
        {
            return CreateBy(3);
        }

        public static ControlLevelIndicator CreateL4()
        {
            return CreateBy(4);
        }

        public static ControlLevelIndicator CreateL5()
        {
            return CreateBy(5);
        }

        public static ControlLevelIndicator CreateL6()
        {
            return CreateBy(6);
        }

        public static ControlLevelIndicator CreateL7()
        {
            return CreateBy(7);
        }

        public static ControlLevelIndicator CreateL8()
        {
            return CreateBy(8);
        }

        public static ControlLevelIndicator CreateL9()
        {
            return CreateBy(9);
        }

    }

}