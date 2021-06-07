using FluentAssertions;
using MockGen.Integration.Tests.Utils;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace MockGen.Integration.Tests
{
    [Collection("Load both metadata and sources")]
    public class SpyTests
    {
        private readonly LoadMetadataReferenceFixture loadDependenciesfixture;
        private readonly LoadCommonSpecsFilesFixture loadSourceFilesFixture;
        private readonly TestRunner testRunner;

        public SpyTests(ITestOutputHelper output, LoadMetadataReferenceFixture loadDependenciesfixture, LoadCommonSpecsFilesFixture loadSourceFilesFixture)
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
                loadSourceFilesFixture.ITaskDependencySourceFile,
                loadSourceFilesFixture.Model1SourceFile,
                loadSourceFilesFixture.Model2SourceFile,
                loadSourceFilesFixture.SpyTestFile,
            };
        }

        [Fact]
        public void Should_support_Any_parameter_filter()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(SpyTests), nameof(Should_support_Any_parameter_filter));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_support_Equality_parameter_filter()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(SpyTests), nameof(Should_support_Equality_parameter_filter));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_support_Predicate_parameter_filter()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(SpyTests), nameof(Should_support_Predicate_parameter_filter));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_intersect_all_parameters_criteria()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(SpyTests), nameof(Should_intersect_all_parameters_criteria));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_throw_When_mock_has_not_been_built()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(SpyTests), nameof(Should_throw_When_mock_has_not_been_built));

            action.Should().NotThrow();
        }
    }
}
