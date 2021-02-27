using FluentAssertions;
using MockGen.Tests.Utils;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace MockGen.Tests
{
    [Collection("Load both metadata and sources")]
    public class ReturnsTests
    {
        private readonly LoadMetadataReferenceFixture loadDependenciesfixture;
        private readonly LoadCommonSpecsFilesFixture loadSourceFilesFixture;
        private readonly TestRunner testRunner;

        public ReturnsTests(ITestOutputHelper output, LoadMetadataReferenceFixture loadDependenciesfixture, LoadCommonSpecsFilesFixture loadSourceFilesFixture)
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
                loadSourceFilesFixture.Model1SourceFile,
                loadSourceFilesFixture.Model2SourceFile,
                loadSourceFilesFixture.GeneratorsSourceFile,
                loadSourceFilesFixture.ReturnsTestFile,
            };
        }

        [Fact]
        public void Should_return_default_value_When_nothing_configured()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(ReturnsTests), nameof(Should_return_default_value_When_nothing_configured));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_always_return_mocked_value_When_no_parameters_in_mocked_method()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(ReturnsTests), nameof(Should_always_return_mocked_value_When_no_parameters_in_mocked_method));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_return_per_parameter_mocked_value_When_parameter_match_value()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(ReturnsTests), nameof(Should_return_per_parameter_mocked_value_When_parameter_match_value));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_return_mocked_value_When_configured_for_null()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(ReturnsTests), nameof(Should_return_mocked_value_When_configured_for_null));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_return_per_parameter_mocked_value_When_parameter_match_predicate()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(ReturnsTests), nameof(Should_return_per_parameter_mocked_value_When_parameter_match_predicate));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_return_per_parameter_mocked_value_When_parameter_match_equality_by_reference()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(ReturnsTests), nameof(Should_return_per_parameter_mocked_value_When_parameter_match_equality_by_reference));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_fallback_to_mocked_value_for_Any_When_parameter_doesnt_match_anything_else()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(ReturnsTests), nameof(Should_fallback_to_mocked_value_for_Any_When_parameter_doesnt_match_anything_else));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_fallback_to_default_When_parameter_doesnt_match_anything_and_no_Any_configured()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(ReturnsTests), nameof(Should_fallback_to_default_When_parameter_doesnt_match_anything_and_no_Any_configured));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_return_per_parameters_mocked_value_When_parameters_match_both_criterias()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(ReturnsTests), nameof(Should_return_per_parameters_mocked_value_When_parameters_match_both_criterias));

            action.Should().NotThrow();
        }
    }
}
