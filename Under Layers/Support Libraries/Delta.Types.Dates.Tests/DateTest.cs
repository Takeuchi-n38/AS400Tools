using System;
using Xunit;

namespace Delta.Types.Dates.Tests
{
    public class DateTest
    {
        [Fact]
        public void TotalDaysFrom()
        {
            var stu = Date.OfyyMMdd("210303");
            var fromDate = Date.OfyyMMdd("210227");
            Assert.Equal(4,stu.TotalDaysFrom(fromDate));
        }

        [Fact]
        public void TotalDaysTo()
        {
            var stu = Date.OfyyMMdd("210227");
            var toDate = Date.OfyyMMdd("210303");
            Assert.Equal(4, stu.TotalDaysTo(toDate));
        }

        [Fact]
        public void NegativeTotalDaysFrom()
        {
            var stu = Date.OfyyMMdd("210227");
            var fromDate = Date.OfyyMMdd("210303");
            Assert.Equal(-4, stu.TotalDaysFrom(fromDate));
        }

        [Fact]
        public void NegativeTotalDaysTo()
        {
            var stu = Date.OfyyMMdd("210303");
            var toDate = Date.OfyyMMdd("210227");
            Assert.Equal(-4, stu.TotalDaysTo(toDate));
        }

        [Fact]
        public void NegativeAddDays()
        {
            var stu = Date.OfyyMMdd("210303");
            Assert.Equal(Date.OfyyMMdd("210302").Value, stu.AddDays(-1).Value);
        }


        [Fact]
        public void TryParse20010229()
        {
            var succeed = Date.TryParse(20010229,out Date actual);

            Assert.False(succeed);
        }

        [Fact]
        public void TryParse20000228()
        {
            var succeed = Date.TryParse(20000228, out Date actual);

            Assert.True(succeed);
            var expected = Date.Of(20000228);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TryParse20000229()
        {
            var succeed = Date.TryParse(20000229, out Date actual);

            Assert.True(succeed);
            var expected = Date.Of(20000229);

            Assert.Equal(expected, actual);
        }
    }
}
