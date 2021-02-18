using System;
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
        private string basePathToSpecsSources;
        public string IDependencySourceFile { get; private set; }
        public string ServiceSourceFile { get; private set; }
        
        public LoadCommonSpecsFilesFixture()
        {
            // Prepare the common source files that we always need
            var solutionDirectoryPath = Directory
                .GetParent(Environment.CurrentDirectory.Split($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}")[0])
                .FullName;

            basePathToSpecsSources = Path.Combine(solutionDirectoryPath, "MockGen.Specs");
            IDependencySourceFile = File.ReadAllText(Path.Combine(Path.Combine(basePathToSpecsSources, "Sut"), "IDependency.cs"), Encoding.UTF8);
            ServiceSourceFile = File.ReadAllText(Path.Combine(Path.Combine(basePathToSpecsSources, "Sut"), "Service.cs"), Encoding.UTF8);
        }

        /// <summary>
        /// Helper method that reads the file (this one read it every time)
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string LoadThisFile(string fileName) => File.ReadAllText(Path.Combine(Path.Combine(basePathToSpecsSources), fileName), Encoding.UTF8);
    }
}
