using FluentAssertions;
using MockGen.Tests.Utils;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace MockGen.Tests
{
    [Collection("Load both metadata and sources")]
    public class BasicUsageTests
    {
        private readonly LoadMetadataReferenceFixture loadDependenciesfixture;
        private readonly LoadCommonSpecsFilesFixture loadSourceFilesFixture;
        private readonly TestRunner testRunner;

        public BasicUsageTests(ITestOutputHelper output, LoadMetadataReferenceFixture loadDependenciesfixture, LoadCommonSpecsFilesFixture loadSourceFilesFixture)
        {
            this.loadDependenciesfixture = loadDependenciesfixture;
            this.loadSourceFilesFixture = loadSourceFilesFixture;
            testRunner = new TestRunner(output, loadDependenciesfixture.MetadataReferences);
        }

        private List<string> GetSourceFilesToCompileFromSpecs()
        {
            return new List<string>
            {
                loadSourceFilesFixture.IDependencySourceFile,
                loadSourceFilesFixture.ServiceSourceFile,
                loadSourceFilesFixture.Model1SourceFile,
                loadSourceFilesFixture.LoadThisFile($"{nameof(BasicUsageTests)}.cs"),
            };
        }

        [Fact]
        public void MethodVoid_Should_spy_number_of_calls()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(BasicUsageTests), nameof(MethodVoid_Should_spy_number_of_calls));

            action.Should().NotThrow();
        }

        [Fact]
        public void MethodVoidWithParam_Should_spy_number_of_calls()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(BasicUsageTests), nameof(MethodVoidWithParam_Should_spy_number_of_calls));

            action.Should().NotThrow();
        }

        [Fact]
        public void MethodVoidWithReferenceTypeParam_Should_handle_null_values()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(BasicUsageTests), nameof(MethodVoidWithReferenceTypeParam_Should_handle_null_values));

            action.Should().NotThrow();
        }

        [Fact]
        public void MethodVoidWithReferenceTypeParam_Should_throw_depending_on_arg_predicate()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(BasicUsageTests), nameof(MethodVoidWithReferenceTypeParam_Should_throw_depending_on_arg_predicate));

            action.Should().NotThrow();
        }

        [Fact]
        public void MethodTReturn_Should_return_default_value_When_not_mocked()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(BasicUsageTests), nameof(MethodTReturn_Should_return_default_value_When_not_mocked));

            action.Should().NotThrow();
        }

        [Fact]
        public void MethodTReturn_Should_return_given_value_When_mocked()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(BasicUsageTests), nameof(MethodTReturn_Should_return_given_value_When_mocked));

            action.Should().NotThrow();
        }

        [Fact]
        public void MethodTReturn_Should_spy_the_number_of_calls_to_the_mocked_method()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(BasicUsageTests), nameof(MethodTReturn_Should_spy_the_number_of_calls_to_the_mocked_method));

            action.Should().NotThrow();
        }

        [Fact]
        public void MethodTReturnTParam_Should_return_given_number_for_given_param()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(BasicUsageTests), nameof(MethodTReturnTParam_Should_return_given_number_for_given_param));

            action.Should().NotThrow();
        }

        [Fact]
        public void MethodTReturnTParam_Should_return_param_given_for_Any_When_param_doesnt_match_a_specific_one()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(BasicUsageTests), nameof(MethodTReturnTParam_Should_return_param_given_for_Any_When_param_doesnt_match_a_specific_one));

            action.Should().NotThrow();
        }

        [Fact]
        public void Throw_should_always_throw_When_no_parameter()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(BasicUsageTests), nameof(Throw_should_always_throw_When_no_parameter));

            action.Should().NotThrow();
        }

        [Fact]
        public void Throw_for_parameter_should_only_throws_When_this_parameter_is_given()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(BasicUsageTests), nameof(Throw_for_parameter_should_only_throws_When_this_parameter_is_given));

            action.Should().NotThrow();
        }
    }
}
