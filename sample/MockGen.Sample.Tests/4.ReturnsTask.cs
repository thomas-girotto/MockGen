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
            var mock = Mock.IDependency();
            var expected = new SomeModel();
            // Don't need to setup Returns method with a Task<SomeModel>, you can directly use SomeModel instead
            mock.GetSomeModelAsync(Arg<int>.Any).Returns(expected);
            var sut = new SutService(mock.Build());

            // When
            var actual = await sut.GetSomeModelAsync(1);

            // Then
            Assert.Equal(actual, expected);
        }
    }
}
