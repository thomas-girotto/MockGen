using FluentAssertions;
using MockGen.Specs.Generated.Helpers;
using MockGen.Specs.Generated.IDependencyNs;
using MockGen.Specs.Sut;
using System;
using Xunit;

namespace MockGen.Specs
{
    public class BasicUsageTests
    {
        [Fact]
        public void MethodVoid_Should_spy_number_of_calls()
        {
            // Given
            var mock = Mock<IDependency>.Create();
            var service = new Service(mock.Build());

            // When
            service.ExecuteSomeAction();

            // Then
            mock.DoSomething().Calls.Should().Be(1);
        }

        [Fact]
        public void MethodVoidWithParam_Should_spy_number_of_calls()
        {
            // Given
            var mock = Mock<IDependency>.Create();
            var service = new Service(mock.Build());

            // When
            service.ExecuteSomeActionWithParam(1);
            service.ExecuteSomeActionWithParam(1);
            service.ExecuteSomeActionWithParam(2);

            // Then
            mock.DoSomethingWithParameter(Arg<int>.Any).Calls.Should().Be(3);
            mock.DoSomethingWithParameter(1).Calls.Should().Be(2);
            mock.DoSomethingWithParameter(2).Calls.Should().Be(1);
        }

        [Fact]
        public void MethodVoidWithReferenceTypeParam_Should_handle_null_values()
        {
            // Given
            var mock = Mock<IDependency>.Create();
            var service = new Service(mock.Build());

            // When
            Model1 model = new Model1 { Id = 1 };
            service.ExecuteSomeActionWithReferenceTypeParameter(null);
            service.ExecuteSomeActionWithReferenceTypeParameter(model);

            // Then
            mock.DoSomethingWithReferenceTypeParameter(Arg<Model1>.Any).Calls.Should().Be(2);
            mock.DoSomethingWithReferenceTypeParameter(null).Calls.Should().Be(1);
            mock.DoSomethingWithReferenceTypeParameter(model).Calls.Should().Be(1);
        }

        [Fact]
        public void MethodVoidWithReferenceTypeParam_Should_throw_depending_on_arg_predicate()
        {
            // Given
            var mock = Mock<IDependency>.Create();
            mock.DoSomethingWithReferenceTypeParameter(Arg<Model1>.When(m => m.Id == 1)).Throws<Exception>();
            var service = new Service(mock.Build());

            // When
            Model1 model = new Model1 { Id = 1 };
            Action actionThatShouldThrow = () => service.ExecuteSomeActionWithReferenceTypeParameter(new Model1 { Id = 1 });
            Action actionThatShouldNotThrow = () => service.ExecuteSomeActionWithReferenceTypeParameter(new Model1 { Id = 2 });

            // Then
            actionThatShouldThrow.Should().Throw<Exception>();
            actionThatShouldNotThrow.Should().NotThrow();
            mock.DoSomethingWithReferenceTypeParameter(Arg<Model1>.Any).Calls.Should().Be(2);
            mock.DoSomethingWithReferenceTypeParameter(Arg<Model1>.When(m => m.Id == 1)).Calls.Should().Be(1);
            mock.DoSomethingWithReferenceTypeParameter(Arg<Model1>.When(m => m.Id == 2)).Calls.Should().Be(1);
        }

        [Fact]
        public void MethodVoidWithTwoParameters_Should_throw_dependening_on_both_args_predicate()
        {
            // Given
            var mock = Mock<IDependency>.Create();
            mock.DoSomethingWithTwoParameters(Arg<Model1>.When(m => m.Id == 1), Arg<Model2>.When(m => m.Name == "Throw")).Throws<Exception>();
            var service = new Service(mock.Build());

            // When
            Action action1 = () => service.ExecuteSomeActionWithTwoParameters(new Model1 { Id = 1 }, new Model2 { Name = "DontThrow" });
            Action action2 = () => service.ExecuteSomeActionWithTwoParameters(new Model1 { Id = 2 }, new Model2 { Name = "Throw" });
            Action action3 = () => service.ExecuteSomeActionWithTwoParameters(new Model1 { Id = 1 }, new Model2 { Name = "Throw" });

            // Then
            action1.Should().NotThrow();
            action2.Should().NotThrow();
            action3.Should().Throw<Exception>();
            mock.DoSomethingWithTwoParameters(Arg<Model1>.Any, Arg<Model2>.Any).Calls.Should().Be(3);
            mock.DoSomethingWithTwoParameters(Arg<Model1>.When(m => m.Id == 1), Arg<Model2>.Any).Calls.Should().Be(2);
            mock.DoSomethingWithTwoParameters(Arg<Model1>.Any, Arg<Model2>.When(m => m.Name == "Throw")).Calls.Should().Be(2);
            mock.DoSomethingWithTwoParameters(Arg<Model1>.When(m => m.Id == 1), Arg<Model2>.When(m => m.Name == "DontThrow")).Calls.Should().Be(1);
            mock.DoSomethingWithTwoParameters(Arg<Model1>.When(m => m.Id == 2), Arg<Model2>.When(m => m.Name == "Throw")).Calls.Should().Be(1);
            mock.DoSomethingWithTwoParameters(Arg<Model1>.When(m => m.Id == 1), Arg<Model2>.When(m => m.Name == "Throw")).Calls.Should().Be(1);
            mock.DoSomethingWithTwoParameters(Arg<Model1>.When(m => m.Id == 999), Arg<Model2>.When(m => m.Name == "Throw")).Calls.Should().Be(0);
        }

