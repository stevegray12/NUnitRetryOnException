using System;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Commands;

namespace RetryOnException
{
    /// <summary>
    /// Nunit retry policy to work with Exceptions being thrown and allow the test to try again if the given exception is accepted as retry type.
    /// <c>
    /// The existing Nunit retry attribute must be used to invoke the retry process.
    /// </c>
    /// </summary>
    /// See Example below.
    /// <example>
    /// [Retry(2)]
    /// [RetryOnException(ListOfExceptions = new[] { typeof(NoSuchElementException), typeof(WebDriverTimeoutException) })]
    /// </example>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class RetryOnExceptionAttribute : NUnitAttribute, IWrapTestMethod
    {
        /// <summary>
        /// Storage for all the exception types that are allowed to be retried, using the NUnit Retry attribute.
        /// </summary>
        public Type[] ListOfExceptions
        {
            get { return _listOfExceptions; }
            set { _listOfExceptions = value; }
        }
        private static Type[] _listOfExceptions;
        
        public TestCommand Wrap(TestCommand command)
        {
            return new RetryOnExceptionCommand(command);
        }

        private class RetryOnExceptionCommand : DelegatingTestCommand
        {
            public RetryOnExceptionCommand(TestCommand command) : base(command) { }

            public override TestResult Execute(TestExecutionContext context)
            {
                Type caughtType = null;
                Exception exception = null;
                var resultState = new ResultState(TestStatus.Failed);
                try
                {
                    resultState = innerCommand.Execute(context).ResultState;
                }
                catch (Exception ex)
                {
                    if (ex is NUnitException)
                    {
                        ex = ex.InnerException;
                    }
                    caughtType = ex.GetType();
                    exception = ex;
                }

                if (_listOfExceptions.Any(ex => ex == caughtType))
                {
                    return ReturnTestResult(context, caughtType, exception);
                }

                context.CurrentResult.SetResult(resultState);
                return context.CurrentResult;
            }

            private static TestResult ReturnTestResult(TestExecutionContext context, Type caughtType, Exception exception)
            {
                context.CurrentResult.SetResult(ResultState.Failure, string.Format("{0} Inner Exception {1}", caughtType, exception));
                return context.CurrentResult;
            }
        }
    }
}
