using FluentAssertions;
using MockGen.Specs.Sut;
using Xunit;

namespace MockGen.Specs
{
    public class ConcreteClassTests
    {
        [Fact]
        public void Should_mock_virtual_method_from_concrete_class()
        {
            // Given
            var mockBuilder = Mock.ConcreteDependency();
            mockBuilder.ICanBeMocked().Returns(2);
            var mock = mockBuilder.Build();

            // When
            var result = mock.ICanBeMocked();

            // Then
            mock.Should().BeAssignableTo<ConcreteDependency>();
            result.Should().Be(2);
        }

        [Fact]
        public void Should_pass_parameters_in_the_right_mock_ctor_overload()
        {
            // Given
            Model1 model1 = new Model1 { Id = 1 };
            Model2 model2 = new Model2 { Name = "John" };
            var mockBuilder1 = Mock.ConcreteDependency();
            var mockBuilder2 = Mock.ConcreteDependency(model1);
            var mockBuilder3 = Mock.ConcreteDependency(model1, model2);

            // When
            var mock1 = mockBuilder1.Build();
            var mock2 = mockBuilder2.Build();
            var mock3 = mockBuilder3.Build();

            // Then
            mock1.M1.Should().BeNull();
            mock1.M2.Should().BeNull();
            mock2.M1.Should().Be(model1);
            mock2.M2.Should().BeNull();
            mock3.M1.Should().Be(model1);
            mock3.M2.Should().Be(model2);
        }

        [Fact]
        public void Should_mock_protected_method()
        {
            var mockBuilder = Mock.ConcreteDependency();
            var mock = mockBuilder.Build();

            // When
            mock.AddLastNameAndSave("Firstname");

            // Then
            mockBuilder.SaveFullName("Firstname Lastname").NumberOfCalls.Should().Be(1);
        }
    }
}
