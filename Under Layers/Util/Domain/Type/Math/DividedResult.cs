using System;

namespace Domain.Type.Math
{
    public class DividedResult
    {

        public readonly int quotient;
        public readonly int remainder;

        private DividedResult(int aQuotient, int aRemainder)
        {
            quotient = aQuotient;
            remainder = aRemainder;
        }

        public static DividedResult Of(int target, int divisor)
        {

            if (divisor == 0) throw new ArgumentOutOfRangeException("divisor==0");

            var quotient = target / divisor;
            var remainder = target % divisor;
            return new DividedResult(quotient, remainder);
        }

    }
}
