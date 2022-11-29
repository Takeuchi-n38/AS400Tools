using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Delta.AS400.Environments
{
    public class JobAttribute
    {
        DateTime? udate;

        public JobAttribute(DateTime? udate)
        {
            this.udate = udate;
        }

        public static JobAttribute Of(DateTime? udate) => new JobAttribute(udate);

        public static JobAttribute Of() => new JobAttribute(null);

        public string Job { get; set; } = string.Empty;

        public string User { get; set; } = string.Empty;

        public string Nbr { get; set; } = string.Empty;

        public virtual DateTime Udate => udate.HasValue ? new DateTime(udate.Value.Year, udate.Value.Month, udate.Value.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond) : DateTime.Now;

        public int Xdate => int.Parse(Udate.ToString("yyyyMMdd"));

        //ジョブによって使用される8個のジョブ・スイッチの値を受け取るCL変数の名前を指定します。ジョブ・スイッチは，各桁が対応するスイッチの値として1または0を指定する単一の8文字の値として検索されます。このCL変数は，最小の長さが8文字の文字変数でなければなりません。
        char[] Ux = Enumerable.Range(1, 8).Select(i => '0').ToArray();

        public string Sws { get => new string(Ux); set { Ux = value.ToArray(); } }

        public string InU1 { get => Ux[0].ToString(); set => Ux[0] = value[0]; }
        public string InU2 { get => Ux[1].ToString(); set => Ux[1] = value[0]; }
        public string InU3 { get => Ux[2].ToString(); set => Ux[2] = value[0]; }
        public string InU4 { get => Ux[3].ToString(); set => Ux[3] = value[0]; }
        public string InU5 { get => Ux[4].ToString(); set => Ux[4] = value[0]; }
        public string InU6 { get => Ux[5].ToString(); set => Ux[5] = value[0]; }
        public string InU7 { get => Ux[6].ToString(); set => Ux[6] = value[0]; }
        public string InU8 { get => Ux[7].ToString(); set => Ux[7] = value[0]; }

    }
}
