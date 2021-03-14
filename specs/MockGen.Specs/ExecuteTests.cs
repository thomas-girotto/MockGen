using FluentAssertions;
using MockGen.Specs.Sut;
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
        public void Should_execute_callback_When_chained_after_returns_method()
        {
            // Given
            var mockBuilder = Mock.IDependency();
            Model1 param1 = new Model1 { Id = 1 };
            Model2 param2 = new Model2 { Name = "foo" };
            Model1? callbackParam1 = null;
            Model2? callbackParam2 = null;
            mockBuilder.GetSomeNumberWithTwoParameters(Arg<Model1>.Any, Arg<Model2>.Any).Returns(10).AndExecute((m1, m2) =>
            {
                callbackParam1 = m1;
                callbackParam2 = m2;
            });
            var mock = mockBuilder.Build();

            // When
            mock.GetSomeNumberWithTwoParameters(param1, param2);

            // Then
            callbackParam1.Should().Be(param1);
            callbackParam2.Should().Be(param2);
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
