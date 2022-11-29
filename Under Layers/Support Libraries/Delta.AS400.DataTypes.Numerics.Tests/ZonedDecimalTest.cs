using System;
using System.Linq;
using Xunit;

namespace Delta.AS400.DataTypes.Numerics
{
    public class ZonedDecimalTest
    {
        [Fact]
        public void To4sStringFromZoneTest()
        {
            var input = new byte[] { 0xF0, 0xF1, 0xF2, 0xF3 };


            var actual = ZonedDecimal.To4sStringFrom(input, 1, 4, 0);

            var expected = "0123";

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void TestZero()
        {
            var actual = ZonedDecimal.ToUnsignedBytesFrom(0,5);
            var expected = new byte[5] { 0xF0, 0xF0, 0xF0, 0xF0, 0xF0, };
            Assert.Equal(expected,actual);
        }
        [Fact]
        public void ZoneToPositiveInt()
        {
            forZoneToIntTest = 210620;

            Assert.Equal(210620, forZoneToIntTest);
        }
        [Fact]
        public void ZoneToInt()
        {
            forZoneToIntTest = -210620;

            Assert.Equal(-210620, forZoneToIntTest);
        }
        [Fact]
        public void ZoneToPositiveDec()
        {
            forZoneToDecTest = 2106.20M;

            Assert.Equal(2106.20M, forZoneToDecTest);
        }
        [Fact]
        public void ZoneToDec()
        {
            forZoneToDecTest = -2106.20M;

            Assert.Equal(-2106.20M, forZoneToDecTest);
        }

        [Fact]
        public void SignedOne()
        {
            var actual = ZonedDecimal.ToSignedBytesFrom(1, 2);
            var expected = new byte[2] { 0xF0, 0xC1, };
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SignedNegativeOne()
        {
            var actual = ZonedDecimal.ToSignedBytesFrom(-1, 2);
            var expected = new byte[2] { 0xF0, 0xD1, };
            Assert.Equal(expected, actual);
        }

        //[Fact]
        //public void IsPositiveF()
        //{
        //    Assert.False(ZonedDecimal.IsNegative(0xF1));
        //}
        //[Fact]
        //public void IsPositiveC()
        //{
        //    Assert.False(ZonedDecimal.IsNegative(0xC1));
        //}
        //[Fact]
        //public void IsNegativeD()
        //{
        //    Assert.True(ZonedDecimal.IsNegative(0xD1));
        //}

        byte[] dsValue21CCSID930Bytes = Enumerable.Repeat((byte)0xF0, 8).ToArray();
        //0022      I                                        1   80W\DATE            
        int forZoneToIntTest
        {
            get
            {
                return (int)ZonedDecimal.ToDecimalFrom(dsValue21CCSID930Bytes, 1, 8);
            }
            set
            {
                BytesExtensions.SetBytes(ZonedDecimal.ToUnsignedBytesFrom(value, 8-1+1), dsValue21CCSID930Bytes, 1 - 1);
            }
        }
        decimal forZoneToDecTest
        {
            get
            {
                return ZonedDecimal.ToDecimalFrom(dsValue21CCSID930Bytes, 1, 8, 2);
            }
            set
            {
                BytesExtensions.SetBytes(ZonedDecimal.ToUnsignedBytesFrom(value,8-1+1,2), dsValue21CCSID930Bytes, 1 - 1);
            }
        }

        //[Fact]
        //public void X()
        //{
        //    int input = -3;
        //    uint actual =(uint)input;
        //    uint expected = 3;
        //    Assert.Equal(expected, actual);
        //}
    }
}
