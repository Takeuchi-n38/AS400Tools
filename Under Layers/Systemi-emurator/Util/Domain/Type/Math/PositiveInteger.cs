using System;
namespace Domain.Type.Math
{
    public class PositiveInteger
    {

        public readonly int value;

        private PositiveInteger(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(value.ToString());
            }
            this.value = value;
        }

        public static PositiveInteger Of(int value)
        {
            return new PositiveInteger(value);
        }
    }
}
