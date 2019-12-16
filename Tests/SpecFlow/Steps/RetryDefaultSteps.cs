using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Tests.SpecFlow.Steps
{
    [Binding]
    public class RetryDefaultSteps
    {
        private static int _retryCount = 0;

        [When(@"I increment the default retry count")]
        public void WhenIIncrementTheDefaultRetryCount()
        {
            _retryCount++;
        }

        [Then(@"the default result should be (.*)")]
        public void ThenTheDefaultResultShouldBe(int expected)
        {
            Assert.AreEqual(expected, _retryCount);
        }
    }
}
