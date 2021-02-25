using FluentAssertions;
using MockGen.Specs.Generated.Helpers;
using MockGen.Specs.Generated.IDependencyNs;
using MockGen.Specs.Sut;
using System;
using Xunit;

namespace MockGen.Specs
{
    public class ThrowsTests
    {
        [Fact]
        public void Should_always_throw_When_no_parameter()
        {
            // Given
            var mockBuilder = Mock<IDependency>.Create();
            mockBuilder.DoSomething().Throws<Exception>();
            var mock = mockBuilder.Build();

            // When
            Action action = () => mock.DoSomething();

            // Then
            action.Should().Throw<Exception>();
        }

        [Fact]
        public void Should_throw_a_specific_exception_instance()
        {
            // Given
            var mockBuilder = Mock<IDependency>.Create();
            var exception = new Exception();
            mockBuilder.DoSomething().Throws(exception);
            var mock = mockBuilder.Build();

            // When
            Action action = () => mock.DoSomething();

            // Then
            action.Should().Throw<Exception>().Which.Should().Be(exception);
        }

        [Fact]
        public void Should_only_throw_for_matching_parameter_When_configured_for_a_specific_parameter()
        {
            // Given
            var mockBuilder = Mock<IDependency>.Create();
            mockBuilder.GetSomeNumberWithParameter(1).Returns(2);
            mockBuilder.GetSomeNumberWithParameter(3).Throws<Exception>();
            var mock = mockBuilder.Build();

            // When
            Func<int> actionThatShouldNotThrow = () => mock.GetSomeNumberWithParameter(1);
            Func<int> actionThatShouldThrow = () => mock.GetSomeNumberWithParameter(3);

            // Then
            actionThatShouldNotThrow.Should().NotThrow();
            actionThatShouldThrow.Should().Throw<Exception>();
        }

        [Fact]
        public void Should_only_throw_for_matching_parameter_When_configured_with_parameter_predicate()
        {
            // Given
            var mockBuilder = Mock<IDependency>.Create();
            mockBuilder.DoSomethingWithTwoParameters(Arg<Model1>.When(m => m.Id == 1), Arg<Model2>.When(m => m.Name == "Throw")).Throws<Exception>();
            var mock = mockBuilder.Build();

            // When
            Action action1 = () => mock.DoSomethingWithTwoParameters(new Model1 { Id = 1 }, new Model2 { Name = "DontThrow" });
            Action action2 = () => mock.DoSomethingWithTwoParameters(new Model1 { Id = 2 }, new Model2 { Name = "Throw" });
            Action action3 = () => mock.DoSomethingWithTwoParameters(new Model1 { Id = 1 }, new Model2 { Name = "Throw" });

            // Then
            action1.Should().NotThrow();
            action2.Should().NotThrow();
            action3.Should().Throw<Exception>();
        }
    }
}
