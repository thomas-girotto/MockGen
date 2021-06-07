using Xunit;

namespace MockGen.Sample.Tests
{
    public class BasicUsage
    {
        [Fact]
        public void Assert_on_number_of_calls_example()
        {
            // Given
            var mock = MockG.Generate<IDependency>().New();
            var sut = new SutService(mock.Build());
            var model = new SomeModel { Id = 1 };

            // When
            sut.DoSomething();
            sut.DoSomething(model);

            // Assert on number of calls
            Assert.Equal(1, mock.DoSomething().NumberOfCalls);
            Assert.Equal(1, mock.DoSomethingWithParam(Arg<SomeModel>.Any).NumberOfCalls);
            Assert.Equal(1, mock.DoSomethingWithParam(model).NumberOfCalls);
            Assert.Equal(1, mock.DoSomethingWithParam(Arg<SomeModel>.When(m => m.Id == 1)).NumberOfCalls);
            Assert.Equal(0, mock.DoSomethingWithParam(Arg<SomeModel>.When(m => m.Id == 2)).NumberOfCalls);
        }

        [Fact]
        public void Returns_example()
        {
            // Given
            var someModel = new SomeModel { Id = 1 };
            var mock = MockG.Generate<IDependency>().New();
            mock.ReturnSomeInt().Returns(42);
            mock.ReturnSomeIntWithParam(someModel).Returns(1);
            mock.ReturnSomeIntWithParam(Arg<SomeModel>.When(m => m.Id == 2)).Returns(2);
            var sut = new SutService(mock.Build());

            // When
            var shouldBe42 = sut.ReturnSomeInt();
            var shouldBe1 = sut.ReturnSomeInt(someModel);
            var shouldBe2 = sut.ReturnSomeInt(new SomeModel { Id = 2 });

            // Then
            Assert.Equal(42, shouldBe42);
            Assert.Equal(1, shouldBe1);
            Assert.Equal(2, shouldBe2);
        }
    }
}
