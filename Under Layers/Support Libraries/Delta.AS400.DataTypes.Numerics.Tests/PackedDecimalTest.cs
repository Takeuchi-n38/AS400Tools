using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Delta.AS400.DataTypes.Numerics
{
    public class PackedDecimalTest
    {
        [Fact]
        public void ToStringFromPack()
        {
            var actual = PackedDecimal.ToStringFrom(new byte[4] { 0x00, 0x00, 0x27, 0x5F, }, 1, 4, 0);
            var expected = "275";
            Assert.Equal(expected, actual);
        }



        [Fact]
        public void To4sStringFromPackTest()
        {
            var input = new byte[] { 0x01, 0x23, 0x4F };


            var actual = PackedDecimal.To4sStringFrom(input, 1, 3, 0);

            var expected = "01234";

            Assert.Equal(expected, actual);

        }

        //[Fact]
        //public void IsPositiveF()
        //{
        //    Assert.False(PackedDecimal.IsNegative(0x1F));
        //}
        //[Fact]
        //public void IsPositiveC()
        //{
        //    Assert.False(PackedDecimal.IsNegative(0x1C));
        //}
        //[Fact]
        //public void IsNegativeD()
        //{
        //    Assert.True(PackedDecimal.IsNegative(0x1D));
        //}

        [Fact]
        public void IntToDecimalFromUnsigned()
        {
            var packs = new byte[4] { 0x12, 0x34, 0x56, 0x7F, };
            var expected = 1234567;
            var actual = PackedDecimal.ToDecimalFrom(packs,0);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void IntToDecimalFromPositive()
        {
            var packs = new byte[4] { 0x12, 0x34, 0x56, 0x7C, };
            var expected = 1234567;
            var actual = PackedDecimal.ToDecimalFrom(packs, 0);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void IntToDecimalFromNegative()
        {
            var packs = new byte[4] { 0x12, 0x34, 0x56, 0x7D, };
            var expected = -1234567;
            var actual = PackedDecimal.ToDecimalFrom(packs, 0);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void DecimalToDecimalFromUnsigned()
        {
            var packs = new byte[4] { 0x12, 0x34, 0x56, 0x7F, };
            var expected = 12345.67M;
            var actual = PackedDecimal.ToDecimalFrom(packs, 2);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void DecimalIntToDecimalFromPositive()
        {
            var packs = new byte[4] { 0x12, 0x34, 0x56, 0x7C, };
            var expected = 12345.67M;
            var actual = PackedDecimal.ToDecimalFrom(packs, 2);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void DecimalIntToDecimalFromNegative()
        {
            var packs = new byte[4] { 0x12, 0x34, 0x56, 0x7D, };
            var expected = -12345.67M;
            var actual = PackedDecimal.ToDecimalFrom(packs, 2);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void IntToToUnsignedPackedBytesFrom()
        {
            var val = 1234567;
            var expected = new byte[4] { 0x12, 0x34, 0x56, 0x7F, };
            var actual = PackedDecimal.ToUnsignedBytesFrom(val,4, 0);
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void UnsignedZero()
        {
            var actual = PackedDecimal.ToUnsignedBytesFrom(0, 2, 0);
            var expected = new byte[2] { 0x00, 0x0F, };
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SignedOne()
        {
            var actual = PackedDecimal.ToSignedBytesFrom(1, 2, 0);
            var expected = new byte[2] { 0x00, 0x1C, };
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void SignedNegativeOne()
        {
            var actual = PackedDecimal.ToSignedBytesFrom(-1, 2, 0);
            var expected = new byte[2] { 0x00, 0x1D, };
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SignedZero()
        {
            var actual = PackedDecimal.ToSignedBytesFrom(0, 2, 0);
            var expected = new byte[2] { 0x00, 0x0C, };
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void ZoneToSignedInt()
        {
            forPackToSignedIntTest = -210620;

            Assert.Equal(-210620, forPackToSignedIntTest);
        }

        [Fact]
        public void ZoneToSignedDec()
        {
            forPackToSignedDecTest = -2106.20M;

            Assert.Equal(-2106.20M, forPackToSignedDecTest);
        }
        [Fact]
        public void ZoneToInt()
        {
            forPackToIntTest = 210620;

            Assert.Equal(210620, forPackToIntTest);
        }

        [Fact]
        public void ZoneToDec()
        {
            forPackToDecTest = 2106.20M;

            Assert.Equal(2106.20M, forPackToDecTest);
        }

        byte[] dsValue21CCSID930Bytes = Enumerable.Repeat((byte)0xF0, 4).ToArray();
        int forPackToIntTest
        {
            get
            {
                return (int)PackedDecimal.ToDecimalFrom(dsValue21CCSID930Bytes, 1, 4);
            }
            set
            {
                PackedDecimal.SetUnsignedBytes(value, dsValue21CCSID930Bytes,1, 4);
            }
        }
        decimal forPackToDecTest
        {
            get
            {
                return PackedDecimal.ToDecimalFrom(dsValue21CCSID930Bytes, 1, 4, 2);
            }
            set
            {
                PackedDecimal.SetUnsignedBytes(value, dsValue21CCSID930Bytes, 1,4, 2);
            }
        }
        int forPackToSignedIntTest
        {
            get
            {
                return (int)PackedDecimal.ToDecimalFrom(dsValue21CCSID930Bytes, 1, 4);
            }
            set
            {
                PackedDecimal.SetUnsignedBytes(value, dsValue21CCSID930Bytes, 1, 4);
            }
        }
        decimal forPackToSignedDecTest
        {
            get
            {
                return PackedDecimal.ToDecimalFrom(dsValue21CCSID930Bytes, 1, 4, 2);
            }
            set
            {
                PackedDecimal.SetUnsignedBytes(value, dsValue21CCSID930Bytes, 1, 4, 2);
            }
        }
    }
}
