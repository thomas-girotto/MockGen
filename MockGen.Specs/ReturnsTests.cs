using FluentAssertions;
using MockGen.Specs.Generated.Helpers;
using MockGen.Specs.Generated.IDependencyNs;
using MockGen.Specs.Sut;
using Xunit;

namespace MockGen.Specs
{
    public class ReturnsTests
    {
        [Fact]
        public void Should_return_default_value_When_nothing_configured()
        {
            // Given
            var mockBuilder = Mock<IDependency>.Create();
            var mock = mockBuilder.Build();

            // When
            var result = mock.GetSomeNumber();

            // Then
            result.Should().Be(0);
        }

        [Fact]
        public void Should_always_return_mocked_value_When_no_parameters_in_mocked_method()
        {
            // Given
            var mockBuilder = Mock<IDependency>.Create();
            mockBuilder.GetSomeNumber().Returns(2);
            var mock = mockBuilder.Build();

            // When
            var result = mock.GetSomeNumber();

            // Then
            result.Should().Be(2);
        }

        [Fact]
        public void Should_return_per_parameter_mocked_value_When_parameter_match_value()
        {
            // Given
            var mockBuilder = Mock<IDependency>.Create();
            mockBuilder.GetSomeNumberWithParameter(1).Returns(2);
            mockBuilder.GetSomeNumberWithParameter(3).Returns(4);
            var mock = mockBuilder.Build();

            // When
            var result1 = mock.GetSomeNumberWithParameter(1);
            var result2 = mock.GetSomeNumberWithParameter(3);

            // Then
            result1.Should().Be(2);
            result2.Should().Be(4);
        }

        [Fact]
        public void Should_return_mocked_value_When_configured_for_null()
        {
            // Given
            var mockBuilder = Mock<IDependency>.Create();
            mockBuilder.GetSomeNumberWithReferenceTypeParameter(null).Returns(2);
            var mock = mockBuilder.Build();

            // When
            var result1 = mock.GetSomeNumberWithReferenceTypeParameter(null);
            var result2 = mock.GetSomeNumberWithReferenceTypeParameter(new Model1 { Id = 1 });

            // Then
            result1.Should().Be(2);
            result2.Should().Be(0);
        }

        [Fact]
        public void Should_return_per_parameter_mocked_value_When_parameter_match_predicate()
        {
            // Given
            var mockBuilder = Mock<IDependency>.Create();
            mockBuilder.GetSomeNumberWithReferenceTypeParameter(Arg<Model1>.When(m => m.Id == 1)).Returns(2);
            var mock = mockBuilder.Build();

            // When
            var result1 = mock.GetSomeNumberWithReferenceTypeParameter(new Model1 { Id = 1 });

            // Then
            result1.Should().Be(2);
        }

        [Fact]
        public void Should_return_per_parameter_mocked_value_When_parameter_match_equality_by_reference()
        {
            // Given
            var mockBuilder = Mock<IDependency>.Create();
            var model = new Model1 { Id = 1 };
            var anotherInstance = new Model1 { Id = 1 };
            mockBuilder.GetSomeNumberWithReferenceTypeParameter(model).Returns(2);
            var mock = mockBuilder.Build();

            // When
            var result1 = mock.GetSomeNumberWithReferenceTypeParameter(model);
            var result2 = mock.GetSomeNumberWithReferenceTypeParameter(anotherInstance);

            // Then
            result1.Should().Be(2);
            result2.Should().Be(0);
        }

        [Fact]
        public void Should_fallback_to_mocked_value_for_Any_When_parameter_doesnt_match_anything_else()
        {
            // Given
            var mockBuilder = Mock<IDependency>.Create();
            var mockedForAny = 1;
            var mockedFor10Only = 11;
            mockBuilder.GetSomeNumberWithParameter(Arg<int>.Any).Returns(mockedForAny); // The default returned value
            mockBuilder.GetSomeNumberWithParameter(10).Returns(mockedFor10Only); // 11 only if given param is 10
            var mock = mockBuilder.Build();

            // When
            var result1 = mock.GetSomeNumberWithParameter(1);
            var result2 = mock.GetSomeNumberWithParameter(2);
            var result3 = mock.GetSomeNumberWithParameter(10);

            // Then
            result1.Should().Be(mockedForAny);
            result2.Should().Be(mockedForAny);
            result3.Should().Be(mockedFor10Only);
        }

        [Fact]
        public void Should_fallback_to_default_When_parameter_doesnt_match_anything_and_no_Any_configured()
        {
            // Given
            var mockBuilder = Mock<IDependency>.Create();
            var mockedFor10Only = 11;
            mockBuilder.GetSomeNumberWithParameter(10).Returns(mockedFor10Only); // 11 only if given param is 10
            var mock = mockBuilder.Build();

            // When
            var result1 = mock.GetSomeNumberWithParameter(1);
            var result2 = mock.GetSomeNumberWithParameter(10);

            // Then
            result1.Should().Be(0);
            result2.Should().Be(mockedFor10Only);
        }

        [Fact]
        public void Should_return_per_parameters_mocked_value_When_parameters_match_both_criterias()
        {
            // Given
            var mockBuilder = Mock<IDependency>.Create();
            var model1 = new Model1 { Id = 1 };
            var model2 = new Model2 { Name = "test" };
            mockBuilder.GetSomeNumberWithTwoParameters(model1, model2).Returns(10);
            mockBuilder.GetSomeNumberWithTwoParameters(Arg<Model1>.When(m => m.Id == 2), Arg<Model2>.Any).Returns(11);
            mockBuilder.GetSomeNumberWithTwoParameters(Arg<Model1>.When(m => m.Id == 3), Arg<Model2>.When(m => m.Name == "name")).Returns(12);

            var mock = mockBuilder.Build();

            // When
            var result1 = mock.GetSomeNumberWithTwoParameters(model1, model2);
            var result2 = mock.GetSomeNumberWithTwoParameters(new Model1 { Id = 2 }, model2);
            var result3 = mock.GetSomeNumberWithTwoParameters(new Model1 { Id = 3 }, new Model2 { Name = "name" });
            var result4 = mock.GetSomeNumberWithTwoParameters(new Model1 { Id = 3 }, new Model2 { Name = "wrongName" });

            // Then
            result1.Should().Be(10);
            result2.Should().Be(11);
            result3.Should().Be(12);
            result4.Should().Be(0);
        }
    }
}
