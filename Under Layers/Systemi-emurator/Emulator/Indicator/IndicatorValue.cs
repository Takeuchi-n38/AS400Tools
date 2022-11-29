namespace System.Emulater.Indicator
{
    public class IndicatorValue
    {
        public enum EIndicatorValue
        {
            ON, OFF
        }

        public static EIndicatorValue CreateBy(bool isOn)
        {
            return isOn ? EIndicatorValue.ON : EIndicatorValue.OFF;
        }
    }

}

