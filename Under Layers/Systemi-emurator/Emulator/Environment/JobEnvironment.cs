using Domain.Type.Date;
using System;

namespace Systemi_emurator.Emulator.Environment
{
    public class JobEnvironment
    {
        private Date date;
        private string user;
        private static JobEnvironment jobEnvironment = new JobEnvironment();
        private JobEnvironment()
        {
            date = Date.Of(DateTime.Now);
            user = "";
        }
        public static JobEnvironment GetInstance()
        {
            return jobEnvironment;
        }

        public static string Date_y()
        {
            return GetInstance().date.ToStringInyyyyMMddWithSlush();
        }

        public static JobEnvironment ChangeDate(Date date)
        {
            var cur = GetInstance();
            cur.date = date;
            return cur;
        }

        public int GetDATE()
        {
            return date.ToIntInyyyyMMdd();
        }

        public void SetDATE(int yyyyMMdd)
        {
            ChangeDate(Date.Of(yyyyMMdd));
        }

        public int GetUDATE()
        {
            return date.ToIntInyyMMdd();
        }

        public string GetUSER()
        {
            return user;
        }

        public void SetUSER(string user)
        {
            this.user = user;
        }
    }
}
