using FluentAssertions;
using MockGen.Integration.Tests.Utils;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace MockGen.Integration.Tests
{
    [Collection("Load both metadata and sources")]
    public class OutParameterTests
    {
        private readonly LoadMetadataReferenceFixture loadDependenciesfixture;
        private readonly LoadCommonSpecsFilesFixture loadSourceFilesFixture;
        private readonly TestRunner testRunner;

        public OutParameterTests(ITestOutputHelper output, LoadMetadataReferenceFixture loadDependenciesfixture, LoadCommonSpecsFilesFixture loadSourceFilesFixture)
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
                loadSourceFilesFixture.OutParameterTestFile,
            };
        }

        [Fact]
        public void Should_default_out_parameter_When_not_explicitly_setup()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(OutParameterTests), nameof(Should_default_out_parameter_When_not_explicitly_setup));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_setup_out_parameter_depending_on_input_parameter()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(OutParameterTests), nameof(Should_setup_out_parameter_depending_on_input_parameter));

            action.Should().NotThrow();
        }

        [Fact]
        public void Should_setup_out_parameters_as_tuple_When_several_out_parameters()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(OutParameterTests), nameof(Should_setup_out_parameters_as_tuple_When_several_out_parameters));

            action.Should().NotThrow();
        }
    }
}
