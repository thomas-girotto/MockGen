using FluentAssertions;
using Xunit;

namespace MockGen.Specs
{
    public class SpyTests
    {
        [Fact]
        public void Should_spy_number_of_calls()
        {
            // Given
            var mockBuilder = Mock.IDependency();
            var mock = mockBuilder.Build();
            
            // When
            mock.DoSomething();

            // Then
            mockBuilder.DoSomething().NumberOfCalls.Should().Be(1);
        }

        [Fact]
        public void Should_spy_number_of_calls_depending_on_parameter()
        {
            // Given
            var mockBuilder = Mock.IDependency();
            var mock = mockBuilder.Build();

            // When
            mock.DoSomethingWithParameter(1);
            mock.DoSomethingWithParameter(1);
            mock.DoSomethingWithParameter(2);

            // Then
            mockBuilder.DoSomethingWithParameter(Arg<int>.Any).NumberOfCalls.Should().Be(3);
            mockBuilder.DoSomethingWithParameter(1).NumberOfCalls.Should().Be(2);
            mockBuilder.DoSomethingWithParameter(2).NumberOfCalls.Should().Be(1);
        }
    }
}
