namespace System.Emulater.Indicator
{
    public class LastRecordIndicator : Indicator
    {
        private LastRecordIndicator() : base(IndicatorType.LastRecord)
        {
        }

        public static LastRecordIndicator Create()
        {
            return new LastRecordIndicator();
        }

        public new bool Equals(Object obj)
        {
            if (this == obj)
                return true;
            if (obj == null)
                return false;
            if (this != obj)
                return false;
            return true;
        }

    }

}