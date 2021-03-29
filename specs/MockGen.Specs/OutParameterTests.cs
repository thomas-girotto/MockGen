using FluentAssertions;
using MockGen.Specs.Sut;
using Xunit;

namespace MockGen.Specs
{
    public class OutParameterTests
    {
        [Fact]
        public void Should_default_out_parameter_When_not_explicitly_setup()
        {
            // Given
            var mockBuilder = Mock.IDependency();
            mockBuilder.TryGetById(Arg<int>.Any).Returns(true);
            var mock = mockBuilder.Build();

            // When
            var success = mock.TryGetById(1, out var param1);

            // Then
            success.Should().BeTrue();
            param1.Should().BeNull();
        }

        [Fact]
        public void Should_setup_out_parameter_For_any_parameter()
        {
            // Given
            var mockBuilder = Mock.IDependency();
            var model = new Model1 { Id = 1 };
            mockBuilder.TryGetById(Arg<int>.Any, () => model).Returns(true);
            var mock = mockBuilder.Build();

            // When
            var success = mock.TryGetById(1, out var outParam);

            // Then
            success.Should().BeTrue();
            outParam.Should().Be(model);
        }

        [Fact]
        public void Should_setup_out_parameter_depending_on_input_parameter()
        {
            // Given
            var mockBuilder = Mock.IDependency();
            var model1 = new Model1 { Id = 1 };
            var model2 = new Model1 { Id = 2 };
            mockBuilder.TryGetById(Arg<int>.Any, (id) => id == 1 ? model1 : model2).Returns(true);
            var mock = mockBuilder.Build();

            // When
            var success1 = mock.TryGetById(1, out var outParam1);
            var success2 = mock.TryGetById(2, out var outParam2);

            // Then
            success1.Should().BeTrue();
            outParam1.Should().Be(model1);
            success2.Should().BeTrue();
            outParam2.Should().Be(model2);
        }

        [Fact]
        public void Should_setup_out_parameters_as_tuple_When_several_out_parameters()
        {
            // Given
            var mockBuilder = Mock.IDependency();
            var model1 = new Model1 { Id = 1 };
            var model2 = new Model2 { Name = "foo" };
            mockBuilder.TryGetById(Arg<int>.Any, () => (model1, model2)).Returns(true);
            var mock = mockBuilder.Build();

            // When
            var success = mock.TryGetById(1, out var outParam1, out var outParam2);

            // Then
            success.Should().BeTrue();
            outParam1.Should().Be(model1);
            outParam2.Should().Be(model2);
        }
    }
}
