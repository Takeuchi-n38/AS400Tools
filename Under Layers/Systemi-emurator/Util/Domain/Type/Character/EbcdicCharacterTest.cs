using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Domain.Type.Character
{
    [TestFixture]
    public class EbcdicCharacterTest
    {
        [Test]
        public void スペース_英語の子文字_大文字_数字の順に並ぶこと()
        {
            var stu = EbcdicCharacter.stringComposedOf2byteCharacterComparator;
            List<string> datas = new List<string>
            {
                "2",
                " ",
                "b",
                "B",
                "A",
                "a",
                "1"
            };
            List<string> expected = new List<string>
            {
                " ",
                "a",
                "b",
                "A",
                "B",
                "1",
                "2"
            };
            datas.Sort(stu);
            var actual = datas;
            Assert.AreEqual(expected.ToArray(), actual.ToArray());
        }

        [Test]
        public void 非対応文字が含まれていたら例外を投げる_1byteを意図時()
        {
            var stu = EbcdicCharacter.stringComposedOf1byteCharacterComparator;
            List<string> datas = new List<string>
            {
                "あ",
                "&",
                "\\"
            };

            Assert.Throws<Exception>(new TestDelegate((Action)(() => datas.Sort(stu))));
        }

        public void SPC_ｧ_minus_sharp_ア_A_0の順に並ぶこと()
        {
            var stu = EbcdicCharacter.stringComposedOf2byteCharacterComparator;
            List<string> datas = new List<string>
            {
                "-",
                "ｱ",
                " ",
                "#",
                "ｧ",
                "A",
                "0"
            };
            List<string> expected = new List<string>
            {
                " ",
                "ｧ",
                "-",
                "#",
                "ｱ",
                "A",
                "0"
            };
            datas.Sort(stu);
            var actual = datas;
            Assert.AreEqual(expected.ToArray(), actual.ToArray());
        }

        public void バイト長異文字混合()
        {
            var stu = EbcdicCharacter.stringComposedOf2byteCharacterComparator;
            List<string> datas = new List<string>
            {
                "-",
                "ｱ",
                " ",
                "い",
                "#",
                "あ",
                "ｧ",
                "A",
                "0"
            };
            List<string> expected = new List<string>
            {
                " ",
                "ｧ",
                "-",
                "#",
                "ｱ",
                "A",
                "0",
                "あ",
                "い"
            };
            datas.Sort(stu);
            var actual = datas;
            Assert.AreEqual(expected.ToArray(), actual.ToArray());
        }

        public void getByteLength半角混合()
        {
            var actual = EbcdicCharacter.GetByteLength("0aｱ");
            var expected = 3;
            Assert.AreEqual(expected, actual);
        }

        public void getByteLength異文字混合()
        {
            var actual = EbcdicCharacter.GetByteLength("0aあ");
            var expected = 4;
            Assert.AreEqual(expected, actual);
        }

    }
}
