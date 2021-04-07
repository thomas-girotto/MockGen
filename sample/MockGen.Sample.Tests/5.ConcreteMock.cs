using FluentAssertions;
using Xunit;

namespace MockGen.Sample.Tests
{
    public class ConcreteMock
    {
        [Fact]
        public void Mock_should_respect_constructor_signature()
        {
            var mock = Mock.ConcreteDependency("foo").Build();
            mock.ConstructorParam.Should().Be("foo");
        }

        [Fact]
        public void Can_mock_public_virtual_method_as_any_other_interface_method()
        {
            // Given
            var mock = Mock.ConcreteDependency("foo");
            bool wasCalled = false;
            mock.SaveModel(Arg<SomeModel>.Any).Execute(_ => wasCalled = true);
            var sut = new SutServiceConcrete(mock.Build());

            // When
            sut.DoSomethingAndSave(new SomeModel());

            // Then
            wasCalled.Should().BeTrue();
            mock.SaveModel(Arg<SomeModel>.Any).NumberOfCalls.Should().Be(1);
        }

        [Fact]
        public void Can_mock_protected_virtual_method_as_any_other_method()
        {
            // Given
            var mock = Mock.ConcreteDependency("foo");
            bool wasCalled = false;
            mock.DoSomethingProtected(Arg<SomeModel>.Any).Execute(_ => wasCalled = true);
            var sut = new SutServiceConcrete(mock.Build());

            // When
            sut.DoSomethingAndCallProtectedMethod(new SomeModel());

            // Then
            wasCalled.Should().BeTrue();
            mock.DoSomethingProtected(Arg<SomeModel>.Any).NumberOfCalls.Should().Be(1);
        }
    }
}
