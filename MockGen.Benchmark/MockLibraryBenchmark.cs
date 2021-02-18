using BenchmarkDotNet.Attributes;
using FluentAssertions;
using MockGen.Specs.Generated;
using MockGen.Specs.Sut;
using NSubstitute;

namespace MockGen.Benchmark
{
    public class MockLibraryBenchmark
    {
        [Benchmark]
        public void MockGenMock()
        {
            var mockGen = Mock<IDependency>.IDependency();
            mockGen.GetSomeNumber().WillReturn(2);
            var service = new Service(mockGen.Build());
            service.ReturnDependencyNumber();
            mockGen.GetSomeNumber().Calls.Should().Be(1);
        }

        [Benchmark]
        public void NSubstitute()
        {
            var substitute = Substitute.For<IDependency>();
            substitute.GetSomeNumber().Returns(2);
            var service = new Service(substitute);
            service.ReturnDependencyNumber();
            substitute.Received(1).GetSomeNumber();
        }
    }
}
