using FluentAssertions;
using MockGen.Specs.Sut;
using System;
using Xunit;

namespace MockGen.Specs
{
    public class SpyTests
    {
        [Fact]
        public void Should_support_Any_parameter_filter()
        {
            // Given
            var mockBuilder = Mock.IDependency();
            var mock = mockBuilder.Build();
            mock.DoSomethingWithTwoParameters(new Model1 { Id = 1 }, new Model2 { Name = "toto" });
            mock.DoSomethingWithTwoParameters(new Model1 { Id = 2 }, new Model2 { Name = "toto" });
            mock.DoSomethingWithTwoParameters(new Model1 { Id = 2 }, new Model2 { Name = "tata" });

            // When
            var callsForToto = mockBuilder.DoSomethingWithTwoParameters(Arg<Model1>.Any, Arg<Model2>.Any).MatchingCalls;
            
            // Then
            callsForToto.Should().HaveCount(3);
        }

        [Fact]
        public void Should_support_Equality_parameter_filter()
        {
            // Given
            var mockBuilder = Mock.IDependency();
            var mock = mockBuilder.Build();
            var model2 = new Model2 { Name = "toto" };
            mock.DoSomethingWithTwoParameters(new Model1 { Id = 1 }, new Model2 { Name = "toto" });
            mock.DoSomethingWithTwoParameters(new Model1 { Id = 2 }, model2);
            mock.DoSomethingWithTwoParameters(new Model1 { Id = 3 }, model2);

            // When
            var callsForModel2 = mockBuilder.DoSomethingWithTwoParameters(Arg<Model1>.Any, model2).MatchingCalls;

            // Then
            callsForModel2.Should().HaveCount(2)
                .And.Contain(args => args.param1.Id == 2)
                .And.Contain(args => args.param1.Id == 3);
        }

        [Fact]
        public void Should_support_Predicate_parameter_filter()
        {
            // Given
            var mockBuilder = Mock.IDependency();
            var mock = mockBuilder.Build();
            var model2 = new Model2 { Name = "toto" };
            mock.DoSomethingWithTwoParameters(new Model1 { Id = 1 }, new Model2 { Name = "toto" });
            mock.DoSomethingWithTwoParameters(new Model1 { Id = 2 }, new Model2 { Name = "toto" });
            mock.DoSomethingWithTwoParameters(new Model1 { Id = 2 }, new Model2 { Name = "tata" });

            // When
            var callsForToto = mockBuilder.DoSomethingWithTwoParameters(Arg<Model1>.Any, Arg<Model2>.When(m => m.Name == "toto")).MatchingCalls;

            // Then
            callsForToto.Should().HaveCount(2)
                .And.Contain(args => args.param1.Id == 1)
                .And.Contain(args => args.param1.Id == 2);
        }

        [Fact]
        public void Should_intersect_all_parameters_criteria()
        {
            // Given
            var mockBuilder = Mock.IDependency();
            var mock = mockBuilder.Build();
            var model2 = new Model2 { Name = "toto" };
            mock.DoSomethingWithTwoParameters(new Model1 { Id = 1 }, new Model2 { Name = "toto" });
            mock.DoSomethingWithTwoParameters(new Model1 { Id = 2 }, new Model2 { Name = "toto" });
            mock.DoSomethingWithTwoParameters(new Model1 { Id = 2 }, new Model2 { Name = "tata" });

            // When
            var callsForToto = mockBuilder.DoSomethingWithTwoParameters(
                Arg<Model1>.When(m => m.Id == 2), 
                Arg<Model2>.When(m => m.Name == "toto")).MatchingCalls;

            // Then
            callsForToto.Should().HaveCount(1)
                .And.Contain(args => args.param1.Id == 2);
        }

        [Fact]
        public void Should_throw_When_mock_has_not_been_built()
        {
            // Given
            var mockBuilder = Mock.IDependency();

            // When
            Func<int> spyAction1 = () => mockBuilder.DoSomething().NumberOfCalls;
            Func<int> spyAction2 = () => mockBuilder.DoSomethingWithParameter(1).NumberOfCalls;
            Func<int> spyAction3 = () => mockBuilder.DoSomethingWithTwoParameters(null, null).NumberOfCalls;
            Func<int> spyAction4 = () => mockBuilder.GetSomeNumber().NumberOfCalls;
            Func<int> spyAction5 = () => mockBuilder.GetSomeNumberWithParameter(1).NumberOfCalls;
            Func<int> spyAction6 = () => mockBuilder.GetSomeNumberWithTwoParameters(null, null).NumberOfCalls;

            // Then
            spyAction1.Should().Throw<InvalidOperationException>();
            spyAction2.Should().Throw<InvalidOperationException>();
            spyAction3.Should().Throw<InvalidOperationException>();
            spyAction4.Should().Throw<InvalidOperationException>();
            spyAction5.Should().Throw<InvalidOperationException>();
            spyAction6.Should().Throw<InvalidOperationException>();
        }
    }
}
