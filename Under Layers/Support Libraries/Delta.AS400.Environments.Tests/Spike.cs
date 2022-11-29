using System;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;

namespace Delta.AS400.Environments
{
    public class Spike
    {
        //dotnet tool install -g dotnet-reportgenerator-globaltool
        //dotnet test --collect:"XPlat Code Coverage" --filter DisplayName=Delta.AS400.Environments.RetrieverTest.SystemValueyyyyMMddOfNowOfTest
        //reportgenerator -reports:"coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
        [Fact]
        public void DecimalToIntPos()
        {
            Assert.Equal(1, (int)new Decimal(1.5));
        }
        [Fact]
        public void DecimalToIntPos6()
        {
            Assert.Equal(1, (int)new Decimal(1.6));
        }
        [Fact]
        public void DecimalToIntNega()
        {
            Assert.Equal(-1, (int)new Decimal(-1.5));
        }
        [Fact]
        public void DecimalToIntNega6()
        {
            Assert.Equal(-1, (int)new Decimal(-1.6));
        }

        [Fact]
        public void DecimalDevide()
        {

            Assert.Equal(new decimal(0.25), decimal.Divide(1,4));
        }

        [Fact]
        public void DecimalDevide3()
        {
            var actual = decimal.Round(decimal.Divide(1, 3), 3, MidpointRounding.ToZero);
            Assert.Equal(new decimal(0.333), actual);
        }





    }
}
