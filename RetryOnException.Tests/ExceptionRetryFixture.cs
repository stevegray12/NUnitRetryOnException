using System;
using NUnit.Framework;

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
            Count++;
            throw new NotSupportedException();
        }
    }

    /// <summary>
    /// This test shows that multiple exceptions can be setup and if one of the exceptions is thrown then the test retries.
    /// </summary>
    public class MultipleExceptionSetupIsRetriedFixture : RepeatingTestsFixtureBase
    {
        [Test]
        [RetryOnException(ListOfExceptions = new[] { typeof(NotSupportedException), typeof(NullReferenceException) })]
        [Retry(5)]
        public void RetryAttemptedExceptionInList()
        {
            Count++;
            throw new NullReferenceException();
        }
    }

    /// <summary>
    /// This test shows that if an exception that is not in the retry list is thron then the test does not retry and runs once.
    /// </summary>
    public class ExceptionIsNotRetriedFixture : RepeatingTestsFixtureBase
    {
        [Test]
        [RetryOnException(ListOfExceptions = new[] { typeof(NotSupportedException) })]
        [Retry(3)]
        public void RetryNotAttemptedOnException()
        {
            Count++;
            throw new ArgumentOutOfRangeException();
        }
    }

}
