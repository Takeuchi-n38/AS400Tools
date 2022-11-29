using Delta.Types.Fundamentals;
using System.Collections.Generic;

namespace Delta.Types.Dates
{
    public class DateList : FirstClassList<Date>
    {
        public DateList() : base(new List<Date>())
        {
        }
        public static DateList Of()
        {
            return new DateList();
        }
        public static DateList Of(Date firstItem)
        {
            var instance = Of();
            instance.Add(firstItem);
            return instance;
        }
        public int IndexOf(int yyyyMMdd)
        {
            return IndexOf(Date.Of(yyyyMMdd));
        }
        private List<Date> SortedList()
        {
            var sorting = List();
            sorting.Sort();
            return sorting;
        }
        private List<Date> ReversedSortedList()
        {
            var reversing = SortedList();
            reversing.Reverse();
            return reversing;
        }

        //public Optional<Date> nearestDateBefore(Date targetDate)
        //{
        //    foreach (var cur in reversedSortedList())
        //    {
        //        if (cur.isBefore(targetDate))
        //        {
        //            return Optional.of(cur);
        //        }
        //    }
        //    return Optional.empty();
        //}

        //public Optional<Date> onOrNearestDateBefore(Date targetDate)
        //{
        //    if (itsList().contains(targetDate))
        //    {
        //        return Optional.of(targetDate);
        //    }
        //    return nearestDateBefore(targetDate);
        //}

        //public Optional<Date> nearestDateAfter(Date targetDate)
        //{
        //    for (var cur:sortedList())
        //    {
        //        if (cur.isAfter(targetDate))
        //        {
        //            return Optional.of(cur);
        //        }
        //    }
        //    return Optional.empty();
        //}

        //public Optional<Date> onOrNearestDateAfter(Date targetDate)
        //{
        //    if (itsList().contains(targetDate))
        //    {
        //        return Optional.of(targetDate);
        //    }
        //    return nearestDateAfter(targetDate);
        //}

        //public Dates overlappingPart(DateRange dateRange)
        //{
        //    var overlappingPart = Dates.of();
        //    sortedList().Select()
        //    .filter(dateRange::contains)
        //    .forEach(overlappingPart::add);
        //    return overlappingPart;
        //}

        //public Dates limit(long maxSize)
        //{
        //    var overlappingPart = Dates.of();
        //    sortedList().stream().limit(maxSize).forEach(overlappingPart::add);
        //    return overlappingPart;
        //}

        //public Dates remove(Dates dates)
        //{
        //    var removed = Dates.of();
        //    sortedList().stream()
        //    .filter(Predicate.not(dates::contains))
        //    .forEach(removed::add);
        //    return removed;
        //}

        //public Date previousDateWithSkippingContained(int previousCount, Date baseDate)
        //{
        //    var cur = baseDate;
        //    for (var i = 1; i <= previousCount; i++)
        //    {
        //        cur = cur.prevDate();
        //        while (contains(cur))
        //        {
        //            cur = cur.prevDate();
        //        }
        //    }
        //    return cur;
        //}
    }
}
