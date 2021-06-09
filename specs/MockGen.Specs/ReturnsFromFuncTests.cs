using FluentAssertions;
using MockGen.Specs.Sut;
using Xunit;

namespace MockGen.Specs
{
    public class ReturnsFromFuncTests
    {
        [Fact]
        public void Should_return_from_func()
        {
            // Given
            var mockBuilder = MockG.Generate<IDependency>().New();
            var valueToReturn = 1;
            mockBuilder.GetSomeNumber().Returns(() => valueToReturn);
            var mock = mockBuilder.Build();

            // When
            var result1 = mock.GetSomeNumber();
            valueToReturn = 2;
            var result2 = mock.GetSomeNumber();

            // Then
            result1.Should().Be(1);
            result2.Should().Be(2);
        }

        [Fact]
        public void Should_return_from_func_with_params()
        {
            // Given
            var mockBuilder = MockG.Generate<IDependency>().New();
            mockBuilder.GetSomeNumberWithTwoParameters(Arg<Model1>.Any, Arg<Model2>.Any)
                .Returns((m1, m2) => m1.Id + m2.Name.Length);
            var mock = mockBuilder.Build();
            var m1 = new Model1 { Id = 1 };
            var m2 = new Model2 { Name = "foo" };

            // When
            var result = mock.GetSomeNumberWithTwoParameters(m1, m2);

            // Then
            result.Should().Be(4);
        }
    }
}
