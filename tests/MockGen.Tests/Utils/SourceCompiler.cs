using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Tests.Utils
{
    public class SourceCompiler
    {
        public static (MockSourceGeneratorSpy generator, IEnumerable<Diagnostic> diagnostics) Compile(string source, List<MetadataReference> referencedAssemblies)
        {
            var rootNode = CSharpSyntaxTree.ParseText(source).GetRoot();
            var syntaxReceiver = new SyntaxReceiver();
            var syntaxTreeVisitor = new MockGeneratorSyntaxWalker(syntaxReceiver);
            syntaxTreeVisitor.Visit(rootNode);

            var compilation = CSharpCompilation.Create(
                    "TestCodeInMemoryAssembly",
                    new[] { rootNode.SyntaxTree },
                    referencedAssemblies,
                    new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            var generator = new MockSourceGeneratorSpy();

            var driver = CSharpGeneratorDriver.Create(generator);
            driver.RunGeneratorsAndUpdateCompilation(compilation, out var fullCompilation, out var generatorDiagnostics);

            var allDiagnostics = fullCompilation.GetDiagnostics().Where(d => d.Severity == DiagnosticSeverity.Error).ToList();
            allDiagnostics.AddRange(generatorDiagnostics.ToList());

            return (generator, allDiagnostics);
        }
    }
}
