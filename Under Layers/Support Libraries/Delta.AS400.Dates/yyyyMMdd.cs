using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.AS400.Dates
{
    public class yyyyMMdd
    {
        public readonly string Value;
        public int ToInt => int.Parse(Value);
        public string yyyy => Value.Substring(0, 4);
        public int yyyyToInt => int.Parse(yyyy);

        public string yyyyMM=> Value.Substring(0,6);
        public int yyyyMMToInt => int.Parse(yyyyMM);
        public string yyMMdd => Value.Substring(2, 6);
        public int yyMMddToInt => int.Parse(yyMMdd);

        private yyyyMMdd(string ayyyyMMdd)
        {
            if (ayyyyMMdd.Length != 8) throw new NotImplementedException();
            Value = ayyyyMMdd;
        }

        public static yyyyMMdd Of(string ayyyyMMdd)
        {
            return new yyyyMMdd(ayyyyMMdd);
        }
        //public static yyyyMMdd OfyyMMdd(string ayyMMdd)
        //{
        //    return Of($"20{ayyMMdd}");
        //}


        public yyyyMMdd Changedd(string dd)
        {
            return Of($"{this.yyyyMM}{dd}");
        }

        public yyyyMMdd IncrementYear()
        {
            return Of($"{this.ToInt+10000}");
        }

    }
}
