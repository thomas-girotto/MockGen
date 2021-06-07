using Xunit;

namespace MockGen.Sample.Tests
{
    public class ConcreteMock
    {
        [Fact]
        public void Mock_should_respect_constructor_signature()
        {
            var mock = MockG.Generate<ConcreteDependency>().New("foo").Build();
            Assert.Equal("foo", mock.ConstructorParam);
        }

        [Fact]
        public void Can_mock_public_virtual_method_as_any_other_interface_method()
        {
            // Given
            var mock = MockG.Generate<ConcreteDependency>().New("foo");
            bool wasCalled = false;
            mock.SaveModel(Arg<SomeModel>.Any).Execute(_ => wasCalled = true);
            var sut = new SutServiceConcrete(mock.Build());

            // When
            sut.DoSomethingAndSave(new SomeModel());

            // Then
            Assert.True(wasCalled);
            Assert.Equal(1, mock.SaveModel(Arg<SomeModel>.Any).NumberOfCalls);
        }

        [Fact]
        public void Can_mock_protected_virtual_method_as_any_other_method()
        {
            // Given
            var mock = MockG.Generate<ConcreteDependency>().New("foo");
            bool wasCalled = false;
            mock.DoSomethingProtected(Arg<SomeModel>.Any).Execute(_ => wasCalled = true);
            var sut = new SutServiceConcrete(mock.Build());

            // When
            sut.DoSomethingAndCallProtectedMethodFromNonMockedMethod(new SomeModel());

            // Then
            Assert.True(wasCalled);
            Assert.Equal(1, mock.DoSomethingProtected(Arg<SomeModel>.Any).NumberOfCalls);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Can_configure_mock_to_call_base_class(bool callBase)
        {
            var mock = MockG.Generate<ConcreteDependency>().New(callBase, "foo");
            var wasCalled = false;
            mock.DoSomethingProtected(Arg<SomeModel>.Any).Execute(_ => wasCalled = true);
            var sut = new SutServiceConcrete(mock.Build());
            var model = new SomeModel();

            // When
            sut.DoSomethingAndCallProtectedMethodFromMockedMethod(model);

            // Then
            // The protected method is called only when the mock is configured to call base class (i.e. the real class) 
            Assert.Equal(callBase, wasCalled);
        }
    }
}
