using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;
using FluentAssertions;
using System.Collections.Immutable;

namespace MockGen.Tests
{
    public class CompilationErrorTests
    {
        [Fact]
        public void Should_generate_a_diagnostic_error_When_trying_to_generate_a_mock_for_a_sealed_class()
        {
            // Given
            string source = @"
namespace MockGen.Tests
{
    public sealed class SealedClass { }
    public class Generators
    {
        public void GenerateMocks()
        {
            MockGenerator.Generate<SealedClass>();
        }
    }
}";
            // When
            var diagnostics = CompileSources(source);

            // Then
            diagnostics.Should().HaveCount(1).And.ContainSingle(d => d.Severity == DiagnosticSeverity.Error && d.Id == "MG0002");
        }

        private ImmutableArray<Diagnostic> CompileSources(string source)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(source);

            var compilation = CSharpCompilation.Create(
                    "TestCodeInMemoryAssembly",
                    new[] { syntaxTree },
                    null,
                    new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            ISourceGenerator generator = new MockSourceGenerator();

            var driver = CSharpGeneratorDriver.Create(generator);
            driver.RunGeneratorsAndUpdateCompilation(compilation, out var _, out var generatedDiagnostics);

            return generatedDiagnostics;
        }
    }
}
