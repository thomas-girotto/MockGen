using Xunit;

namespace MockGen.Sample.Tests
{
    public class ExecuteCallback
    {
        [Fact]
        public void ExecuteCallback_Exemple()
        {
            // Given
            var mock = Mock.IDependency();
            var count = 0;
            mock.DoSomething().Execute(() => count++);
            var sut = new SutService(mock.Build());

            // When
            sut.DoSomething();
            sut.DoSomething();

            // Then
            Assert.Equal(2, count);
        }

        [Fact]
        public void ExecuteCallback_DependingOnParameter_Exemple()
        {
            // Given
            var mock = Mock.IDependency();
            var count = 0;
            mock.DoSomethingWithParam(Arg<SomeModel>.When(m => m.Id >= 5)).Execute((_) => count++);
            var sut = new SutService(mock.Build());

            // When
            sut.DoSomething(new SomeModel { Id = 1 }); // won't execute callback because of argument's predicate
            sut.DoSomething(new SomeModel { Id = 5 }); // will execute callback
            sut.DoSomething(new SomeModel { Id = 6 }); // will execute callback

            // Then
            Assert.Equal(2, count);
        }

        [Fact]
        public void ExecuteCallback_WithAccessToParameter_Exemple()
        {
            // Given
            var mock = Mock.IDependency();
            var count = 0;
            mock.DoSomethingWithParam(Arg<SomeModel>.Any).Execute(m => count+= m.Id); // use the given parameter in your callback
            var sut = new SutService(mock.Build());

            // When
            sut.DoSomething(new SomeModel { Id = 1 });
            sut.DoSomething(new SomeModel { Id = 2 });

            // Then
            Assert.Equal(3, count);
        }
    }
}
