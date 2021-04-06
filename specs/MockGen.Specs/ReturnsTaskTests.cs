using FluentAssertions;
using System.Threading.Tasks;
using Xunit;

namespace MockGen.Specs
{
    public class ReturnsTaskTests
    {
        [Fact]
        public async Task Should_return_a_Task_When_configured_with_the_underlying_type()
        {
            // Given
            var mock = Mock.IDependency();
            mock.GetSomeNumberAsync().Returns(1);

            // When
            var result = await mock.Build().GetSomeNumberAsync();

            // Then
            result.Should().Be(1);
        }

        [Fact]
        public async Task Should_return_a_ValueTask_When_configured_with_the_underlying_type()
        {
            // Given
            var mock = Mock.IDependency();
            mock.GetSomeNumberWithValueTaskAsync().Returns(1);

            // When
            var result = await mock.Build().GetSomeNumberWithValueTaskAsync();

            // Then
            result.Should().Be(1);
        }
    }
}
