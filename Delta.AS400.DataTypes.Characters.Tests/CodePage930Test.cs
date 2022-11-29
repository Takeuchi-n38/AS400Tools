using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Delta.AS400.DataTypes.Characters
{
    public class CodePage930Test
    {
        //[Fact]
        //public void Case1()
        //{
        //    //00 45 71 0F 0F 45710
        //    var input = new byte[] { 0x00, 0x45, 0x71, 0x0F };

        //    var actual = CodePage930.ToIntegerZoneFromIntegerPack(input, 9);

        //    var expected = new byte[] { 0xF0, 0xF0, 0xF0, 0xF0, 0xF4, 0xF5, 0xF7, 0xF1, 0xF0 };

        //    Assert.Equal(expected, actual);

        //}

        [Fact]

        public void Code930()
        {
            //00 45 71 0F 0F 45710
            var input = new byte[] {
                0xF1, 0x0E, 0x42, 0xF1, 0x42, 0xF2, 0x45, 0x61, 0x45 , 0xB4,
                0x0F, 0x0E, 0x42, 0xF1, 0x42, 0xF2, 0x0F, 0x40, 0x40, 0x40};


            var actual = CodePage930.ToStringFrom(input);

            var expected = "1 １２月分  １２    ";

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void GetSet290()
        {

            Icol = "ABC";

            Assert.Equal("ABC", Icol);

        }

        [Fact]
        public void GetSet930()
        {

            Idata = "1あ2";

            Assert.Equal("1 あ 2", Idata.TrimEnd());

        }
        readonly byte[] CCSID930Bytes = Enumerable.Repeat(CodePage290.ByteOfSpace, 100).ToArray();
        public string Idata { get => CodePage930.ToStringFrom(CCSID930Bytes, 1, 100); set => CodePage930.SetBytes(value, CCSID930Bytes, 1, 100); }
        public string Icol { get => CodePage930.ToStringFrom(CCSID930Bytes, 5, 7); set => CodePage930.SetBytes(value, CCSID930Bytes, 5, 7); }

    }
}
