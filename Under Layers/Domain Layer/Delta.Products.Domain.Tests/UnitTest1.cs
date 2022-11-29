using System;
using Xunit;

namespace Delta.Products.Domain.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void IsNullValueEmpty()
        {
            var stu = ProductItemCode.Of(string.Empty);
            Assert.True(stu.IsNullValue);

        }
        [Fact]
        public void IsNullValue16spaces()
        {
            var stu = ProductItemCode.Of(new string(' ',16));
            Assert.True(stu.IsNullValue);

        }
        [Fact]
        public void IsNullValueXYZ()
        {
            var stu = ProductItemCode.Of("XYZ");
            Assert.False(stu.IsNullValue);

        }
    }
}
