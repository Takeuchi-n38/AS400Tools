using Delta.Types.Fundamentals;

namespace Delta.Types.Dates
{
    public class DateRange : Range<Date>
    {
        public DateRange(Date aFrom, Date aTo) : base(aFrom, aTo)
        {
        }

        public static DateRange Of(Date aFrom, Date aTo)
        {
            return new DateRange(aFrom, aTo);
        }

        public DateList Dates()
        {
            var dates = DateList.Of();
            var cur = From();
            while (cur.IsOnOrBefore(To()))
            {
                dates.Add(cur);
                cur = cur.NextDate();
            }
            return dates;
        }

        public bool IsOverlapping(DateRange target)
        {
            if (From().IsAfter(target.To())) return false;
            if (To().IsBefore(target.From())) return false;
            return true;
        }

        public bool Contains(Date date)
        {
            if (From().IsAfter(date)) return false;
            if (To().IsBefore(date)) return false;
            return true;
        }
    }
}
