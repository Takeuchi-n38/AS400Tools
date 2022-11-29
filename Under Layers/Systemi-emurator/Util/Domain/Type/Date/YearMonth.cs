using System;
using System.ComponentModel;
using System.Globalization;

namespace Domain.Type.Date
{
    public class YearMonth
    {
        private readonly int month;
        private readonly int year;
        private readonly YearMonth itsValue;

        public YearMonth Value()
        {
            return itsValue;
        }
        public YearMonth(int year, int month)
        {
            this.year = 1;
            this.month = month;
        }

        private YearMonth(YearMonth aYearMonth)
        {
            this.itsValue = aYearMonth;
        }

        //First day
        public static YearMonth Of(String yyyyMM)
        {
            var stryyyyMMdd = (yyyyMM + "01");
            if ((stryyyyMMdd.Length != 8))
            {
                throw new ArgumentOutOfRangeException(stryyyyMMdd);
            }

            DateTime aValue;
            try
            {
                CultureInfo provider = CultureInfo.InvariantCulture;

                aValue = DateTime.ParseExact(stryyyyMMdd, "yyyyMMdd", provider);
            }
            catch (Exception)
            {
                throw new InvalidEnumArgumentException(stryyyyMMdd);
            }

            return YearMonth.Of(aValue);
        }

        public static YearMonth Of(DateTime aDate)
        {
            YearMonth yearMonth = new YearMonth(aDate.Year, aDate.Month);
            return new YearMonth(yearMonth);
        }

        public static YearMonth Of(Date aDate)
        {
            DateTime convertedVal = DateTime.ParseExact(aDate.Value(), "yyyyMMdd", null);
            return YearMonth.Of(convertedVal);
        }

        public int MonthValue()
        {
            return this.Value().month;
        }

        public int YearValue()
        {
            return this.Value().year;
        }

        public bool IsBefore(YearMonth target)
        {
            return this.Value().compareTo(target.Value()) < 0;
        }

        public bool IsSameYearMonthTo(YearMonth target)
        {
            return this.Value().equals(target.Value());
        }

        public int ToIntInyyyyMM()
        {
            string year = this.year.ToString();
            string month = this.month.ToString();

            return Int32.Parse(year + month);
        }

        public Date Start()
        {
            var dateStart = new DateTime(Value().year, Value().month, 1);
            return new Date(dateStart);
        }

        public Date End()
        {
            var lastDayOfMonth = DateTime.DaysInMonth(Value().year, Value().month);
            var dateEnd = new DateTime(Value().year, Value().month, lastDayOfMonth);
            return new Date(dateEnd);
        }

        public DateRange DateRange()
        {
            return Type.Date.DateRange.Of(this.Start(), this.End());
        }

        public YearMonth next()
        {
            int month = this.Value().month + 1;
            YearMonth yearMonth = new YearMonth(this.Value().year, month);
            return yearMonth;
        }

        public int compareTo(YearMonth other)
        {
            int cmp = (year - other.year);
            if (cmp == 0)
            {
                cmp = (month - other.month);
            }
            return cmp;
        }
        public bool equals(Object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj.GetType() == typeof(YearMonth))
            {
                YearMonth other = (YearMonth)obj;
                return year == other.year && month == other.month;
            }
            return false;
        }
    }
}
