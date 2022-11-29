using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Delta.AS400.DataTypes.Characters
{
    public class CodePage290ComparatorTest
    {
        [Fact]
        public void Case1()
        {
            var stu = new CodePage290Comparator();

            var actual = stu.Compare("0","A")>0;

            Assert.True(actual);

        }
        [Fact]
        public void Case2()
        {
            var stu = new CodePage290Comparator();

            var actual = stu.Compare("0", "0") == 0;

            Assert.True(actual);

        }
        [Fact]
        public void Case3()
        {
            var stu = new CodePage290Comparator();

            var actual = stu.Compare("A", "0") < 0;

            Assert.True(actual);

        }
        [Fact]
        public void Case4()
        {
            var stu = new CodePage290Comparator();

            var actual = stu.Compare("Aあ", "Aい") < 0;

            Assert.True(actual);

        }
    }
}
