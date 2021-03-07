using FluentAssertions;
using MockGen.Integration.Tests.Utils;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace MockGen.Integration.Tests
{
    [Collection("Load both metadata and sources")]
    public class ThrowsTests
    {
        private readonly LoadMetadataReferenceFixture loadDependenciesfixture;
        private readonly LoadCommonSpecsFilesFixture loadSourceFilesFixture;
        private readonly TestRunner testRunner;

        public ThrowsTests(ITestOutputHelper output, LoadMetadataReferenceFixture loadDependenciesfixture, LoadCommonSpecsFilesFixture loadSourceFilesFixture)
        {
            this.loadDependenciesfixture = loadDependenciesfixture;
            this.loadSourceFilesFixture = loadSourceFilesFixture;
            testRunner = new TestRunner(output, loadDependenciesfixture.MetadataReferences);
        }

        private List<string> GetSourceFilesToCompileFromSpecs()
        {
            return new List<string>
            {
                loadSourceFilesFixture.ConcreteDependencySourceFile,
                loadSourceFilesFixture.IDependencySourceFile,
                loadSourceFilesFixture.Model1SourceFile,
                loadSourceFilesFixture.Model2SourceFile,
                loadSourceFilesFixture.GeneratorsSourceFile,
                loadSourceFilesFixture.ThrowsTestFile,
            };
        }

        [Fact]
        public void Should_always_throw_When_no_parameter()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(ThrowsTests), nameof(Should_always_throw_When_no_parameter));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_throw_a_specific_exception_instance()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(ThrowsTests), nameof(Should_throw_a_specific_exception_instance));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_only_throw_for_matching_parameter_When_configured_for_a_specific_parameter()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(ThrowsTests), nameof(Should_only_throw_for_matching_parameter_When_configured_for_a_specific_parameter));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_only_throw_for_matching_parameter_When_configured_with_parameter_predicate()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(ThrowsTests), nameof(Should_only_throw_for_matching_parameter_When_configured_with_parameter_predicate));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_throw_When_called_after_mock_has_been_built()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(ThrowsTests), nameof(Should_throw_When_called_after_mock_has_been_built));

            action.Should().NotThrow();
        }
    }
}
