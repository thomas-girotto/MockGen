﻿using System.IO;
using Xunit;

namespace MockGen.Integration.Tests.Utils
{
    /// <summary>
    /// Load only once the common source files we're using accross all tests
    /// </summary>
    [CollectionDefinition("Load common specs files")]
    public class LoadCommonSpecsFilesFixture : ICollectionFixture<LoadMetadataReferenceFixture>
    {
        public string ConcreteDependencySourceFile { get; private set; }
        public string ITaskDependencySourceFile { get; private set; }
        public string IDependencySourceFile { get; private set; }
        public string Model1SourceFile { get; private set; }
        public string Model2SourceFile { get; private set; }
        public string ReturnsTestFile { get; private set; }
        public string ReturnsFromFuncTestFile { get; private set; }
        public string ReturnsTaskTestFile { get; private set; }
        public string ThrowsTestFile { get; private set; }
        public string SpyTestFile { get; private set; }
        public string ExecuteTestFile { get; private set; }
        public string OutParameterTestFile { get; private set; }
        public string ConcreteClassTestFile { get; private set; }
        public string PropertyTestFile { get; private set; }

        public LoadCommonSpecsFilesFixture()
        {
            ConcreteDependencySourceFile = SourceFileReader.ReadFile(Path.Combine("Sut", "ConcreteDependency.cs"));
            ITaskDependencySourceFile = SourceFileReader.ReadFile(Path.Combine("Sut", "ITaskDependency.cs"));
            IDependencySourceFile = SourceFileReader.ReadFile(Path.Combine("Sut", "IDependency.cs"));
            Model1SourceFile = SourceFileReader.ReadFile(Path.Combine("Sut", "Model1.cs"));
            Model2SourceFile = SourceFileReader.ReadFile(Path.Combine("Sut", "Model2.cs"));
            ReturnsTestFile = SourceFileReader.ReadFile("ReturnsTests.cs");
            ReturnsFromFuncTestFile = SourceFileReader.ReadFile("ReturnsFromFuncTests.cs");
            ReturnsTaskTestFile = SourceFileReader.ReadFile("ReturnsTaskTests.cs");
            ThrowsTestFile = SourceFileReader.ReadFile("ThrowsTests.cs");
            SpyTestFile = SourceFileReader.ReadFile("SpyTests.cs");
            ExecuteTestFile = SourceFileReader.ReadFile("ExecuteTests.cs");
            ConcreteClassTestFile = SourceFileReader.ReadFile("ConcreteClassTests.cs");
            PropertyTestFile = SourceFileReader.ReadFile("PropertyTests.cs");
            OutParameterTestFile = SourceFileReader.ReadFile("OutParameterTests.cs");
        }
    }
}
