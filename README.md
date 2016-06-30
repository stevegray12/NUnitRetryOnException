# NUnitRetryOnException
NUnit extension that catches exceptions and allows the test to be retried use-sing the NUnit Retry Option; Very useull for Selenium UI automated test. 

## Instructions for Use
Get the package from Nuget

[Test]
    [RetryOnException(ListOfExceptions = new[] { typeof(Exception)})]
    //Retry 
    [Retry(5)]
    public void Test()
    {
    }

###Examplees of exceptions
[RetryOnException(ListOfExceptions = new[] { typeof(NotSupportedException), typeof(NullReferenceException) })]




## Building the Package

* Clone this repository to your local file system
* Open the soloution in Visual studio 2015 and build

