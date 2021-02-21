using Xunit;
using MockGen.Specs.Generated.IExternalDependencyNs;
using MockGen.Specs.Generated.Helpers;
using FluentAssertions;

namespace SampleLib.Tests
{
    public class ServiceTests
    {
        [Fact]
        public void Should_add_number_from_external_source_to_given_number()
        {
            var mock = Mock<IExternalDependency>.Create();
            mock.GetSomeNumber().WillReturn(2);

            var service = new Service(mock.Build());
            var number = service.ReturnDependencyNumber();

            number.Should().Be(2);
            mock.GetSomeNumber().Calls.Should().Be(1);
        }

        [Fact]
        public void MethodVoidWithReferenceTypeParam_Should_handle_null_values()
        {
            var mock = Mock<IExternalDependency>.Create();
            var model = new Model { Id = 1 };
            var service = new Service(mock.Build());
            service.DoSomething(model);
            service.DoSomething(null);

            mock.DoSomething(Arg<Model>.Any).Calls.Should().Be(2);
            mock.DoSomething(null).Calls.Should().Be(1);
            mock.DoSomething(model).Calls.Should().Be(1);
        }
    }
}
