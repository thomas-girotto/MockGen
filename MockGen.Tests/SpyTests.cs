using FluentAssertions;
using MockGen.Tests.Utils;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace MockGen.Tests
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
                loadSourceFilesFixture.IDependencySourceFile,
                loadSourceFilesFixture.Model1SourceFile,
                loadSourceFilesFixture.Model2SourceFile,
                loadSourceFilesFixture.GeneratorsSourceFile,
                loadSourceFilesFixture.SpyTestFile,
            };
        }

        [Fact]
        public void Should_spy_number_of_calls()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(SpyTests), nameof(Should_spy_number_of_calls));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_spy_number_of_calls_depending_on_parameter()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(SpyTests), nameof(Should_spy_number_of_calls_depending_on_parameter));

            action.Should().NotThrow();
        }
    }
}
