using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Delta.AS400.DataTypes.Characters
{
    public class CodePage290Test
    {
        [Fact]

        public void Encodable()
        {
            //00 45 71 0F 0F 45710
            //var input = new byte[] {
            //    0xF1, 0x0E, 0x42, 0xF1, 0x42, 0xF2, 0x45, 0x61, 0x45 , 0xB4,
            //    0x0F, 0x0E, 0x42, 0xF1, 0x42, 0xF2, 0x0F, 0x40, 0x40, 0x40};

            var input = "1 １２月分  １２    ";
            var actual = CodePage290.Encodable(input);

            Assert.False(actual);

        }

        [Fact]
        public void TryParse()
        {
            var parsed = CodePage290.TryParse(' ', out byte actual);
            Assert.Equal(0x40, actual);
        }

        [Fact]
        public void TryParse0x41()
        {
            var parsed = CodePage290.TryParse('｡', out byte actual);
            Assert.Equal(0x41, actual);
        }
        [Fact]
        public void TryParse0x42()
        {
            var parsed = CodePage290.TryParse('｢', out byte actual);
            Assert.Equal(0x42, actual);
        }
        [Fact]
        public void TryParse0x43()
        {
            var parsed = CodePage290.TryParse('｣', out byte actual);
            Assert.Equal(0x43, actual);
        }
        [Fact]
        public void TryParse0x44()
        {
            var parsed = CodePage290.TryParse('､', out byte actual);
            Assert.Equal(0x44, actual);
        }
        [Fact]
        public void TryParse0x46()
        {
            var parsed = CodePage290.TryParse('ｦ', out byte actual);
            Assert.Equal(0x46, actual);
        }
        [Fact]
        public void TryParse0x47()
        {
            var parsed = CodePage290.TryParse('ｧ', out byte actual);
            Assert.Equal(0x47, actual);
        }
        [Fact]
        public void TryParse0x48()
        {
            var parsed = CodePage290.TryParse('ｨ', out byte actual);
            Assert.Equal(0x48, actual);
        }
        [Fact]
        public void TryParse0x49()
        {
            var parsed = CodePage290.TryParse('ｩ', out byte actual);
            Assert.Equal(0x49, actual);
        }
        [Fact]
        public void TryParse0x4B()
        {
            var parsed = CodePage290.TryParse('.', out byte actual);
            Assert.Equal(0x4B, actual);
        }
        [Fact]
        public void TryParse0x4C()
        {
            var parsed = CodePage290.TryParse('<', out byte actual);
            Assert.Equal(0x4C, actual);
        }
        [Fact]
        public void TryParse0x4D()
        {
            var parsed = CodePage290.TryParse('(', out byte actual);
            Assert.Equal(0x4D, actual);
        }
        [Fact]
        public void TryParse0x4E()
        {
            var parsed = CodePage290.TryParse('+', out byte actual);
            Assert.Equal(0x4E, actual);
        }
        [Fact]
        public void TryParse0x4F()
        {
            var parsed = CodePage290.TryParse('|', out byte actual);
            Assert.Equal(0x4F, actual);
        }
        [Fact]
        public void TryParse0x50()
        {
            var parsed = CodePage290.TryParse('&', out byte actual);
            Assert.Equal(0x50, actual);
        }
        [Fact]
        public void TryParse0x51()
        {
            var parsed = CodePage290.TryParse('ｪ', out byte actual);
            Assert.Equal(0x51, actual);
        }
        [Fact]
        public void TryParse0x52()
        {
            var parsed = CodePage290.TryParse('ｫ', out byte actual);
            Assert.Equal(0x52, actual);
        }
        [Fact]
        public void TryParse0x53()
        {
            var parsed = CodePage290.TryParse('ｬ', out byte actual);
            Assert.Equal(0x53, actual);
        }
        [Fact]
        public void TryParse0x54()
        {
            var parsed = CodePage290.TryParse('ｭ', out byte actual);
            Assert.Equal(0x54, actual);
        }
        [Fact]
        public void TryParse0x55()
        {
            var parsed = CodePage290.TryParse('ｮ', out byte actual);
            Assert.Equal(0x55, actual);
        }
        [Fact]
        public void TryParse0x56()
        {
            var parsed = CodePage290.TryParse('ｯ', out byte actual);
            Assert.Equal(0x56, actual);
        }
        [Fact]
        public void TryParse0x58()
        {
            var parsed = CodePage290.TryParse('ｰ', out byte actual);
            Assert.Equal(0x58, actual);
        }
        [Fact]
        public void TryParse0x5A()
        {
            var parsed = CodePage290.TryParse('!', out byte actual);
            Assert.Equal(0x5A, actual);
        }
        [Fact]
        public void TryParse0x5B()
        {
            var parsed = CodePage290.TryParse('¥', out byte actual);
            Assert.Equal(0x5B, actual);
        }

        [Fact]
        public void TryParseen()
        {
            var parsed = CodePage290.TryParse(0x5B, out char actual);
            Assert.Equal('¥', actual);
        }
        [Fact]
        public void TryParse0x5C()
        {
            var parsed = CodePage290.TryParse('*', out byte actual);
            Assert.Equal(0x5C, actual);
        }
        [Fact]
        public void TryParse0x5D()
        {
            var parsed = CodePage290.TryParse(')', out byte actual);
            Assert.Equal(0x5D, actual);
        }
        [Fact]
        public void TryParse0x5E()
        {
            var parsed = CodePage290.TryParse(';', out byte actual);
            Assert.Equal(0x5E, actual);
        }
        [Fact]
        public void TryParse0x5F()
        {
            var parsed = CodePage290.TryParse('¬', out byte actual);
            Assert.Equal(0x5F, actual);
        }
        [Fact]
        public void TryParse0x60()
        {
            var parsed = CodePage290.TryParse('-', out byte actual);
            Assert.Equal(0x60, actual);
        }
        [Fact]
        public void TryParse0x61()
        {
            var parsed = CodePage290.TryParse('/', out byte actual);
            Assert.Equal(0x61, actual);
        }
        [Fact]
        public void TryParse0x62()
        {
            var parsed = CodePage290.TryParse('a', out byte actual);
            Assert.Equal(0x62, actual);
        }
        [Fact]
        public void TryParse0x63()
        {
            var parsed = CodePage290.TryParse('b', out byte actual);
            Assert.Equal(0x63, actual);
        }
        [Fact]
        public void TryParse0x64()
        {
            var parsed = CodePage290.TryParse('c', out byte actual);
            Assert.Equal(0x64, actual);
        }
        [Fact]
        public void TryParse0x65()
        {
            var parsed = CodePage290.TryParse('d', out byte actual);
            Assert.Equal(0x65, actual);
        }
        [Fact]
        public void TryParse0x66()
        {
            var parsed = CodePage290.TryParse('e', out byte actual);
            Assert.Equal(0x66, actual);
        }
        [Fact]
        public void TryParse0x67()
        {
            var parsed = CodePage290.TryParse('f', out byte actual);
            Assert.Equal(0x67, actual);
        }
        [Fact]
        public void TryParse0x68()
        {
            var parsed = CodePage290.TryParse('g', out byte actual);
            Assert.Equal(0x68, actual);
        }
        [Fact]
        public void TryParse0x69()
        {
            var parsed = CodePage290.TryParse('h', out byte actual);
            Assert.Equal(0x69, actual);
        }
        [Fact]
        public void TryParse0x6B()
        {
            var parsed = CodePage290.TryParse(',', out byte actual);
            Assert.Equal(0x6B, actual);
        }
        [Fact]
        public void TryParse0x6C()
        {
            var parsed = CodePage290.TryParse('%', out byte actual);
            Assert.Equal(0x6C, actual);
        }
        [Fact]
        public void TryParse0x6D()
        {
            var parsed = CodePage290.TryParse('_', out byte actual);
            Assert.Equal(0x6D, actual);
        }
        [Fact]
        public void TryParse0x6E()
        {
            var parsed = CodePage290.TryParse('>', out byte actual);
            Assert.Equal(0x6E, actual);
        }
        [Fact]
        public void TryParse0x6F()
        {
            var parsed = CodePage290.TryParse('?', out byte actual);
            Assert.Equal(0x6F, actual);
        }
        [Fact]
        public void TryParse0x70()
        {
            var parsed = CodePage290.TryParse('[', out byte actual);
            Assert.Equal(0x70, actual);
        }
        [Fact]
        public void TryParse0x71()
        {
            var parsed = CodePage290.TryParse('i', out byte actual);
            Assert.Equal(0x71, actual);
        }
        [Fact]
        public void TryParse0x72()
        {
            var parsed = CodePage290.TryParse('j', out byte actual);
            Assert.Equal(0x72, actual);
        }
        [Fact]
        public void TryParse0x73()
        {
            var parsed = CodePage290.TryParse('k', out byte actual);
            Assert.Equal(0x73, actual);
        }
        [Fact]
        public void TryParse0x74()
        {
            var parsed = CodePage290.TryParse('l', out byte actual);
            Assert.Equal(0x74, actual);
        }
        [Fact]
        public void TryParse0x75()
        {
            var parsed = CodePage290.TryParse('m', out byte actual);
            Assert.Equal(0x75, actual);
        }
        [Fact]
        public void TryParse0x76()
        {
            var parsed = CodePage290.TryParse('n', out byte actual);
            Assert.Equal(0x76, actual);
        }
        [Fact]
        public void TryParse0x77()
        {
            var parsed = CodePage290.TryParse('o', out byte actual);
            Assert.Equal(0x77, actual);
        }
        [Fact]
        public void TryParse0x78()
        {
            var parsed = CodePage290.TryParse('p', out byte actual);
            Assert.Equal(0x78, actual);
        }
        [Fact]
        public void TryParse0x79()
        {
            var parsed = CodePage290.TryParse('`', out byte actual);
            Assert.Equal(0x79, actual);
        }
        [Fact]
        public void TryParse0x7A()
        {
            var parsed = CodePage290.TryParse(':', out byte actual);
            Assert.Equal(0x7A, actual);
        }
        [Fact]
        public void TryParse0x7B()
        {
            var parsed = CodePage290.TryParse('#', out byte actual);
            Assert.Equal(0x7B, actual);
        }
        [Fact]
        public void TryParse0x7C()
        {
            var parsed = CodePage290.TryParse('@', out byte actual);
            Assert.Equal(0x7C, actual);
        }
        [Fact]
        public void TryParse0x7D()
        {
            var parsed = CodePage290.TryParse('\'', out byte actual);
            Assert.Equal(0x7D, actual);
        }
        [Fact]
        public void TryParse0x7E()
        {
            var parsed = CodePage290.TryParse('=', out byte actual);
            Assert.Equal(0x7E, actual);
        }
        [Fact]
        public void TryParse0x7F()
        {
            var parsed = CodePage290.TryParse('"', out byte actual);
            Assert.Equal(0x7F, actual);
        }
        [Fact]
        public void TryParse0x80()
        {
            var parsed = CodePage290.TryParse(']', out byte actual);
            Assert.Equal(0x80, actual);
        }
        [Fact]
        public void TryParse0x81()
        {
            var parsed = CodePage290.TryParse('ｱ', out byte actual);
            Assert.Equal(0x81, actual);
        }
        [Fact]
        public void TryParse0x82()
        {
            var parsed = CodePage290.TryParse('ｲ', out byte actual);
            Assert.Equal(0x82, actual);
        }
        [Fact]
        public void TryParse0x83()
        {
            var parsed = CodePage290.TryParse('ｳ', out byte actual);
            Assert.Equal(0x83, actual);
        }
        [Fact]
        public void TryParse0x84()
        {
            var parsed = CodePage290.TryParse('ｴ', out byte actual);
            Assert.Equal(0x84, actual);
        }
        [Fact]
        public void TryParse0x85()
        {
            var parsed = CodePage290.TryParse('ｵ', out byte actual);
            Assert.Equal(0x85, actual);
        }
        [Fact]
        public void TryParse0x86()
        {
            var parsed = CodePage290.TryParse('ｶ', out byte actual);
            Assert.Equal(0x86, actual);
        }
        [Fact]
        public void TryParse0x87()
        {
            var parsed = CodePage290.TryParse('ｷ', out byte actual);
            Assert.Equal(0x87, actual);
        }
        [Fact]
        public void TryParse0x88()
        {
            var parsed = CodePage290.TryParse('ｸ', out byte actual);
            Assert.Equal(0x88, actual);
        }
        [Fact]
        public void TryParse0x89()
        {
            var parsed = CodePage290.TryParse('ｹ', out byte actual);
            Assert.Equal(0x89, actual);
        }
        [Fact]
        public void TryParse0x8A()
        {
            var parsed = CodePage290.TryParse('ｺ', out byte actual);
            Assert.Equal(0x8A, actual);
        }
        [Fact]
        public void TryParse0x8B()
        {
            var parsed = CodePage290.TryParse('q', out byte actual);
            Assert.Equal(0x8B, actual);
        }
        [Fact]
        public void TryParse0x8C()
        {
            var parsed = CodePage290.TryParse('ｻ', out byte actual);
            Assert.Equal(0x8C, actual);
        }
        [Fact]
        public void TryParse0x8D()
        {
            var parsed = CodePage290.TryParse('ｼ', out byte actual);
            Assert.Equal(0x8D, actual);
        }
        [Fact]
        public void TryParse0x8E()
        {
            var parsed = CodePage290.TryParse('ｽ', out byte actual);
            Assert.Equal(0x8E, actual);
        }
        [Fact]
        public void TryParse0x8F()
        {
            var parsed = CodePage290.TryParse('ｾ', out byte actual);
            Assert.Equal(0x8F, actual);
        }
        [Fact]
        public void TryParse0x90()
        {
            var parsed = CodePage290.TryParse('ｿ', out byte actual);
            Assert.Equal(0x90, actual);
        }
        [Fact]
        public void TryParse0x91()
        {
            var parsed = CodePage290.TryParse('ﾀ', out byte actual);
            Assert.Equal(0x91, actual);
        }
        [Fact]
        public void TryParse0x92()
        {
            var parsed = CodePage290.TryParse('ﾁ', out byte actual);
            Assert.Equal(0x92, actual);
        }
        [Fact]
        public void TryParse0x93()
        {
            var parsed = CodePage290.TryParse('ﾂ', out byte actual);
            Assert.Equal(0x93, actual);
        }
        [Fact]
        public void TryParse0x94()
        {
            var parsed = CodePage290.TryParse('ﾃ', out byte actual);
            Assert.Equal(0x94, actual);
        }
        [Fact]
        public void TryParse0x95()
        {
            var parsed = CodePage290.TryParse('ﾄ', out byte actual);
            Assert.Equal(0x95, actual);
        }
        [Fact]
        public void TryParse0x96()
        {
            var parsed = CodePage290.TryParse('ﾅ', out byte actual);
            Assert.Equal(0x96, actual);
        }
        [Fact]
        public void TryParse0x97()
        {
            var parsed = CodePage290.TryParse('ﾆ', out byte actual);
            Assert.Equal(0x97, actual);
        }
        [Fact]
        public void TryParse0x98()
        {
            var parsed = CodePage290.TryParse('ﾇ', out byte actual);
            Assert.Equal(0x98, actual);
        }
        [Fact]
        public void TryParse0x99()
        {
            var parsed = CodePage290.TryParse('ﾈ', out byte actual);
            Assert.Equal(0x99, actual);
        }
        [Fact]
        public void TryParse0x9A()
        {
            var parsed = CodePage290.TryParse('ﾉ', out byte actual);
            Assert.Equal(0x9A, actual);
        }
        [Fact]
        public void TryParse0x9B()
        {
            var parsed = CodePage290.TryParse('r', out byte actual);
            Assert.Equal(0x9B, actual);
        }
        [Fact]
        public void TryParse0x9D()
        {
            var parsed = CodePage290.TryParse('ﾊ', out byte actual);
            Assert.Equal(0x9D, actual);
        }
        [Fact]
        public void TryParse0x9E()
        {
            var parsed = CodePage290.TryParse('ﾋ', out byte actual);
            Assert.Equal(0x9E, actual);
        }
        [Fact]
        public void TryParse0x9F()
        {
            var parsed = CodePage290.TryParse('ﾌ', out byte actual);
            Assert.Equal(0x9F, actual);
        }
        [Fact]
        public void TryParse0xA0()
        {
            var parsed = CodePage290.TryParse('~', out byte actual);
            Assert.Equal(0xA0, actual);
        }
        [Fact]
        public void TryParse0xA2()
        {
            var parsed = CodePage290.TryParse('ﾍ', out byte actual);
            Assert.Equal(0xA2, actual);
        }
        [Fact]
        public void TryParse0xA3()
        {
            var parsed = CodePage290.TryParse('ﾎ', out byte actual);
            Assert.Equal(0xA3, actual);
        }
        [Fact]
        public void TryParse0xA4()
        {
            var parsed = CodePage290.TryParse('ﾏ', out byte actual);
            Assert.Equal(0xA4, actual);
        }
        [Fact]
        public void TryParse0xA5()
        {
            var parsed = CodePage290.TryParse('ﾐ', out byte actual);
            Assert.Equal(0xA5, actual);
        }
        [Fact]
        public void TryParse0xA6()
        {
            var parsed = CodePage290.TryParse('ﾑ', out byte actual);
            Assert.Equal(0xA6, actual);
        }
        [Fact]
        public void TryParse0xA7()
        {
            var parsed = CodePage290.TryParse('ﾒ', out byte actual);
            Assert.Equal(0xA7, actual);
        }
        [Fact]
        public void TryParse0xA8()
        {
            var parsed = CodePage290.TryParse('ﾓ', out byte actual);
            Assert.Equal(0xA8, actual);
        }
        [Fact]
        public void TryParse0xA9()
        {
            var parsed = CodePage290.TryParse('ﾔ', out byte actual);
            Assert.Equal(0xA9, actual);
        }
        [Fact]
        public void TryParse0xAA()
        {
            var parsed = CodePage290.TryParse('ﾕ', out byte actual);
            Assert.Equal(0xAA, actual);
        }
        [Fact]
        public void TryParse0xAB()
        {
            var parsed = CodePage290.TryParse('s', out byte actual);
            Assert.Equal(0xAB, actual);
        }
        [Fact]
        public void TryParse0xAC()
        {
            var parsed = CodePage290.TryParse('ﾖ', out byte actual);
            Assert.Equal(0xAC, actual);
        }
        [Fact]
        public void TryParse0xAD()
        {
            var parsed = CodePage290.TryParse('ﾗ', out byte actual);
            Assert.Equal(0xAD, actual);
        }
        [Fact]
        public void TryParse0xAE()
        {
            var parsed = CodePage290.TryParse('ﾘ', out byte actual);
            Assert.Equal(0xAE, actual);
        }
        [Fact]
        public void TryParse0xAF()
        {
            var parsed = CodePage290.TryParse('ﾙ', out byte actual);
            Assert.Equal(0xAF, actual);
        }
        [Fact]
        public void TryParse0xB0()
        {
            var parsed = CodePage290.TryParse('^', out byte actual);
            Assert.Equal(0xB0, actual);
        }
        [Fact]
        public void TryParse0xB2()
        {
            var parsed = CodePage290.TryParse('\\', out byte actual);
            Assert.Equal(0xB2, actual);
        }
        [Fact]
        public void TryParse0xB3()
        {
            var parsed = CodePage290.TryParse('t', out byte actual);
            Assert.Equal(0xB3, actual);
        }
        [Fact]
        public void TryParse0xB4()
        {
            var parsed = CodePage290.TryParse('u', out byte actual);
            Assert.Equal(0xB4, actual);
        }
        [Fact]
        public void TryParse0xB5()
        {
            var parsed = CodePage290.TryParse('v', out byte actual);
            Assert.Equal(0xB5, actual);
        }
        [Fact]
        public void TryParse0xB6()
        {
            var parsed = CodePage290.TryParse('w', out byte actual);
            Assert.Equal(0xB6, actual);
        }
        [Fact]
        public void TryParse0xB7()
        {
            var parsed = CodePage290.TryParse('x', out byte actual);
            Assert.Equal(0xB7, actual);
        }
        [Fact]
        public void TryParse0xB8()
        {
            var parsed = CodePage290.TryParse('y', out byte actual);
            Assert.Equal(0xB8, actual);
        }
        [Fact]
        public void TryParse0xB9()
        {
            var parsed = CodePage290.TryParse('z', out byte actual);
            Assert.Equal(0xB9, actual);
        }
        [Fact]
        public void TryParse0xBA()
        {
            var parsed = CodePage290.TryParse('ﾚ', out byte actual);
            Assert.Equal(0xBA, actual);
        }
        [Fact]
        public void TryParse0xBB()
        {
            var parsed = CodePage290.TryParse('ﾛ', out byte actual);
            Assert.Equal(0xBB, actual);
        }
        [Fact]
        public void TryParse0xBC()
        {
            var parsed = CodePage290.TryParse('ﾜ', out byte actual);
            Assert.Equal(0xBC, actual);
        }
        [Fact]
        public void TryParse0xBD()
        {
            var parsed = CodePage290.TryParse('ﾝ', out byte actual);
            Assert.Equal(0xBD, actual);
        }
        [Fact]
        public void TryParse0xBE()
        {
            var parsed = CodePage290.TryParse('ﾞ', out byte actual);
            Assert.Equal(0xBE, actual);
        }
        [Fact]
        public void TryParse0xBF()
        {
            var parsed = CodePage290.TryParse('ﾟ', out byte actual);
            Assert.Equal(0xBF, actual);
        }
        [Fact]
        public void TryParse0xC0()
        {
            var parsed = CodePage290.TryParse('{', out byte actual);
            Assert.Equal(0xC0, actual);
        }
        [Fact]
        public void TryParse0xC1()
        {
            var parsed = CodePage290.TryParse('A', out byte actual);
            Assert.Equal(0xC1, actual);
        }
        [Fact]
        public void TryParse0xC2()
        {
            var parsed = CodePage290.TryParse('B', out byte actual);
            Assert.Equal(0xC2, actual);
        }
        [Fact]
        public void TryParse0xC3()
        {
            var parsed = CodePage290.TryParse('C', out byte actual);
            Assert.Equal(0xC3, actual);
        }
        [Fact]
        public void TryParse0xC4()
        {
            var parsed = CodePage290.TryParse('D', out byte actual);
            Assert.Equal(0xC4, actual);
        }
        [Fact]
        public void TryParse0xC5()
        {
            var parsed = CodePage290.TryParse('E', out byte actual);
            Assert.Equal(0xC5, actual);
        }
        [Fact]
        public void TryParse0xC6()
        {
            var parsed = CodePage290.TryParse('F', out byte actual);
            Assert.Equal(0xC6, actual);
        }
        [Fact]
        public void TryParse0xC7()
        {
            var parsed = CodePage290.TryParse('G', out byte actual);
            Assert.Equal(0xC7, actual);
        }
        [Fact]
        public void TryParse0xC8()
        {
            var parsed = CodePage290.TryParse('H', out byte actual);
            Assert.Equal(0xC8, actual);
        }
        [Fact]
        public void TryParse0xC9()
        {
            var parsed = CodePage290.TryParse('I', out byte actual);
            Assert.Equal(0xC9, actual);
        }
        [Fact]
        public void TryParse0xD0()
        {
            var parsed = CodePage290.TryParse('}', out byte actual);
            Assert.Equal(0xD0, actual);
        }
        [Fact]
        public void TryParse0xD1()
        {
            var parsed = CodePage290.TryParse('J', out byte actual);
            Assert.Equal(0xD1, actual);
        }
        [Fact]
        public void TryParse0xD2()
        {
            var parsed = CodePage290.TryParse('K', out byte actual);
            Assert.Equal(0xD2, actual);
        }
        [Fact]
        public void TryParse0xD3()
        {
            var parsed = CodePage290.TryParse('L', out byte actual);
            Assert.Equal(0xD3, actual);
        }
        [Fact]
        public void TryParse0xD4()
        {
            var parsed = CodePage290.TryParse('M', out byte actual);
            Assert.Equal(0xD4, actual);
        }
        [Fact]
        public void TryParse0xD5()
        {
            var parsed = CodePage290.TryParse('N', out byte actual);
            Assert.Equal(0xD5, actual);
        }
        [Fact]
        public void TryParse0xD6()
        {
            var parsed = CodePage290.TryParse('O', out byte actual);
            Assert.Equal(0xD6, actual);
        }
        [Fact]
        public void TryParse0xD7()
        {
            var parsed = CodePage290.TryParse('P', out byte actual);
            Assert.Equal(0xD7, actual);
        }
        [Fact]
        public void TryParse0xD8()
        {
            var parsed = CodePage290.TryParse('Q', out byte actual);
            Assert.Equal(0xD8, actual);
        }
        [Fact]
        public void TryParse0xD9()
        {
            var parsed = CodePage290.TryParse('R', out byte actual);
            Assert.Equal(0xD9, actual);
        }
        [Fact]
        public void TryParse0xE0()
        {
            var parsed = CodePage290.TryParse('$', out byte actual);
            Assert.Equal(0xE0, actual);
        }
        [Fact]
        public void TryParse0xE2()
        {
            var parsed = CodePage290.TryParse('S', out byte actual);
            Assert.Equal(0xE2, actual);
        }
        [Fact]
        public void TryParse0xE3()
        {
            var parsed = CodePage290.TryParse('T', out byte actual);
            Assert.Equal(0xE3, actual);
        }
        [Fact]
        public void TryParse0xE4()
        {
            var parsed = CodePage290.TryParse('U', out byte actual);
            Assert.Equal(0xE4, actual);
        }
        [Fact]
        public void TryParse0xE5()
        {
            var parsed = CodePage290.TryParse('V', out byte actual);
            Assert.Equal(0xE5, actual);
        }
        [Fact]
        public void TryParse0xE6()
        {
            var parsed = CodePage290.TryParse('W', out byte actual);
            Assert.Equal(0xE6, actual);
        }
        [Fact]
        public void TryParse0xE7()
        {
            var parsed = CodePage290.TryParse('X', out byte actual);
            Assert.Equal(0xE7, actual);
        }
        [Fact]
        public void TryParse0xE8()
        {
            var parsed = CodePage290.TryParse('Y', out byte actual);
            Assert.Equal(0xE8, actual);
        }
        [Fact]
        public void TryParse0xE9()
        {
            var parsed = CodePage290.TryParse('Z', out byte actual);
            Assert.Equal(0xE9, actual);
        }
        [Fact]
        public void TryParse0xF0()
        {
            var parsed = CodePage290.TryParse('0', out byte actual);
            Assert.Equal(0xF0, actual);
        }
        [Fact]
        public void TryParse0xF1()
        {
            var parsed = CodePage290.TryParse('1', out byte actual);
            Assert.Equal(0xF1, actual);
        }
        [Fact]
        public void TryParse0xF2()
        {
            var parsed = CodePage290.TryParse('2', out byte actual);
            Assert.Equal(0xF2, actual);
        }
        [Fact]
        public void TryParse0xF3()
        {
            var parsed = CodePage290.TryParse('3', out byte actual);
            Assert.Equal(0xF3, actual);
        }
        [Fact]
        public void TryParse0xF4()
        {
            var parsed = CodePage290.TryParse('4', out byte actual);
            Assert.Equal(0xF4, actual);
        }
        [Fact]
        public void TryParse0xF5()
        {
            var parsed = CodePage290.TryParse('5', out byte actual);
            Assert.Equal(0xF5, actual);
        }
        [Fact]
        public void TryParse0xF6()
        {
            var parsed = CodePage290.TryParse('6', out byte actual);
            Assert.Equal(0xF6, actual);
        }
        [Fact]
        public void TryParse0xF7()
        {
            var parsed = CodePage290.TryParse('7', out byte actual);
            Assert.Equal(0xF7, actual);
        }
        [Fact]
        public void TryParse0xF8()
        {
            var parsed = CodePage290.TryParse('8', out byte actual);
            Assert.Equal(0xF8, actual);
        }
        [Fact]
        public void TryParse0xF9()
        {
            var parsed = CodePage290.TryParse('9', out byte actual);
            Assert.Equal(0xF9, actual);
        }
        //[Fact]
        //public void TryParse0xFA()
        //{
        //    var parsed = CodePage290.TryParse('|', out byte actual);
        //    Assert.Equal(0xFA, actual);
        //}

    }
}
