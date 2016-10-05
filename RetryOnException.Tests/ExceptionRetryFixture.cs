using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace RetryOnException.Tests
{
    /// <summary>
    /// This test shows that if an exception is setup to retry, then the test is run multiple times when that exception is thrown.
    /// </summary>
    public class ExceptionIsRetriedFixture : RepeatingTestsFixtureBase
    {
        [Test]
        [RetryOnException(ListOfExceptions = new[] {typeof(NotSupportedException)})]
        [Retry(3)]
        public void RetryAttemptedOnException()
        {
            const int totalAttempts = 3;
            Count++;
            if (Count == totalAttempts)
            {
                if (Equals(TestContext.CurrentContext.Result.Outcome, ResultState.Failure))
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }
            else if (Count > totalAttempts)
            {
                Assert.Fail();
            }
            throw new NotSupportedException();
        }
    }

    /// <summary>
    /// This test shows that multiple exceptions can be setup and if one of the exceptions is thrown then the test retries.
    /// </summary>
    public class MultipleExceptionSetupIsRetriedFixture : RepeatingTestsFixtureBase
    {
        [Test]
        [RetryOnException(ListOfExceptions = new[] {typeof(NotSupportedException), typeof(NullReferenceException)})]
        [Retry(5)]
        public void RetryAttemptedExceptionInList()
        {
            const int totalAttempts = 5;
            Count++;
            if (Count == totalAttempts)
            {
                if (Equals(TestContext.CurrentContext.Result.Outcome, ResultState.Failure))
                {
                    Assert.Pass();
                }
                else
                {
                    Assert.Fail();
                }
            }
            else if (Count > totalAttempts)
            {
                Assert.Fail();
            }
            throw new NullReferenceException();
        }
    }

    /// <summary>
    /// This test shows that if an exception that is not in the retry list then the test does not retry and runs once only, 
    /// returning the exception message back to the running test.
    /// </summary>
    public class ExceptionIsNotRetriedFixture : RepeatingTestsFixtureBase
    {
        [Test]
        [RetryOnException(ListOfExceptions = new[] {typeof(NotSupportedException)})]
        [Retry(3)]
        public void RetryNotAttemptedOnException()
        {
            Count++;

            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => { throw new ArgumentOutOfRangeException(); });
            Assert.That(Count == 1);
            Assert.That(TestContext.CurrentContext.Result.Outcome, Is.EqualTo(ResultState.Inconclusive));
            Assert.That(ex.Message, Is.EqualTo(new ArgumentOutOfRangeException().Message));
        }

    }

}
