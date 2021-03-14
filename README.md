# MockGen
MockGen is a .NET mock library based on source generators.

[![thomas-girotto](https://circleci.com/gh/thomas-girotto/MockGen.svg?style=svg)](https://app.circleci.com/pipelines/github/thomas-girotto/MockGen)


## How it works

Instead of generating test doubles at runtime by using dynamic proxy like [Moq](https://github.com/moq/moq4) or [NSubstitute](https://nsubstitute.github.io), MockGen use [source generators](https://devblogs.microsoft.com/dotnet/introducing-c-source-generators/) to generate *sources* of the test doubles which will be included in your test assembly.

That allow the following capabilities :
 - Exposing an API that really embrace the types you want to mock (no more object[] that are still present here and there in other libs, like in constructors argument when you want to mock a class and not an interface)
 - Navigate and setup a breakpoint inside the generated sources, which can be handy to understand why some calls didn't work as expected
 - Expose directly the calls with their parameters that were made on your mocked methods (reprensented as a list of tuples), and let you use your favorite assertion library to check them, instead of having assertions being part of the mocking library
 - Mock protected method (that's still a TODO :))
 - Have a nice API like nsubstitute (which i prefer compared to moq), but without the downsize of having all those extensions methods on object, which pop in your intellisense every time you enter a dot :)

## Quick look

```csharp
// Tell the compiler you want to generate a mock for IDependency. You can do that only once per type, 
// and put them all in a Generators.cs class for instance
MockGenerator.Mock<IDependency>();

// Once the compiler have detected that you're interested in mocking IDependency type, it generates 
// IDependency method on Mock static class. This is the entrypoint of you mock configuration
var mock = Mock.IDependency();

// Setup mock behavior
mock.GetAge(Arg<string>.Any).Returns(42); // Will return 42 for any parameter
mock.GetAge("My Daughter").Returns(5); // Will return 5 only if this parameter is given
mock.GetAge("My Daughter").Returns(5).AndExecute(_ => {})); // Also execute the given action when called
mock.GetAge(Arg<string>.When(s => s.StartsWith("Grand"))).Returns(70); // Will return 70 only for parameters starting with "Grand"
mock.GetAge("Lemmy Kilmister").Throws<Exception>(); // Will throw a new instance of Exception
mock.GetAge("Lemmy Kilmister").Throws(new Exception("He's dead :(")); // Will throw this specifc exception

// Pass the mock to the sut
var sut = new MyServiceUnderTest(mock.Build()); // Build() returns the original type setup with mock behavior

// Assertions on calls 
Assert.Equal(2, mock.GetAge(Arg<string>.Any).NumberOfCalls); // Number of calls to GetAge for any parameter
Assert.Equal(1, mock.GetAge("My Daughter").NumberOfCalls); // Number of calls to GetAge with "My Daughter" parameter
Assert.Equal(1, mock.GetAge(Arg<string>.When(s => s.StartsWith("foo")).NumberOfCalls)); // Number of calls matching predicate

```