using System;
using System.Threading;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;
using NUnit.Framework.Internal.Execution;

namespace RetryOnException.Tests
{
    /// <summary>
    ///  This class runs the unit tests within the NUnit framework.
    /// </summary>
    public class TestBuilder
    {
        public static TestSuite MakeFixture(Type type)
        {
            return new DefaultSuiteBuilder().BuildFrom(new TypeWrapper(type));
        }

        public static TestSuite MakeFixture(object fixture)
        {
            TestSuite suite = MakeFixture(fixture.GetType());
            suite.Fixture = fixture;
            return suite;
        }
        
        public static ITestResult RunTestFixture(object fixture)
        {
            return RunTestSuite(MakeFixture(fixture), fixture);
        }

        public static ITestResult RunTestSuite(TestSuite suite, object testObject)
        {
            TestExecutionContext context = new TestExecutionContext {TestObject = testObject};

            WorkItem work = WorkItem.CreateWorkItem(suite, TestFilter.Empty);
            work.InitializeContext(context);
            work.Execute();

            while (work.State != WorkItemState.Complete)
            {
                Thread.Sleep(1);
            }

            return work.Result;
        }
        
    }
}
