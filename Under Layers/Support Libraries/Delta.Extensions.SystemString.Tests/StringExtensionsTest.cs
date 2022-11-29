using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Delta.Extensions.SystemString
{
    public class StringExtensionsTest
    {
        [Fact]
        public void Test1()
        {
            var stu="0000";
            var actual = stu.ReplacePart(1,"12");
            Assert.Equal("0120",actual);
        }
    }
}
