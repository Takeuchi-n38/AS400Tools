namespace Delta.AS400.Functions
{
    public abstract class Edtcde
    {
        readonly protected decimal value;
        protected Edtcde(decimal value)
        {
            this.value = value;
        }

        public abstract string FormattedValue();

        //public static string J(decimal value)
        //{
        //    if (value == 0) return "";
        //    return value.ToString("#,#.00 ;#,#.00-");
        //}

        public static string K(decimal value)
        {
            if (value == 0) return "";
            return value.ToString("#,#.00 ;#,#.00-");
        }

        public static string M(decimal value)
        {
            if (value == 0) return "";
            return value.ToString("#.00 ;#.00-");
        }
    }
}
//,     0      //負数表示

//             -（後ろマイナス）	符号なし	浮動負符号（前マイナス）	CR
//あり	あり   J	1	N	A
//あり	なし   K	2	O	B
//なし	あり   L	3	P	C
//なし	なし   M	4	Q	D
