using FluentAssertions;
using MockGen.Specs.Sut;
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
            var mock = MockG.Generate<ITaskDependency>().New();
            mock.GetSomeNumberTaskAsync().Returns(1);

            // When
            var result = await mock.Build().GetSomeNumberTaskAsync();

            // Then
            result.Should().Be(1);
        }

        [Fact]
        public async Task Should_return_a_ValueTask_When_configured_with_the_underlying_type()
        {
            // Given
            var mock = MockG.Generate<ITaskDependency>().New();
            mock.GetSomeNumberValueTaskAsync().Returns(1);

            // When
            var result = await mock.Build().GetSomeNumberValueTaskAsync();

            // Then
            result.Should().Be(1);
        }
    }
}
