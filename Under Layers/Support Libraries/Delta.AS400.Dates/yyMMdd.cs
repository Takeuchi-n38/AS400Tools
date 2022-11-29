using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.AS400.Dates
{
    public class yyMMdd
    {
        public readonly string Value;

        public string yy=>Value[0..2];
        public string yyMM => Value[0..4];
        public string MM => Value[2..4];
        public string dd => Value[4..6];
        public int ddToInt => int.Parse(dd);

        //public string yyyyMMdd => (Value == "000000" || Value == "999999")? yy:"20" + Value;

        //public int yyyyMMddtoInt => int.Parse(yyyyMMdd);

        private yyMMdd(string ayyMMdd)
        {
            if(ayyMMdd.Length!=6) throw new NotImplementedException();
            Value= ayyMMdd;
        }

        public yyMMdd OfDifferent(string dd)
        {
            return new yyMMdd($"{this.yyMM}{dd}");
        }

        public static yyMMdd Of(string ayyMMdd)
        {
            return new yyMMdd(ayyMMdd);
        }

        public static yyMMdd Of(DateTime dateTime)
        {
            return new yyMMdd(dateTime.ToString("yyMMdd"));
        }

    }
}
