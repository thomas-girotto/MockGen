using System.IO;
using System.Text;
using Xunit;

namespace MockGen.Tests.Utils
{
    /// <summary>
    /// Load only once the common source files we're using accross all tests
    /// </summary>
    [CollectionDefinition("Load common specs files")]
    public class LoadCommonSpecsFilesFixture : ICollectionFixture<LoadMetadataReferenceFixture>
    {
        public string IDependencySourceFile { get; private set; }
        public string ServiceSourceFile { get; private set; }
        public string Model1SourceFile { get; private set; }
        public string Model2SourceFile { get; private set; }
        public string BasicUsageTestFile { get; private set; }
        
        public LoadCommonSpecsFilesFixture()
        {
            IDependencySourceFile = SourceFileReader.ReadFile(Path.Combine("Sut", "IDependency.cs"));
            ServiceSourceFile = SourceFileReader.ReadFile(Path.Combine("Sut", "Service.cs"));
            Model1SourceFile = SourceFileReader.ReadFile(Path.Combine("Sut", "Model1.cs"));
            Model2SourceFile = SourceFileReader.ReadFile(Path.Combine("Sut", "Model2.cs"));
            BasicUsageTestFile = SourceFileReader.ReadFile("BasicUsageTests.cs");
        }
    }
}
