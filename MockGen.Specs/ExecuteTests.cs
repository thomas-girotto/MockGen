using FluentAssertions;
using System;
using Xunit;

namespace MockGen.Specs
{
    public class ExecuteTests
    {
        [Fact]
        public void Should_execute_callback()
        {
            // Given
            var mockBuilder = Mock.IDependency();
            var wasCallbackExecuted = false;
            mockBuilder.DoSomething().Execute(() => wasCallbackExecuted = true);
            var mock = mockBuilder.Build();

            // When
            mock.DoSomething();

            // Then
            wasCallbackExecuted.Should().BeTrue();
        }

        [Fact]
        public void Should_throw_When_called_after_mock_has_been_built()
        {
            // Given
            var mockBuilder = Mock.IDependency();
            var mock = mockBuilder.Build();

            // When
            Action action1 = () => mockBuilder.DoSomething().Execute(() => { });
            Action action2 = () => mockBuilder.DoSomethingWithParameter(1).Execute((_) => { });
            Action action3 = () => mockBuilder.DoSomethingWithTwoParameters(null, null).Execute((_, _) => { });
            Action action4 = () => mockBuilder.GetSomeNumber().Execute(() => { });
            Action action5 = () => mockBuilder.GetSomeNumberWithParameter(1).Execute((_) => { });
            Action action6 = () => mockBuilder.GetSomeNumberWithTwoParameters(null, null).Execute((_, _) => { });

            // Then
            action1.Should().Throw<InvalidOperationException>();
            action2.Should().Throw<InvalidOperationException>();
            action3.Should().Throw<InvalidOperationException>();
            action4.Should().Throw<InvalidOperationException>();
            action5.Should().Throw<InvalidOperationException>();
            action6.Should().Throw<InvalidOperationException>();
        }
    }
}
