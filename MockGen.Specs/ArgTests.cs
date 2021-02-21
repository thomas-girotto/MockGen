using FluentAssertions;
using MockGen.Specs.Generated.Helpers;
using MockGen.Specs.Sut;
using Xunit;

namespace MockGen.Specs
{
    public class ArgTests
    {
        [Fact]
        public void Two_Null_Arg_Should_be_equals()
        {
            var null1 = Arg<Model>.Null;
            var null2 = Arg<Model>.Null;

            null1.Should().Be(null2);
        }

        [Fact]
        public void ArgAny_Should_be_different_than_ArgNull()
        {
            var anyArg = Arg<Model>.Any;
            var nullArg = Arg<Model>.Null;

            anyArg.Should().NotBe(nullArg);
        }

        [Fact]
        public void TwoArg_having_same_instance_as_value_Should_be_equal()
        {
            var model = new Model { Id = 1 };
            var arg1 = Arg<Model>.Create(model);
            var arg2 = Arg<Model>.Create(model);

            arg1.Should().Be(arg2);
        }
    }
}
