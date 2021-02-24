using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MockGen.Tests.Utils
{
    public static class SourceFileReader
    {
        private static string basePathToSpecsSources = Path.Combine(
                Directory.GetParent(Environment.CurrentDirectory.Split($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}")[0]).FullName, 
                "MockGen.Specs");

        public static string ReadFile(string filePathRelativeToProject) => File.ReadAllText(Path.Combine(basePathToSpecsSources, filePathRelativeToProject), Encoding.UTF8);
    }
}
