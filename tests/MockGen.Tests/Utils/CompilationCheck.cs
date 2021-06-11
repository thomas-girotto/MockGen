using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using Xunit.Sdk;

namespace MockGen.Tests.Utils
{
    public static class CompilationCheck
    {
        public static void NoError(IEnumerable<Diagnostic> diagnostics)
        {
            if (diagnostics.Any())
            {
                throw new XunitException($"There are {diagnostics.Count()} compilation errors. Details for the first one: \n{diagnostics.First().Descriptor.MessageFormat}\n{diagnostics.First().Location.SourceTree}");
            }
        }
    }
}
