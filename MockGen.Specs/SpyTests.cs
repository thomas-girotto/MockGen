using FluentAssertions;
using System;
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

        [Fact]
        public void Should_throw_When_mock_has_not_been_built()
        {
            // Given
            var mockBuilder = Mock.IDependency();

            // When
            Func<int> spyAction1 = () => mockBuilder.DoSomething().NumberOfCalls;
            Func<int> spyAction2 = () => mockBuilder.DoSomethingWithParameter(1).NumberOfCalls;
            Func<int> spyAction3 = () => mockBuilder.DoSomethingWithTwoParameters(null, null).NumberOfCalls;
            Func<int> spyAction4 = () => mockBuilder.GetSomeNumber().NumberOfCalls;
            Func<int> spyAction5 = () => mockBuilder.GetSomeNumberWithParameter(1).NumberOfCalls;
            Func<int> spyAction6 = () => mockBuilder.GetSomeNumberWithTwoParameters(null, null).NumberOfCalls;

            // Then
            spyAction1.Should().Throw<InvalidOperationException>();
            spyAction2.Should().Throw<InvalidOperationException>();
            spyAction3.Should().Throw<InvalidOperationException>();
            spyAction4.Should().Throw<InvalidOperationException>();
            spyAction5.Should().Throw<InvalidOperationException>();
            spyAction6.Should().Throw<InvalidOperationException>();
        }
    }
}
