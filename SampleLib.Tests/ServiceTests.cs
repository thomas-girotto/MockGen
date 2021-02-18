using Xunit;
using MockGen.Specs.Generated;
using FluentAssertions;

namespace SampleLib.Tests
{
    public class ServiceTests
    {
        [Fact]
        public void Should_add_number_from_external_source_to_given_number()
        {
            IExternalDependencyMockBuilder mock = Mock<IExternalDependency>.IExternalDependency();
            mock.GetSomeNumber().WillReturn(2);

            var service = new Service(mock.Build());
            var number = service.ReturnDependencyNumber();

            number.Should().Be(2);
            mock.GetSomeNumber().Calls.Should().Be(1);
        }
    }
}
