using FluentAssertions;
using MockGen.Integration.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MockGen.Integration.Tests
{
    [Collection("Load both metadata and sources")]
    public class ReturnsTaskTests
    {
        private readonly LoadMetadataReferenceFixture loadDependenciesfixture;
        private readonly LoadCommonSpecsFilesFixture loadSourceFilesFixture;
        private readonly TestRunner testRunner;

        public ReturnsTaskTests(ITestOutputHelper output, LoadMetadataReferenceFixture loadDependenciesfixture, LoadCommonSpecsFilesFixture loadSourceFilesFixture)
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
                loadSourceFilesFixture.ReturnsTaskTestFile,
            };
        }

        [Fact]
        public void Should_return_a_Task_When_configured_with_the_underlying_type()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(ReturnsTaskTests), nameof(Should_return_a_Task_When_configured_with_the_underlying_type));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_return_a_ValueTask_When_configured_with_the_underlying_type()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(ReturnsTaskTests), nameof(Should_return_a_ValueTask_When_configured_with_the_underlying_type));

            action.Should().NotThrow();
        }
    }
}
