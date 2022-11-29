using System;
using System.Linq;

namespace Delta.Products
{
    public class ProductItemCode
    {
        public readonly string Full16Digits;
        public string First11Digits => Full16Digits.Substring(0,11);
        public string First15Digits => Full16Digits.Substring(0, 15);
        public string GetSkip0Take4 => Full16Digits.Substring(0, 4);
        public string GetSkip4Take4 => Full16Digits.Substring(4, 4);
        public string GetSkip8Take4 => Full16Digits.Substring(8, 4);

        public string Nksm => Full16Digits.Substring(4, 7);
        public string GetSkip9Take5 => Full16Digits.Substring(9, 5);
        public string GetSkip9Tak6 => Full16Digits.Substring(9, 6);

        public string Suffix => Full16Digits.Substring(11, 1);
        public string ColorAndProcessCode => Full16Digits.Substring(12, 3);
        public string ColorCode => Full16Digits.Substring(12, 2);
        public string ProcessCode => Full16Digits.Substring(14, 1);
        public string FactoryCode => Full16Digits.Substring(15, 1);
        ProductItemCode(string aFull16Digits)
        {
            this.Full16Digits = aFull16Digits;
        }

        public static ProductItemCode Of(string number)
        {
            return new ProductItemCode((number ?? string.Empty).PadRight(16));
        }

        public static ProductItemCode NullValue = Of(string.Empty);

        public bool IsNullValue => this.Equals(NullValue);

        public ProductItemCode CreateOfFirst11Digits()
        {
            return Of(First11Digits);
        }


        public ProductItemCode ChangeProcessCodeToRinCaseOfNotNullValue(){
            return IsNullValue ? this : ChangeProcessCodeToR();
        }

        public ProductItemCode ChangeProcessCode(char processCode)
        {
            return Of($"{Full16Digits.Substring(0, 14)}{processCode}{Full16Digits.Substring(15, 1)}");
        }

        public ProductItemCode ClearProcessCode()
        {
            return ChangeProcessCode(' ');
        }

        public ProductItemCode ChangeProcessCodeToR()
        {
            return ChangeProcessCode('R');
        }

        public ProductItemCode ClearFactoryCode()
        {
            return ChangeFactoryCode(' ');
        }

        public ProductItemCode ChangeFactoryCode(char factoryCode)
        {
            return Of($"{Full16Digits.Substring(0, 15)}{factoryCode}");
        }

        //public ProductItemCode ChangeFactoryCodeToR()
        //{
        //    return ChangeFactoryCode('R');
        //}

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var target = (ProductItemCode)obj;

            if (target.Full16Digits != Full16Digits) return false;

            return true;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Full16Digits.GetHashCode();
        }

    }
}
