using FluentAssertions;
using MockGen.Model;
using Xunit;

namespace MockGen.Tests.Model
{
    public class GenericTypesDescriptorTests
    {
        [Theory]
        [InlineData(1, "(_)")]
        [InlineData(2, "(_, _)")]
        public void Should_discard_the_right_number_of_times(int numberOfParameters, string discardExpected)
        {
            var descriptor = new GenericTypesDescriptor { NumberOfTypes = numberOfParameters };
            descriptor.DiscardParameters.Should().Be(discardExpected);
        }

        [Theory]
        [InlineData(1, "P1")]
        [InlineData(2, "P1P2")]
        [InlineData(3, "P1P2P3")]
        public void Should_build_file_suffix_according_to_number_of_parameter_types(int numberOfParameters, string expectedSuffix)
        {
            var descriptor = new GenericTypesDescriptor { NumberOfTypes = numberOfParameters };

            var suffix = descriptor.FileSuffix;

            suffix.Should().Be(expectedSuffix);
        }
        
        [Theory]
        [InlineData(1, "TParam1")]
        [InlineData(2, "TParam1, TParam2")]
        public void Should_build_types(int numberOfParameters, string expectedGenerics)
        {
            var descriptor = new GenericTypesDescriptor { NumberOfTypes = numberOfParameters };

            var generics = descriptor.GenericTypes;

            generics.Should().Be(expectedGenerics);
        }

        [Theory]
        [InlineData(1, "ArgMatcher<TParam1>")]
        [InlineData(2, "ArgMatcher<TParam1>, ArgMatcher<TParam2>")]
        public void Should_concat_class_name_for_each_parameter_type(int numberOfParameters, string expectedGenerics)
        {
            var descriptor = new GenericTypesDescriptor { NumberOfTypes = numberOfParameters };

            var generics = descriptor.ConcatClassByParameterType("ArgMatcher");

            generics.Should().Be(expectedGenerics);
        }

        [Theory]
        [InlineData(1, "new ArgMatcher<TParam1>()")]
        [InlineData(2, "new ArgMatcher<TParam1>(), new ArgMatcher<TParam2>()")]
        public void Should_concat_instance_creation_for_each_parameter_type(int numberOfParameters, string expectedGenerics)
        {
            var descriptor = new GenericTypesDescriptor { NumberOfTypes = numberOfParameters };

            var generics = descriptor.ConcatNewClassByParameterType("ArgMatcher");

            generics.Should().Be(expectedGenerics);
        }

        [Theory]
        [InlineData(1, "TParam1 param1")]
        [InlineData(2, "TParam1 param1, TParam2 param2")]
        public void Should_concat_parameter_types_with_name(int numberOfParameters, string expectedParameters)
        {
            var descriptor = new GenericTypesDescriptor { NumberOfTypes = numberOfParameters };
            descriptor.ParametersTypesWithName("param").Should().Be(expectedParameters);
        }

        [Theory]
        [InlineData(1, "param1")]
        [InlineData(2, "param1, param2")]
        public void Should_concat_parameter_names(int numberOfParameters, string expectedParameterNames)
        {
            var descriptor = new GenericTypesDescriptor { NumberOfTypes = numberOfParameters };
            descriptor.ParametersNames.Should().Be(expectedParameterNames);
        }

        [Theory]
        [InlineData(1, "ArgMatcher<TParam1> matcher1")]
        [InlineData(2, "ArgMatcher<TParam1> matcher1, ArgMatcher<TParam2> matcher2")]
        public void Should_concat_types_and_parameter_names(int numberOfParameters, string expectedParameterNames)
        {
            var descriptor = new GenericTypesDescriptor { NumberOfTypes = numberOfParameters };
            descriptor.ConcatClassParameterByParameterType("ArgMatcher", "matcher").Should().Be(expectedParameterNames);
        }

        [Theory]
        [InlineData(1, "matcher1.Match(param1)")]
        [InlineData(2, "matcher1.Match(param1) && matcher2.Match(param2)")]
        public void Should_concat_matcher_calls(int numberOfParameters, string expectedParameterNames)
        {
            var descriptor = new GenericTypesDescriptor { NumberOfTypes = numberOfParameters };
            var matcherCalls = descriptor.ConcatMatcherCalls("matcher", "param");
            matcherCalls.Should().Be(expectedParameterNames);
        }
    }
}
