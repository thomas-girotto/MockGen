using System.Threading.Tasks;
using Xunit;

namespace MockGen.Sample.Tests
{
    public class ReturnsTask
    {
        [Fact]
        public async Task ReturnTask_Exemple()
        {
            // Given
            var mock = MockG.Generate<ITaskDependency>().New();
            // No need to return a Task<SomeModel>, you can directly returns a SomeModel instance
            var expected = new SomeModel();
            mock.GetSomeModelTaskAsync(Arg<int>.Any).Returns(expected);
            var sut = new SutServiceAsync(mock.Build());

            // When
            var actual = await sut.GetSomeModelTaskAsync(1);

            // Then
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task ReturnValueTask_Exemple()
        {
            // Given
            var mock = MockG.Generate<ITaskDependency>().New();
            var expected = new SomeModel();
            mock.GetSomeModelValueTaskAsync(Arg<int>.Any).Returns(expected);
            var sut = new SutServiceAsync(mock.Build());

            // When
            var actual = await sut.GetSomeNumberValueTaskAsync(1);

            // Then
            Assert.Equal(expected, actual);
        }
    }
}
