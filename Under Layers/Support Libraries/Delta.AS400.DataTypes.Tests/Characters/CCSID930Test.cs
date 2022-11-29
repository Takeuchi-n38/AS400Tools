using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Delta.AS400.DataTypes.Characters
{
    public class CCSID930Test
    {

        [Fact]
        public void Case1()
        {
            //00 45 71 0F 0F 45710
            var input = new byte[] { 0x00, 0x45, 0x71, 0x0F };

            var actual = CCSID930.ToIntegerZoneFromIntegerPack(input, 9);

            var expected = new byte[] { 0xF0, 0xF0, 0xF0, 0xF0, 0xF4, 0xF5, 0xF7, 0xF1, 0xF0 };

            Assert.Equal(expected, actual);

        }

        [Fact]

        public void Code930()
        {
            //00 45 71 0F 0F 45710
            var input = new byte[] {
                0xF1, 0x0E, 0x42, 0xF1, 0x42, 0xF2, 0x45, 0x61, 0x45 , 0xB4,
                0x0F, 0x0E, 0x42, 0xF1, 0x42, 0xF2, 0x0F, 0x40, 0x40, 0x40};


            var actual = CCSID930.ToStringFrom(input);

            var expected = "1 １２月分  １２    ";

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void ToCCSID930CharBytesFromNull()
        {
            string input = null;
            var actual = new byte[3];

            CCSID930.ToCCSID930CharBytesFrom(input, actual, 1, 3);

            var expected = new byte[3]{ 0x40, 0x40, 0x40, };

            Assert.Equal(expected, actual);

        }


        //[Fact]
        //public void ToZoneBytesFrom()
        //{
        //    var input = 21113;
        //    var actual = new byte[5];

        //    CCSID930.ToZoneBytesFrom(input, actual, 1, 5);

        //    var expected = new byte[5] { 0xF2, 0xF1, 0xF1, 0xF1, 0xF3, };

        //    Assert.Equal(expected, actual);

        //}

        //[Fact]
        //public void ToDecimalFromZone()
        //{
        //    var input = new byte[5] { 0xF2, 0xF1, 0xF1, 0xF1, 0xF3, };

        //    var actual = CCSID930.ToDecimalFromZone(input, 1, 5);

        //    var expected = 21113;

        //    Assert.Equal(expected, actual);

        //}
    }
}
