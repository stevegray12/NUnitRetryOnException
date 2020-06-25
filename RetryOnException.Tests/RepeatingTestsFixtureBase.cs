using NUnit.Framework;

namespace RetryOnException.Tests
{
    [TestFixture]
    public class RepeatingTestsFixtureBase
    {
        //public int OneTimeSetupCount { get; private set; }
        //public int OneTimeTeardownCount { get; private set; }
        //public int SetupCount { get; private set; }
        //public int TeardownCount { get; private set; }
        public int Count { get; set; }

        //[OneTimeSetUp]
        //public void OneTimeSetUp()
        //{
        //    OneTimeSetupCount++;
        //}

        //[OneTimeTearDown]
        //public void OneTimeTearDown()
        //{
        //    OneTimeTeardownCount++;
        //}

        //[SetUp]
        //public void SetUp()
        //{
        //    SetupCount++;
        //}

        //[TearDown]
        //public void TearDown()
        //{
        //    TeardownCount++;
        //}
        
    }
}
