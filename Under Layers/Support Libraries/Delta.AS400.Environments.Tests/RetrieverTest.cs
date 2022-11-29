using System;
using Xunit;

namespace Delta.AS400.Environments
{
    public class RetrieverTest
    {
        [Fact]
        public void SystemValueyyyyMMddOfNowOfTest()
        {
            Retriever.Instance.System = SystemValue.Of(new DateTime(2021,7,30,12,34,56));
            Assert.Equal(20210730,Retriever.Instance.System.yyyyMMddOfNow);
        }
        [Fact]
        public void SystemValueyyyyMMddOfNowTest()
        {
            Assert.Equal(int.Parse(DateTime.Now.ToString("yyyyMMdd")), Retriever.Instance.System.yyyyMMddOfNow);
        }
    }
}
