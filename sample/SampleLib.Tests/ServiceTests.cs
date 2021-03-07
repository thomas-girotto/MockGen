using Xunit;
using MockGen;
using FluentAssertions;

namespace SampleLib.Tests
{
    public class ServiceTests
    {
        [Fact]
        public void Should_add_number_from_external_source_to_given_number()
        {
            var mock = Mock.IExternalDependency();
            mock.GetSomeNumber().Returns(2);

            var service = new Service(mock.Build());
            var number = service.ReturnDependencyNumber();

            number.Should().Be(2);
            mock.GetSomeNumber().NumberOfCalls.Should().Be(1);
        }

        [Fact]
        public void MethodVoidWithReferenceTypeParam_Should_handle_null_values()
        {
            var mock = Mock.IExternalDependency();
            var model = new Model { Id = 1 };
            var service = new Service(mock.Build());
            service.DoSomething(model);
            service.DoSomething(null);

            mock.DoSomething(Arg<Model>.Any).NumberOfCalls.Should().Be(2);
            mock.DoSomething(null).NumberOfCalls.Should().Be(1);
            mock.DoSomething(model).NumberOfCalls.Should().Be(1);
        }
    }
}
