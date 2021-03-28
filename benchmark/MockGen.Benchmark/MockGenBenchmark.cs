using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using MockGen.Specs.Sut;
using NSubstitute;
using System.Linq;
using Xunit;

namespace MockGen.Benchmark
{
    [SimpleJob, GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory), CategoriesColumn]
    public class MockGenBenchmark
    {
        private IDependencyMockBuilder mockGen;
        private IDependency nSubstitute;
        private Moq.Mock<IDependency> moq;

        #region Returns Benchmark

        [Benchmark(Baseline = true, Description = "MockGen"), BenchmarkCategory("Returns")]
        public int MockGenReturnsBench()
        {
            var mockGen = Mock.IDependency();
            mockGen.GetSomeNumber().Returns(2);
            var mock = mockGen.Build();
            return mock.GetSomeNumber();
        }

        [Benchmark(Description = "NSubstitute"), BenchmarkCategory("Returns")]
        public int NSubstituteReturnsBench()
        {
            var substitute = Substitute.For<IDependency>();
            substitute.GetSomeNumber().Returns(2);
            substitute.GetSomeNumber();
            return substitute.Received(1).GetSomeNumber();
        }

        [Benchmark(Description = "Moq"), BenchmarkCategory("Returns")]
        public int MoqReturnsBench()
        {
            var moq = new Moq.Mock<IDependency>();
            moq.Setup(d => d.GetSomeNumber()).Returns(2);
            return moq.Object.GetSomeNumber();
        }

        #endregion

        #region Assert Benchmark

        [GlobalSetup(Target = nameof(MockGenAssertBench))]
        public void MockGenAssertOnOneCallSetup()
        {
            mockGen = Mock.IDependency();
            mockGen.GetSomeNumberWithParameter(1).Returns(2);
            var mock = mockGen.Build();
            for (int i = 0; i < 1000; i++)
            {
                mock.GetSomeNumberWithParameter(i);
            }
        }

        [Benchmark(Baseline = true, Description = "MockGen"), BenchmarkCategory("Assert")]
        public void MockGenAssertBench()
        {
            Assert.Equal(1000, mockGen.GetSomeNumberWithParameter(Arg<int>.Any).NumberOfCalls);
        }

        [GlobalSetup(Target = nameof(NSubstituteAssertBench))]
        public void NSubstituteAssertOnOneCallSetup()
        {
            nSubstitute = Substitute.For<IDependency>();
            nSubstitute.GetSomeNumberWithParameter(1).Returns(2);
            for (int i = 0; i < 1000; i++)
            {
                nSubstitute.GetSomeNumberWithParameter(i);
            }
        }

        [Benchmark(Description = "NSubstitute"), BenchmarkCategory("Assert")]
        public void NSubstituteAssertBench()
        {
            nSubstitute.Received(1000).GetSomeNumberWithParameter(Arg.Any<int>());
        }

        #endregion
        [GlobalSetup(Target = nameof(MoqAssertBench))]
        public void MoqAssertOnOneCallSetup()
        {
            moq = new Moq.Mock<IDependency>();
            moq.Setup(m => m.GetSomeNumberWithParameter(1)).Returns(2);
            for (int i = 0; i < 1000; i++)
            {
                moq.Object.GetSomeNumberWithParameter(i);
            }
        }

        [Benchmark(Description = "Moq"), BenchmarkCategory("Assert")]
        public void MoqAssertBench()
        {
            moq.Verify(m => m.GetSomeNumberWithParameter(Moq.It.IsAny<int>()), Moq.Times.Exactly(1000));
        }
    }
}
