using System;
using System.IO;
using System.Text;

namespace MockGen.Integration.Tests.Utils
{
    public static class SourceFileReader
    {
        private static readonly string solutionPath = Directory.GetParent(Directory.GetParent(
            Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}") - 1))
            .FullName).FullName;

        public static string SpecsProjectPath = Path.Combine(solutionPath, "specs", "MockGen.Specs");

        public static string ReadFile(string filePathRelativeToProject) => File.ReadAllText(Path.Combine(SpecsProjectPath, filePathRelativeToProject), Encoding.UTF8);
    }
}
