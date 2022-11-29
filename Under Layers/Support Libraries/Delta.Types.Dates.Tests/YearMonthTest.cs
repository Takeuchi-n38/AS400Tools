using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Delta.Types.Dates.Tests
{
    public class YearMonthTest
    {
        [Fact]
        public void HalfaYearLaterSameYear()
        {
            var stu = YearMonth.Of(202206);
            var actual = stu.HalfaYearLater();
            Assert.Equal(YearMonth.Of(202212), actual);
        }
        [Fact]
        public void HalfaYearLaterNextYear()
        {
            var stu = YearMonth.Of(202207);
            var actual = stu.HalfaYearLater();
            Assert.Equal(YearMonth.Of(202301), actual);
        }
        [Fact]
        public void LaterByMonthCountNextYear()
        {
            var stu = YearMonth.Of(202212);
            var actual = stu.LaterByMonthCount(1);
            Assert.Equal(YearMonth.Of(202301), actual);
        }
    }
}
