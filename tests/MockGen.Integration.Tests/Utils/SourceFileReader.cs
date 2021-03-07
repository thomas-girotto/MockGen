using MockGen.Utils;
using System.IO;
using System.Text;

namespace MockGen.Integration.Tests.Utils
{
    public static class SourceFileReader
    {
        public static string ReadFile(string filePathRelativeToProject) => File.ReadAllText(Path.Combine(CsprojLocator.SpecsProjectPath, filePathRelativeToProject), Encoding.UTF8);
    }
}
