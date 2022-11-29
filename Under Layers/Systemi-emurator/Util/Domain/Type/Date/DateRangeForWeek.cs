namespace Domain.Type.Date
{
    public class DateRangeForWeek : DateRange
    {
        public DateRangeForWeek(Date aFrom, Date aTo) : base(aFrom, aTo)
        {
        }
        public static DateRangeForWeek of(Date aFromDate)
        {
            var toDate = aFromDate.PlusDays(6);
            return new DateRangeForWeek(aFromDate, toDate);
        }
        public DateRangeForWeek nextWeek()
        {
            var fromDate = this.To().NextDate();
            return of(fromDate);
        }
    }
}
