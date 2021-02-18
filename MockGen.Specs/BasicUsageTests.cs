using FluentAssertions;
using MockGen.Specs.Generated;
using MockGen.Specs.Sut;
using Xunit;

namespace MockGen.Specs
{
    public class BasicUsageTests
    {
        [Fact]
        public void Should_return_given_value_When_mocked()
        {
            // Given
            var mock = Mock<IDependency>.IDependency();
            mock.GetSomeNumber().WillReturn(2);
            var service = new Service(mock.Build());

            // When
            var result = service.ReturnDependencyNumber();

            // Then
            result.Should().Be(2);
        }

        [Fact]
        public void Should_return_default_value_When_not_mocked()
        {
            // Given
            var mock = Mock<IDependency>.IDependency();
            var service = new Service(mock.Build());

            // When
            var result = service.ReturnDependencyNumber();

            // Then
            result.Should().Be(0);
        }

        [Fact]
        public void Should_spy_the_number_of_calls_to_the_mocked_method()
        {
            // Given
            var mock = Mock<IDependency>.IDependency();
            mock.GetSomeNumber().WillReturn(2);
            var service = new Service(mock.Build());

            // When
            service.ReturnDependencyNumber();

            // Then
            mock.GetSomeNumber().Calls.Should().Be(1);
        }
    }
}
