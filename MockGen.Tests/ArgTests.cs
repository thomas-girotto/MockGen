using FluentAssertions;
using MockGen.Tests.Utils;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace MockGen.Tests
{
    [Collection("Load both metadata and sources")]
    public class ArgTests
    {
        private readonly LoadMetadataReferenceFixture loadDependenciesfixture;
        private readonly LoadCommonSpecsFilesFixture loadSourceFilesFixture;
        private readonly TestRunner testRunner;

        public ArgTests(ITestOutputHelper output, LoadMetadataReferenceFixture loadDependenciesfixture, LoadCommonSpecsFilesFixture loadSourceFilesFixture)
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
                loadSourceFilesFixture.ModelSourceFile,
                loadSourceFilesFixture.LoadThisFile($"{nameof(ArgTests)}.cs"),
            };
        }

        [Fact]
        public void Two_Null_Arg_Should_be_equals()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(ArgTests), nameof(Two_Null_Arg_Should_be_equals));

            action.Should().NotThrow();
        }

        [Fact]
        public void ArgAny_Should_be_different_than_ArgNull()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(ArgTests), nameof(ArgAny_Should_be_different_than_ArgNull));

            action.Should().NotThrow();
        }

        [Fact]
        public void TwoArg_having_same_instance_as_value_Should_be_equal()
        {
            var sources = GetSourceFilesToCompileFromSpecs();

            Action action = () => testRunner.RunTest(sources, nameof(ArgTests), nameof(TwoArg_having_same_instance_as_value_Should_be_equal));

            action.Should().NotThrow();
        }
    }
}
