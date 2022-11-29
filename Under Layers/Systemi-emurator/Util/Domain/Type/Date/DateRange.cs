using Domain.Type.Fundamental;

namespace Domain.Type.Date
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

        public Dates Dates()
        {
            var dates = Type.Date.Dates.Of();
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
            if (this.From().IsAfter(target.To())) return false;
            if (this.To().IsBefore(target.From())) return false;
            return true;
        }

        public bool Contains(Date date)
        {
            if (this.From().IsAfter(date)) return false;
            if (this.To().IsBefore(date)) return false;
            return true;
        }
    }
}