        [Fact]
        public void MethodTReturn_Should_return_given_value_When_mocked()
        {
            // Given
            var mock = Mock<IDependency>.Create();
            mock.GetSomeNumber().Returns(2);
            var service = new Service(mock.Build());

            // When
            var result = service.ReturnDependencyNumber();

            // Then
            result.Should().Be(2);
        }

        [Fact]
        public void MethodTReturn_Should_return_default_value_When_not_mocked()
        {
            // Given
            var mock = Mock<IDependency>.Create();
            var service = new Service(mock.Build());

            // When
            var result = service.ReturnDependencyNumber();

            // Then
            result.Should().Be(0);
        }

        [Fact]
        public void MethodTReturn_Should_spy_the_number_of_calls_to_the_mocked_method()
        {
            // Given
            var mock = Mock<IDependency>.Create();
            mock.GetSomeNumber().Returns(2);
            var service = new Service(mock.Build());

            // When
            service.ReturnDependencyNumber();

            // Then
            mock.GetSomeNumber().Calls.Should().Be(1);
        }

        [Fact]
        public void MethodTReturnTParam_Should_return_given_number_for_given_param()
        {
            // Given
            var mock = Mock<IDependency>.Create();
            mock.GetSomeNumberWithParameter(1).Returns(2);
            mock.GetSomeNumberWithParameter(3).Returns(4);
            var service = new Service(mock.Build());

            // When
            var result1 = service.ReturnDependencyNumberWithParam(1);
            var result2 = service.ReturnDependencyNumberWithParam(3);

            // Then
            result1.Should().Be(2);
            result2.Should().Be(4);
        }

        [Fact]
        public void MethodTReturnTParam_Should_return_param_given_for_Any_When_param_doesnt_match_a_specific_one()
        {
            // Given
            var mock = Mock<IDependency>.Create();
            mock.GetSomeNumberWithParameter(Arg<int>.Any).Returns(1); // The default returned value
            mock.GetSomeNumberWithParameter(10).Returns(11); // 11 only if given param is 10
            var service = new Service(mock.Build());

            // When
            var result1 = service.ReturnDependencyNumberWithParam(1);
            var result2 = service.ReturnDependencyNumberWithParam(2);
            var result3 = service.ReturnDependencyNumberWithParam(10);

            // Then
            result1.Should().Be(1);
            result2.Should().Be(1);
            result3.Should().Be(11);
        }

        [Fact]
        public void Throw_should_always_throw_When_no_parameter()
        {
            // Given
            var mock = Mock<IDependency>.Create();
            mock.DoSomething().Throws<Exception>();
            var service = new Service(mock.Build());

            // When
            Action action = () => service.ExecuteSomeAction();

            // Then
            action.Should().Throw<Exception>();
        }

        [Fact]
        public void Throw_for_parameter_should_only_throws_When_this_parameter_is_given()
        {
            // Given
            var mock = Mock<IDependency>.Create();
            mock.GetSomeNumberWithParameter(1).Returns(2);
            mock.GetSomeNumberWithParameter(3).Throws<Exception>();
            var service = new Service(mock.Build());

            // When
            Func<int> actionThatShouldNotThrow = () => service.ReturnDependencyNumberWithParam(1);
            Func<int> actionThatShouldThrow = () => service.ReturnDependencyNumberWithParam(3);

            // Then
            actionThatShouldNotThrow.Should().NotThrow();
            actionThatShouldThrow.Should().Throw<Exception>();
        }
    }
}
