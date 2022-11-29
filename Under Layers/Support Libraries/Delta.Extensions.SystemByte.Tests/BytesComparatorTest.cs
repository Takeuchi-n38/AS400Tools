using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Delta.AS400.DataTypes
{
    public class BytesComparatorTest
    {
        [Fact]
        public void xLessy()
        {
            var actual = BytesComparator.Instance.Compare(new byte[4] { 0x00, 0x00, 0x26, 0x5F, }, new byte[4] { 0x00, 0x00, 0x27, 0x5F, });
            var expected = -1;
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void xEqualy()
        {
            var actual = BytesComparator.Instance.Compare(new byte[4] { 0x00, 0x00, 0x27, 0x5F, }, new byte[4] { 0x00, 0x00, 0x27, 0x5F, });
            var expected = 0;
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void xThany()
        {
            var actual = BytesComparator.Instance.Compare(new byte[4] { 0x00, 0x00, 0x28, 0x5F, }, new byte[4] { 0x00, 0x00, 0x27, 0x5F, });
            var expected = 1;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToBytesFrom()
        {
            var input = "ABCD";
            var actual = BytesExtensions.ToBytesFrom(input);
            var expected = new byte[2] { 0xAB, 0xCD, };
            Assert.Equal(expected, actual);
        }
    }
}
