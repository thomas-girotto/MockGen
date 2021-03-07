using System;
using System.IO;

namespace MockGen.Integration.Tests.Utils
{
    public static class CsprojLocator
    {
        private static readonly string solutionPath = Directory.GetParent(Directory.GetParent(
            Environment.CurrentDirectory.Substring(0, Environment.CurrentDirectory.IndexOf($"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}") - 1))
            .FullName).FullName;

        public static string SpecsProjectPath = Path.Combine(solutionPath, "specs", "MockGen.Specs");
    }
}
