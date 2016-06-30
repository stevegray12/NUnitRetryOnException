using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace RetryOnException.Tests
{
    [TestFixture]
    public class ExceptionRetryAttributeTests
    {
        private const string Failed = "Failed(Child)";

        [TestCase(typeof(ExceptionIsRetriedFixture), Failed, 3)]
        [TestCase(typeof(MultipleExceptionSetupIsRetriedFixture), Failed, 5)]
        [TestCase(typeof(ExceptionIsNotRetriedFixture), Failed, 1)]
        public void CheckExceptionRetryPolicy(Type fixtureType, string outcome, int testRunCount)
        {
            RepeatingTestsFixtureBase fixture = (RepeatingTestsFixtureBase)Reflect.Construct(fixtureType);
            ITestResult result = TestBuilder.RunTestFixture(fixture);

            Assert.That(result.ResultState.ToString(), Is.EqualTo(outcome));
            Assert.AreEqual(testRunCount, fixture.Count);
        }
        
    }
}
