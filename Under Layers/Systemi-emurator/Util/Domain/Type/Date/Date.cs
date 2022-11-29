using System;
using System.ComponentModel;
using System.Globalization;

namespace Domain.Type.Date
{
    public class Date : ICloneable, IComparable<Date>
    {
        private readonly DateTime itsValue;

        public Date(DateTime aValue)
        {
            itsValue = aValue;
        }

        public static Date Of(YearMonth yearMonth, int dayOfMonth)
        {
            return new Date(DateTime.Parse(yearMonth.YearValue().ToString() + yearMonth.MonthValue().ToString() + dayOfMonth.ToString()));
        }

        public static Date Of(int yyyyMM, int dayOfMonth)
        {
            int yyyyMMdd = ((yyyyMM * 100)
                        + dayOfMonth);
            return Date.Of(yyyyMMdd);
        }

        public static Date Of(string yyyyMMdd)
        {
            if ((yyyyMMdd.Length != 8))
            {
                throw new InvalidEnumArgumentException(yyyyMMdd);
            }

            DateTime aValue;
            try
            {
                //ParseExact(time, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None)
                aValue = DateTime.ParseExact(yyyyMMdd, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
            }
            catch (Exception)
            {
                throw new InvalidEnumArgumentException(yyyyMMdd);
            }

            return new Date(aValue);
        }

        public static Date Of(int yyyyMMdd)
        {
            return Date.Of(yyyyMMdd.ToString());
        }

        public static Date Of(int yyyyMMdd, bool onHold)
        {
            if (onHold)
            {
                if ((yyyyMMdd == 0))
                {
                    return new Date(DateTime.MinValue);
                }

                if ((yyyyMMdd == 99999999))
                {
                    return new Date(DateTime.MaxValue);
                }
            }
            return Date.Of(yyyyMMdd);
        }

        public static Date Of(DateTime aValue)
        {
            return new Date(aValue);
        }

        public string Value()
        {
            return itsValue.ToString();
        }

        public YearMonth YearMonth()
        {
            return Type.Date.YearMonth.Of(Value());
        }

        private DateTime FirstDateOfMonthInLocalDate()
        {
            DateTime firstDayOfMonth = new DateTime(itsValue.Year, itsValue.Month, 1);
            return firstDayOfMonth;
        }

        public DateTime Firstdateofmonth()
        {
            return FirstDateOfMonthInLocalDate();
        }

        private DateTime FirstDateOfNextMonth()
        {
            DateTime firstDayOfMonth = new DateTime(itsValue.Year, itsValue.Month, 1);
            return firstDayOfMonth;
        }

        public DateTime LastDateOfNextMonth()
        {
            int lastDayOfNextMonth = DateTime.DaysInMonth(itsValue.Year, itsValue.Month + 1);
            DateTime lastDateOfNextMonth = new DateTime(itsValue.Year, itsValue.Month + 1, lastDayOfNextMonth);
            return lastDateOfNextMonth;
        }

        public DateTime LastDateOfMonth()
        {
            return LastDateOfMonthInLocalDate();
        }

        private DateTime LastDateOfMonthInLocalDate()
        {

            DateTime lastDateOfMonthInLocalDate = new DateTime(itsValue.Year, itsValue.Month, LastDayOfMonth());
            return lastDateOfMonthInLocalDate;
        }

        private int LastDayOfMonth()
        {
            int lastDayOfMonth = DateTime.DaysInMonth(itsValue.Year, itsValue.Month);
            return lastDayOfMonth;
        }

        public string ToStringInyyyyMMdd()
        {
            if (Value().Equals(DateTime.MinValue))
            {
                return "00000000";
            }

            if (Value().Equals(DateTime.MaxValue))
            {
                return "99999999";
            }
            return Convert.ToDateTime(Value()).ToString("yyyyMMdd");
        }

        public string ToStringInyyMMdd()
        {
            if (Value().Equals(DateTime.MinValue))
            {
                return "00000000";
            }

            if (Value().Equals(DateTime.MaxValue))
            {
                return "99999999";
            }

            return DateTime.ParseExact(Value(), "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None).ToString();
        }

        public string ToStringInyyyyMMddWithSlush()
        {
            return DateTime.ParseExact(Value().ToString(), "yyyy/MM/dd h:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
            //return DateTime.ParseExact(this.Value().ToString(), "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("yyyy/MM/dd");
        }

        public int ToIntInyyyyMMdd()
        {
            return int.Parse(ToStringInyyyyMMdd());
        }

        public int ToIntInyyMMdd()
        {
            return int.Parse(ToStringInyyMMdd());
        }

        public Date NextDate()
        {
            return new Date(itsValue.AddDays(1));
        }

        public Date PlusDays(int daysToAdd)
        {
            return new Date(itsValue.AddDays(daysToAdd));
        }

        public DateTime PrevDate()
        {
            return itsValue.AddDays(-1);
        }

        public Date MinusDays(short daysToMinus)
        {
            return new Date(itsValue.AddDays(-daysToMinus));
        }

        public bool IsOnOrBefore(int yyyyMMdd)
        {
            return (ToIntInyyyyMMdd() <= yyyyMMdd);
        }

        public bool IsOnOrBefore(Date target)
        {
            return IsOnOrBefore(target.ToIntInyyyyMMdd());
        }

        public bool IsBefore(int yyyyMMdd)
        {
            return (ToIntInyyyyMMdd() < yyyyMMdd);
        }

        public bool IsBefore(Date target)
        {
            return IsBefore(target.ToIntInyyyyMMdd());
        }

        public bool IsSameDateTo(int yyyyMMdd)
        {
            return (ToIntInyyyyMMdd() == yyyyMMdd);
        }

        public bool IsSameDateTo(Date target)
        {
            return IsSameDateTo(target.ToIntInyyyyMMdd());
        }

        public bool IsAfter(int yyyyMMdd)
        {
            return (ToIntInyyyyMMdd() > yyyyMMdd);
        }

        public bool IsAfter(Date target)
        {
            return IsAfter(target.ToIntInyyyyMMdd());
        }

        public bool IsOnOrAfter(int yyyyMMdd)
        {
            return (ToIntInyyyyMMdd() >= yyyyMMdd);
        }

        public bool IsOnOrAfter(Date target)
        {
            return IsOnOrAfter(target.ToIntInyyyyMMdd());
        }

        public int Day()
        {
            return itsValue.Day;
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public int CompareTo(Date other)
        {
            throw new NotImplementedException();
        }

        //TODO:[Override()]
        //public int hashCode()
        //{
        //    int prime = 31;
        //    int result = 1;
        //    result = ((prime * result)
        //                + (this.itsValue == null));
        //   // Warning!!!, inline IF is not supported ?
        //    return result;
        //}

        //[Override()]
        //public bool equals(Object obj)
        //{
        //    if ((this == obj))
        //    {
        //        return true;
        //    }

        //    if ((obj == null))
        //    {
        //        return false;
        //    }

        //    if ((getClass() != obj.getClass()))
        //    {
        //        return false;
        //    }

        //    Date other = ((Date)(obj));
        //    if ((this.itsValue == null))
        //    {
        //        if ((other.itsValue != null))
        //        {
        //            return false;
        //        }

        //    }
        //    else if (!this.itsValue.equals(other.itsValue))
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //[Override()]
        //public int compareTo(Date o)
        //{
        //    return this.value().compareTo(o.value());
        //}

        //[Override]
        //public Date clone()
        //{
        //    return new Date(this.value());
        //}

        //public static Date before(Date target1, Date target2)
        //{
        //    if ((target1.compareTo(target2) <= 0))
        //    {
        //        return target1;
        //    }

        //    return target2;
        //}

        //public static Date after(Date target1, Date target2)
        //{
        //    if ((target1.compareTo(target2) <= 0))
        //    {
        //        return target2;
        //    }

        //    return target2;
        //}
    }
}
