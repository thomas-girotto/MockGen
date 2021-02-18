using FluentAssertions;
using MockGen.Specs.Generated.Helpers;
using MockGen.Specs.Generated.IDependencyNs;
using MockGen.Specs.Sut;
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
        public void MethodTReturn_Should_return_given_value_When_mocked()
        {
            // Given
            var mock = Mock<IDependency>.Create();
            mock.GetSomeNumber().WillReturn(2);
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
            mock.GetSomeNumber().WillReturn(2);
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
            mock.GetSomeNumberWithParameter(1).WillReturn(2);
            mock.GetSomeNumberWithParameter(3).WillReturn(4);
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
            mock.GetSomeNumberWithParameter(Arg<int>.Any).WillReturn(1); // The default returned value
            mock.GetSomeNumberWithParameter(10).WillReturn(11); // 11 only if given param is 10
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
    }
}
