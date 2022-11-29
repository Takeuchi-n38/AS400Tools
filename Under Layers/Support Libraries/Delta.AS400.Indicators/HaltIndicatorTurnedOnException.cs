using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.AS400.Indicators
{
    public class HaltIndicatorTurnedOnException:Exception
    {
        public readonly int IndicatorNumber;

        HaltIndicatorTurnedOnException(int aIndicatorNumber)
        {
            this.IndicatorNumber = aIndicatorNumber;
        }

        public static HaltIndicatorTurnedOnException OfH1()
        {
            return new HaltIndicatorTurnedOnException(1);
        }
        public static HaltIndicatorTurnedOnException OfH2()
        {
            return new HaltIndicatorTurnedOnException(2);
        }

        public static HaltIndicatorTurnedOnException OfH3()
        {
            return new HaltIndicatorTurnedOnException(3);
        }

        public static HaltIndicatorTurnedOnException OfH4()
        {
            return new HaltIndicatorTurnedOnException(4);
        }

        public static HaltIndicatorTurnedOnException OfH5()
        {
            return new HaltIndicatorTurnedOnException(5);
        }
    }
}
