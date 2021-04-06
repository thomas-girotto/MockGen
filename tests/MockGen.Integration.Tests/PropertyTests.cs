using FluentAssertions;
using MockGen.Integration.Tests.Utils;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace MockGen.Integration.Tests
{
    [Collection("Load both metadata and sources")]
    public class PropertyTests
    {
        private readonly LoadMetadataReferenceFixture loadDependenciesfixture;
        private readonly LoadCommonSpecsFilesFixture loadSourceFilesFixture;
        private readonly TestRunner testRunner;

        public PropertyTests(ITestOutputHelper output, LoadMetadataReferenceFixture loadDependenciesfixture, LoadCommonSpecsFilesFixture loadSourceFilesFixture)
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
                loadSourceFilesFixture.GeneratorsSourceFile,
                loadSourceFilesFixture.PropertyTestFile,
            };
        }

        [Fact]
        public void Should_mock_return_value_for_get_only_property()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(PropertyTests), nameof(Should_mock_return_value_for_get_only_property));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_execute_callback_When_get_only_property_accessed()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(PropertyTests), nameof(Should_execute_callback_When_get_only_property_accessed));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_count_number_of_calls_to_get_only_property()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(PropertyTests), nameof(Should_count_number_of_calls_to_get_only_property));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_execute_callback_When_set_only_property_accessed()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(PropertyTests), nameof(Should_execute_callback_When_set_only_property_accessed));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_count_number_of_calls_to_set_only_property()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(PropertyTests), nameof(Should_count_number_of_calls_to_set_only_property));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_mock_return_value_for_get_set_property()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(PropertyTests), nameof(Should_mock_return_value_for_get_set_property));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_execute_callback_When_get_set_property_accessed()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(PropertyTests), nameof(Should_execute_callback_When_get_set_property_accessed));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_count_number_of_calls_to_get_set_property()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(PropertyTests), nameof(Should_count_number_of_calls_to_get_set_property));

            action.Should().NotThrow();
        }
    }
}
