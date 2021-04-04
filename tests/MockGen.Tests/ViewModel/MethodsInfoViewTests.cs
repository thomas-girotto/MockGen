using FluentAssertions;
using MockGen.Model;
using MockGen.ViewModel;
using Xunit;

namespace MockGen.Tests.Model
{
    public class MethodsInfoViewTests
    {
        [Theory]
        [InlineData(0, "()")]
        [InlineData(1, "(_)")]
        [InlineData(2, "(_, _)")]
        public void Should_discard_the_right_number_of_times(int numberOfParameters, string discardExpected)
        {
            var templateModel = new MethodsInfoView(new MethodsInfo(numberOfParameters, true, true, false));
            templateModel.DiscardParameters.Should().Be(discardExpected);
        }

        [Theory]
        [InlineData(0, "")]
        [InlineData(1, "P1")]
        [InlineData(2, "P1P2")]
        [InlineData(3, "P1P2P3")]
        public void Should_build_file_suffix_according_to_number_of_parameter_types(int numberOfParameters, string expectedSuffix)
        {
            var templateModel = new MethodsInfoView(new MethodsInfo(numberOfParameters, true, true, false));

            var suffix = templateModel.FileSuffix;

            suffix.Should().Be(expectedSuffix);
        }
        
        [Theory]
        [InlineData(0, "")]
        [InlineData(1, "<TParam1>")]
        [InlineData(2, "<TParam1, TParam2>")]
        public void Should_build_types(int numberOfParameters, string expectedGenerics)
        {
            var templateModel = new MethodsInfoView(new MethodsInfo(numberOfParameters, true, true, false));

            var generics = templateModel.GenericTypes;

            generics.Should().Be(expectedGenerics);
        }

        [Theory]
        [InlineData(0, "<TReturn>")]
        [InlineData(1, "<TParam1, TReturn>")]
        [InlineData(2, "<TParam1, TParam2, TReturn>")]
        public void Should_build_types_with_TReturn(int numberOfParameters, string expectedGenerics)
        {
            var templateModel = new MethodsInfoView(new MethodsInfo(numberOfParameters, true, true, false));

            var generics = templateModel.GenericTypesWithTReturn;

            generics.Should().Be(expectedGenerics);
        }

        [Theory]
        [InlineData(1, "ArgMatcher<TParam1>")]
        [InlineData(2, "ArgMatcher<TParam1>, ArgMatcher<TParam2>")]
        public void Should_concat_class_name_for_each_parameter_type(int numberOfParameters, string expectedGenerics)
        {
            var templateModel = new MethodsInfoView(new MethodsInfo(numberOfParameters, true, true, false));

            var generics = templateModel.ConcatClassByParameterType("ArgMatcher");

            generics.Should().Be(expectedGenerics);
        }

        [Theory]
        [InlineData(1, "new ArgMatcher<TParam1>()")]
        [InlineData(2, "new ArgMatcher<TParam1>(), new ArgMatcher<TParam2>()")]
        public void Should_concat_instance_creation_for_each_parameter_type(int numberOfParameters, string expectedGenerics)
        {
            var templateModel = new MethodsInfoView(new MethodsInfo(numberOfParameters, true, true, false));

            var generics = templateModel.ConcatNewClassByParameterType("ArgMatcher");

            generics.Should().Be(expectedGenerics);
        }

        [Theory]
        [InlineData(0, "")]
        [InlineData(1, "TParam1 param1")]
        [InlineData(2, "TParam1 param1, TParam2 param2")]
        public void Should_concat_parameter_types_with_name(int numberOfParameters, string expectedParameters)
        {
            var templateModel = new MethodsInfoView(new MethodsInfo(numberOfParameters, true, true, false));
            templateModel.ParametersTypesWithName.Should().Be(expectedParameters);
        }

        [Theory]
        [InlineData(0, "")]
        [InlineData(1, "param1")]
        [InlineData(2, "param1, param2")]
        public void Should_concat_parameter_names(int numberOfParameters, string expectedParameterNames)
        {
            var templateModel = new MethodsInfoView(new MethodsInfo(numberOfParameters, true, true, false));
            templateModel.ConcatParameters("param").Should().Be(expectedParameterNames);
        }

        [Theory]
        [InlineData(1, "ArgMatcher<TParam1> matcher1")]
        [InlineData(2, "ArgMatcher<TParam1> matcher1, ArgMatcher<TParam2> matcher2")]
        public void Should_concat_types_and_parameter_names(int numberOfParameters, string expectedParameterNames)
        {
            var templateModel = new MethodsInfoView(new MethodsInfo(numberOfParameters, true, true, false));
            templateModel.ConcatClassParameterByParameterType("ArgMatcher", "matcher").Should().Be(expectedParameterNames);
        }

        [Theory]
        [InlineData(1, "matcher1.Match(param1)")]
        [InlineData(2, "matcher1.Match(param1) && matcher2.Match(param2)")]
        public void Should_concat_matcher_calls(int numberOfParameters, string expectedParameterNames)
        {
            var templateModel = new MethodsInfoView(new MethodsInfo(numberOfParameters, true, true, false));
            var matcherCalls = templateModel.ConcatMatcherCalls("matcher", "param");
            matcherCalls.Should().Be(expectedParameterNames);
        }
    }
}
