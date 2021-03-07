using System;
using System.IO;

namespace MockGen.Utils
{
    public static class CsprojLocator
    {
        private static readonly string solutionPath = Directory.GetParent(
            Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}") - 1)).FullName;

        public static string SpecsProjectPath = Path.Combine(solutionPath, "MockGen.Specs");
    }
}
