using FluentAssertions;
using MockGen.Specs.Sut;
using Xunit;

namespace MockGen.Specs
{
    public class PropertyTests
    {
        [Fact]
        public void Should_mock_return_value_for_get_only_property()
        {
            // Given
            var mockBuilder = Mock.IDependency();
            var propertyValue = new Model1 { Id = 1 };
            mockBuilder.GetOnlyProperty.Get.Returns(propertyValue);
            var mock = mockBuilder.Build();

            // When
            var result = mock.GetOnlyProperty;

            // Then
            result.Should().Be(propertyValue);
        }

        [Fact]
        public void Should_execute_callback_When_get_only_property_accessed()
        {
            // Given
            var mockBuilder = Mock.IDependency();
            var propertyValue = new Model1 { Id = 1 };
            var wasCalled = false;
            mockBuilder.GetOnlyProperty.Get.Execute(() => wasCalled = true);
            var mock = mockBuilder.Build();

            // When
            var result = mock.GetOnlyProperty;

            // Then
            wasCalled.Should().BeTrue();
        }

        [Fact]
        public void Should_count_number_of_calls_to_get_only_property()
        {
            // Given
            var mockBuilder = Mock.IDependency();
            var mock = mockBuilder.Build();

            // When
            var firstCallResult = mock.GetOnlyProperty;
            var secondCallResult = mock.GetOnlyProperty;

            // Then
            mockBuilder.GetOnlyProperty.Get.NumberOfCalls.Should().Be(2);
        }

        [Fact]
        public void Should_execute_callback_When_set_only_property_accessed()
        {
            // Given
            var mockBuilder = Mock.IDependency();
            var wasCalled = false;
            mockBuilder.SetOnlyProperty.Set(Arg<Model1>.Any).Execute(_ => wasCalled = true);
            var mock = mockBuilder.Build();

            // When
            mock.SetOnlyProperty = new Model1();

            // Then
            wasCalled.Should().BeTrue();
        }

        [Fact]
        public void Should_count_number_of_calls_to_set_only_property()
        {
            // Given
            var mockBuilder = Mock.IDependency();
            var mock = mockBuilder.Build();
            var model = new Model1 { Id = 1 };

            // When
            mock.SetOnlyProperty = model;
            mock.SetOnlyProperty = model;

            // Then
            mockBuilder.SetOnlyProperty.Set(Arg<Model1>.Any).NumberOfCalls.Should().Be(2);
        }

        [Fact]
        public void Should_mock_return_value_for_get_set_property()
        {
            // Given
            var mockBuilder = Mock.IDependency();
            var propertyValue = new Model1 { Id = 1 };
            mockBuilder.GetSetProperty.Get.Returns(propertyValue);
            var mock = mockBuilder.Build();

            // When
            var result = mock.GetSetProperty;

            // Then
            result.Should().Be(propertyValue);
        }

        [Fact]
        public void Should_execute_callback_When_get_set_property_accessed()
        {
            // Given
            var mockBuilder = Mock.IDependency();
            var propertyValue = new Model1 { Id = 1 };
            var getWasCalled = false;
            var setWasCalled = false;
            mockBuilder.GetSetProperty.Get.Execute(() => getWasCalled = true);
            mockBuilder.GetSetProperty.Set(Arg<Model1>.Any).Execute(_ => setWasCalled = true);
            var mock = mockBuilder.Build();

            // When
            var result = mock.GetSetProperty;
            mock.GetSetProperty = new Model1();

            // Then
            getWasCalled.Should().BeTrue();
            setWasCalled.Should().BeTrue();
        }

        [Fact]
        public void Should_count_number_of_calls_to_get_set_property()
        {
            // Given
            var mockBuilder = Mock.IDependency();
            var mock = mockBuilder.Build();

            // When
            var firstCallResult = mock.GetSetProperty;
            mock.GetSetProperty = new Model1 { Id = 1 };
            mock.GetSetProperty = new Model1 { Id = 2 };

            // Then
            mockBuilder.GetSetProperty.Get.NumberOfCalls.Should().Be(1);
            mockBuilder.GetSetProperty.Set(Arg<Model1>.Any).NumberOfCalls.Should().Be(2);
            mockBuilder.GetSetProperty.Set(Arg<Model1>.When(m => m.Id == 1)).NumberOfCalls.Should().Be(1);
            mockBuilder.GetSetProperty.Set(Arg<Model1>.When(m => m.Id == 2)).NumberOfCalls.Should().Be(1);
        }
    }
}
