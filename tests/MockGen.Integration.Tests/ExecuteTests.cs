using FluentAssertions;
using MockGen.Integration.Tests.Utils;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace MockGen.Integration.Tests
{
    [Collection("Load both metadata and sources")]
    public class ExecuteTests
    {
        private readonly LoadMetadataReferenceFixture loadDependenciesfixture;
        private readonly LoadCommonSpecsFilesFixture loadSourceFilesFixture;
        private readonly TestRunner testRunner;

        public ExecuteTests(ITestOutputHelper output, LoadMetadataReferenceFixture loadDependenciesfixture, LoadCommonSpecsFilesFixture loadSourceFilesFixture)
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
                loadSourceFilesFixture.ExecuteTestFile,
            };
        }

        [Fact]
        public void Should_execute_callback()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(ExecuteTests), nameof(Should_execute_callback));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_execute_callback_When_chained_after_returns_method()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(ExecuteTests), nameof(Should_execute_callback_When_chained_after_returns_method));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_throw_When_called_after_mock_has_been_built()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(ExecuteTests), nameof(Should_throw_When_called_after_mock_has_been_built));

            action.Should().NotThrow();
        }
    }
}
