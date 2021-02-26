using BenchmarkDotNet.Attributes;
using FluentAssertions;
using MockGen.IDependencyNs;
using MockGen.Specs.Sut;
using NSubstitute;

namespace MockGen.Benchmark
{
    public class MockLibraryBenchmark
    {
        [Benchmark(Baseline = true, Description = "MockGen")]
        public void MockGenBench()
        {
            var mockGen = Mock<IDependency>.Create();
            mockGen.GetSomeNumber().Returns(2);
            var mock = mockGen.Build();
            mock.GetSomeNumber();
            mockGen.GetSomeNumber().Calls.Should().Be(1);
        }

        [Benchmark(Description = "NSubstitute")]
        public void NSubstituteBench()
        {
            var substitute = Substitute.For<IDependency>();
            substitute.GetSomeNumber().Returns(2);
            substitute.GetSomeNumber();
            substitute.Received(1).GetSomeNumber();
        }

        [Benchmark(Description = "Moq")]
        public void MoqBench()
        {
            var moq = new Moq.Mock<IDependency>();
            moq.Setup(d => d.GetSomeNumber()).Returns(2);
            moq.Object.GetSomeNumber();
            moq.Verify(d => d.GetSomeNumber(), Moq.Times.Once);
        }
    }
}
