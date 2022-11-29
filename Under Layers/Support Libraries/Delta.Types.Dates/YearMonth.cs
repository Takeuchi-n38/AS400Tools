using System;
using System.ComponentModel;
using System.Globalization;

namespace Delta.Types.Dates
{
    public class YearMonth
    {
        public readonly int Month;
        public readonly int Year;
       // private readonly YearMonth itsValue;

        //public YearMonth Value()
        //{
        //    return itsValue;
        //}
        YearMonth(int aYear, int aMonth)
        {
            this.Year = aYear;
            this.Month = aMonth;
        }

        //private YearMonth(YearMonth aYearMonth)
        //{
        //    itsValue = aYearMonth;
        //}

        //First day
        //public static YearMonth Of(string yyyyMM)
        //{
        //    var stryyyyMMdd = yyyyMM + "01";
        //    if (stryyyyMMdd.Length != 8)
        //    {
        //        throw new ArgumentOutOfRangeException(stryyyyMMdd);
        //    }

        //    DateTime aValue;
        //    try
        //    {
        //        CultureInfo provider = CultureInfo.InvariantCulture;

        //        aValue = DateTime.ParseExact(stryyyyMMdd, "yyyyMMdd", provider);
        //    }
        //    catch (Exception)
        //    {
        //        throw new InvalidEnumArgumentException(stryyyyMMdd);
        //    }

        //    return Of(aValue);
        //}
        public static YearMonth Of(int yyyyMM)
        {
            var year=yyyyMM/100;
            var month= yyyyMM - year*100;
            return new YearMonth(year, month);
        }

        public static YearMonth Of(DateTime aDate)
        {
            return new YearMonth(aDate.Year, aDate.Month);
        }

        public static YearMonth Of(Date aDate)
        {
            return Of(aDate.Value);
        }

        //public int MonthValue()
        //{
        //    return Value().month;
        //}

        //public int YearValue()
        //{
        //    return Value().year;
        //}

        //public bool IsBefore(YearMonth target)
        //{
        //    return Value().compareTo(target.Value()) < 0;
        //}

        //public bool IsSameYearMonthTo(YearMonth target)
        //{
        //    return Value().equals(target.Value());
        //}

        public int ToIntInyyyyMM => Year * 100 + Month;
        public string ToStringInyyyyMM => ToIntInyyyyMM.ToString();

        //public Date Start()
        //{
        //    var dateStart = new DateTime(Value().year, Value().month, 1);
        //    return new Date(dateStart);
        //}

        //public Date End()
        //{
        //    var lastDayOfMonth = DateTime.DaysInMonth(Value().year, Value().month);
        //    var dateEnd = new DateTime(Value().year, Value().month, lastDayOfMonth);
        //    return new Date(dateEnd);
        //}

        //public DateRange DateRange()
        //{
        //    return Dates.DateRange.Of(Start(), End());
        //}

        //public YearMonth next()
        //{
        //    int month = Value().month + 1;
        //    YearMonth yearMonth = new YearMonth(Value().year, month);
        //    return yearMonth;
        //}
        public YearMonth HalfaYearLater()
        {
            return LaterByMonthCount(6);
        }
        public YearMonth LaterByMonthCount(int monthCount)
        {
            int year = this.Month + monthCount > 12 ? Year + 1 : Year;
            int month = (this.Month + monthCount) % 12;

            return new YearMonth(year, month);
        }

        //public int compareTo(YearMonth other)
        //{
        //    int cmp = year - other.year;
        //    if (cmp == 0)
        //    {
        //        cmp = month - other.month;
        //    }
        //    return cmp;
        //}


        #region "equals"

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

            var target = (YearMonth)obj;

            if (!target.Year.Equals(Year)) return false;

            if (target.Month != Month) return false;

            return true;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Year.GetHashCode() ^ Month.GetHashCode();
        }

        #endregion

    }
}
