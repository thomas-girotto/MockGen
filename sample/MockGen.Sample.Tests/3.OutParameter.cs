using Xunit;

namespace MockGen.Sample.Tests
{
    public class OutParameter
    {
        [Fact]
        public void OutParameter_default_Exemple()
        {
            // Given
            var mock = Mock.IDependencyOutParams();
            // By not passing a Func that returns a value for SomeModel as a second parameter, we'll have the
            // out parameter assigned default(SomeModel)
            mock.TryGetById(Arg<int>.Any).Returns(true);
            var sut = new SutServiceOutParams(mock.Build());

            // When
            var success = sut.TryGetById(1, out var model);

            // Then
            Assert.True(success);
            Assert.Null(model);
            Assert.Equal(1, mock.TryGetById(Arg<int>.Any).NumberOfCalls);
        }

        [Fact]
        public void OutParameter_Exemple()
        {
            // Given
            var mock = Mock.IDependencyOutParams();
            var expectedModel = new SomeModel();
            // The out parameter will be assigned the result of the Func as second parameter
            mock.TryGetById(Arg<int>.Any, (_) => expectedModel).Returns(true);
            var sut = new SutServiceOutParams(mock.Build());

            // When
            var success = sut.TryGetById(1, out var model);

            // Then
            Assert.True(success);
            Assert.Equal(expectedModel, model);
            Assert.Equal(1, mock.TryGetById(Arg<int>.Any).NumberOfCalls);
        }

        [Fact]
        public void SeveralOutParameters_Exemple()
        {
            // Given
            var mock = Mock.IDependencyOutParams();
            var expectedOutParam1 = new SomeModel();
            var expectedOutParam2 = new SomeModel();
            // When several out parameters, Func should return a tuple of all out parameters
            mock.TryGetByIdWithSeveralOutParameters(Arg<int>.Any, (_) => (expectedOutParam1, expectedOutParam2)).Returns(true);
            var sut = new SutServiceOutParams(mock.Build());

            // When
            var success = sut.TryGetByIdWithSeveralOutParameters(1, out var model1, out var model2);

            // Then
            Assert.True(success);
            Assert.Equal(expectedOutParam1, model1);
            Assert.Equal(expectedOutParam2, model2);
            Assert.Equal(1, mock.TryGetByIdWithSeveralOutParameters(Arg<int>.Any).NumberOfCalls);
        }
    }
}
