# MockGen
MockGen is a .NET mock library based on source generators. It aims to be the best mocking library in .net world by using source generators to offer the most powerful and straightforward API.

[![thomas-girotto](https://circleci.com/gh/thomas-girotto/MockGen.svg?style=svg)](https://app.circleci.com/pipelines/github/thomas-girotto/MockGen)


## How it works

Instead of generating test doubles at runtime by using dynamic proxy like [Moq](https://github.com/moq/moq4) or [NSubstitute](https://nsubstitute.github.io), MockGen use [source generators](https://devblogs.microsoft.com/dotnet/introducing-c-source-generators/) to generate *sources* of the test doubles which will be included in your test assembly.

That allow the following capabilities :
 - Exposing an API that really embrace the types you want to mock (no more object[] that are still present here and there in other libs, like in constructors argument when you want to mock a class and not an interface)
 - Navigate and setup a breakpoint inside the generated sources, which can be handy to understand why some calls didn't work as expected
 - Expose directly the calls with their parameters that were made on your mocked methods (reprensented as a list of tuples), and let you use your favorite assertion library to check them, instead of having assertions being part of the mocking library
 - Mock protected method
 - Have a nice API like nsubstitute (which i prefer compared to moq), but without the downsize of having all those extensions methods on object, which pop in your intellisense every time you enter a dot :)
 - It's a lot faster at runtime: See [Benchmark.md](Benchmark.md)

## Known Issues

### You need at least Visual Studio 16.9.2 
But later is better as source generators are regularly improved within Visual Studio. Although there are still some [issues](https://github.com/dotnet/roslyn/issues/50451). For the moment, newly generated types are not seen right after the compilation in intellisense and you need to restart Visual Studio to see them, which is really frustrating. Hopefully it will be fixed in a future release.

Rider doesn't have this issue and updates correctly intellisense.

### Lack support for generic methods
Yes, it's a shame and make this lib pretty useless, just forgot that use case while developing and i'm realizing now that it's a huge work to support them... Working on it :)

## Install It

Reference MockGen in your csproj like this. It should be referenced as an analyzer.
```xml
<PackageReference Include="MockGen" Version="1.*" OutputItemType="Analyzer" ReferenceOutputAssembly="false" />
```

## Quick look

```csharp
// MockG.Generate<IDependency>() Tell the compiler you want to generate a mock for IDependency.
// It returns an instance of Generate<IDependency> type.
// The compiler will generate an extension method 'New' on Generate<IDepedency> type that will give you 
// the appropriate mock, and all the helper classes used under the hood.
// The known issue here is annoying as for the moment you may have to restart Visual Studio to be able to
// see the New method in your intellisense if it's the first time you're generating a mock for IDependency
var mock = MockG.Generate<IDependency>().New();

// Returns
mock.GetAge(Arg<string>.Any).Returns(42); // Will return 42 for any parameter
mock.GetAge("My Daughter").Returns(5); // Will return 5 only if this parameter is given
mock.GetAge("My Daughter").Returns(5).AndExecute(_ => {})); // Also execute the given action when called
mock.GetAge(Arg<string>.When(s => s.StartsWith("Grand"))).Returns(70); // Will return 70 only for parameters starting with "Grand"

// Exceptions
mock.GetAge("Lemmy Kilmister").Throws<Exception>(); // Will throw a new instance of Exception
mock.GetAge("Lemmy Kilmister").Throws(new Exception("He's dead :(")); // Will throw this specifc exception

// Properties 
// For get/set properties, we need to differentiate getter config from setter config.
mock.GetSetProperty.Get.Returns(42); // Will always return 42
mock.GetSetProperty.Set(42).Execute(val => {}); // Execute given action when setting property to 42

// Task or ValueTask
// Even though GetAgeAsync returns a Task<int> you can configure it directly with an int. Same with ValueTask
mock.GetAgeAsync(Arg<int>.Any).Returns(1)

// Pass the mock to the sut
var sut = new MyServiceUnderTest(mock.Build()); // Build() returns the original type setup with mock behavior

// Assertions on calls 
Assert.Equal(2, mock.GetAge(Arg<string>.Any).NumberOfCalls); // Number of calls to GetAge for any parameter
Assert.Equal(1, mock.GetAge("My Daughter").NumberOfCalls); // Number of calls to GetAge with "My Daughter" parameter
Assert.Equal(1, mock.GetAge(Arg<string>.When(s => s.StartsWith("foo")).NumberOfCalls)); // Number of calls matching predicate

// Concrete class: you have access to constructor overloads and not only a params object[]
var mock = MockG.Generate<ConcreteClass>().New(ctorParam1, ctorParam2);

// Out Parameters
// method we're mocking: 
// bool TryGetById(int id, out Model model) { ... }
mock.TryGetById(Arg<int>.Any).Returns(true); // will returns true and set the out parameter with default value
mock.TryGetById(Arg<int>.Any, (id) => new Model()).Returns(true); // will return true and set the out parameter with new Model()

```

## Closer look


Check the [sample project](https://github.com/thomas-girotto/MockGen/tree/master/sample/MockGen.Sample.Tests) for more details in how to use this lib. 


## Future improvements

- Respect nullable types in generated code when user's lib have them
- Generate documentation
- If you have other ideas, please fill an issue :)
