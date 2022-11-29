using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Delta.Types.Dates
{
    public class Date : IComparable<Date>//,ICloneable, 
    {
        public readonly DateTime Value;

        Date(DateTime aValue)
        {
            this.Value = aValue.Date;
        }

        public static Date Of(YearMonth aYearMonth, int aDayOfMonth)
        {
            return new Date(new DateTime(aYearMonth.Year, aYearMonth.Month, aDayOfMonth));
        }

        //public static Date Of(int yyyyMM, int dayOfMonth)
        //{
        //    int yyyyMMdd = yyyyMM * 100
        //                + dayOfMonth;
        //    return Of(yyyyMMdd);
        //}
        public static bool TryParse(string? yyyyMMdd, [MaybeNullWhen(false)] out Date result)
        {
            if(yyyyMMdd==null)
            {
                result = null;
                return false;
            }
            try
            {
                result=Of(yyyyMMdd);
                return true;
            }
            catch
            {
                result=null;
                return false;
            }
        }
        public static Date Of(string yyyyMMdd)
        {
            if (yyyyMMdd.Length != 8)
            {
                throw new ArgumentException(yyyyMMdd);
            }

            return new Date(DateTime.ParseExact(yyyyMMdd, "yyyyMMdd", null));
        }
        public static Date OfyyMMdd(string yyMMdd) => Of($"20{yyMMdd}");

        public static Date Of(int yyyyMMdd)
        {
            return Of(yyyyMMdd.ToString());
        }
        public static bool TryParse([NotNullWhen(true)] int? yyyyMMdd, out Date result)
        {
            return TryParse(yyyyMMdd.ToString(),out result);
        }

        //public static Date Of(int yyyyMMdd, bool onHold)
        //{
        //    if (onHold)
        //    {
        //        if (yyyyMMdd == 0)
        //        {
        //            return new Date(DateTime.MinValue);
        //        }

        //        if (yyyyMMdd == 99999999)
        //        {
        //            return new Date(DateTime.MaxValue);
        //        }
        //    }
        //    return Of(yyyyMMdd);
        //}

        public static Date Of(DateTime aValue)
        {
            return new Date(aValue);
        }


        public Date CreateWithDiffDay(int day)
        {
            return Of(this.YearMonth, day);
        }

        //public string Value => itsValue.ToString();

        public YearMonth YearMonth => YearMonth.Of(this);
        public int ToIntInyyyyMM => YearMonth.ToIntInyyyyMM;

        public int Year => YearMonth.Year;

        public int Month => YearMonth.Month;

        public int Day => Value.Day;

        //private DateTime FirstDateOfMonthInLocalDate => new DateTime(itsValue.Year, itsValue.Month, 1);

        public Date FirstDateOfThisMonth => Of(this.YearMonth, 1);

        //private DateTime FirstDateOfNextMonth()
        //{
        //    DateTime firstDayOfMonth = new DateTime(itsValue.Year, itsValue.Month, 1);
        //    return firstDayOfMonth;
        //}

        //public DateTime LastDateOfNextMonth()
        //{
        //    int lastDayOfNextMonth = DateTime.DaysInMonth(itsValue.Year, itsValue.Month + 1);
        //    DateTime lastDateOfNextMonth = new DateTime(itsValue.Year, itsValue.Month + 1, lastDayOfNextMonth);
        //    return lastDateOfNextMonth;
        //}

        public Date LastDateOfThisMonth => Of(this.YearMonth,this.LastDayOfMonth);

        //private DateTime LastDateOfMonthInLocalDate()
        //{

        //    DateTime lastDateOfMonthInLocalDate = new DateTime(itsValue.Year, itsValue.Month, LastDayOfMonth());
        //    return lastDateOfMonthInLocalDate;
        //}

        public int LastDayOfMonth => DateTime.DaysInMonth(Value.Year, Value.Month);

        public string ToStringInyyyyMMdd => Value.ToString("yyyyMMdd");
            //if (Value.Equals(DateTime.MinValue))
            //{
            //    return "00000000";
            //}

            //if (Value.Equals(DateTime.MaxValue))
            //{
            //    return "99999999";
            //}
            

        public string ToStringInyyMMdd => Value.ToString("yyMMdd");

        //public string ToStringInyyyyMMddWithSlush()
        //{
        //    return DateTime.ParseExact(Value().ToString(), "yyyy/MM/dd h:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy/MM/dd");
        //    //return DateTime.ParseExact(this.Value().ToString(), "yyyy/MM/dd", CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("yyyy/MM/dd");
        //}

        public int ToIntInyyyyMMdd => int.Parse(ToStringInyyyyMMdd);

        public int ToIntInyyMMdd => int.Parse(ToStringInyyMMdd);

        public Date NextDate => AddDays(1);

        public Date AddDays(int value) => new(Value.AddDays(value));

        //public DateTime PrevDate()
        //{
        //    return itsValue.AddDays(-1);
        //}

        //public Date MinusDays(short daysToMinus)
        //{
        //    return new Date(itsValue.AddDays(-daysToMinus));
        //}

        //public bool IsOnOrBefore(int yyyyMMdd)
        //{
        //    return ToIntInyyyyMMdd() <= yyyyMMdd;
        //}

        //public bool IsOnOrBefore(Date target)
        //{
        //    return IsOnOrBefore(target.ToIntInyyyyMMdd());
        //}

        //public bool IsBefore(int yyyyMMdd)
        //{
        //    return ToIntInyyyyMMdd() < yyyyMMdd;
        //}

        //public bool IsBefore(Date target)
        //{
        //    return IsBefore(target.ToIntInyyyyMMdd());
        //}

        //public bool IsSameDateTo(int yyyyMMdd)
        //{
        //    return ToIntInyyyyMMdd() == yyyyMMdd;
        //}

        //public bool IsSameDateTo(Date target)
        //{
        //    return IsSameDateTo(target.ToIntInyyyyMMdd());
        //}

        //public bool IsAfter(int yyyyMMdd)
        //{
        //    return ToIntInyyyyMMdd() > yyyyMMdd;
        //}

        public bool IsAfter(Date target) => CompareTo(target) > 0;

        //public bool IsOnOrAfter(int yyyyMMdd)
        //{
        //    return ToIntInyyyyMMdd() >= yyyyMMdd;
        //}

        //public bool IsOnOrAfter(Date target)
        //{
        //    return IsOnOrAfter(target.ToIntInyyyyMMdd());
        //}


        public bool IsSameYearMonthTo(Date targetDate) => this.YearMonth.Equals(targetDate.YearMonth);

        public Date DateOfOneYearLater => new(Value.AddYears(1));

        //public object Clone()
        //{
        //    throw new NotImplementedException();
        //}

        public int CompareTo(Date? other)
        {
            return Value.CompareTo(other.Value);
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

        public int TotalDaysFrom(Date fromDate)
        {
            return ((int)(this.Value - fromDate.Value).TotalDays);
        }

        public int TotalDaysTo(Date toDate)
        {
            return ((int)(toDate.Value - this.Value).TotalDays);
        }
    }
}
