using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using MockGen.Tests.Utils;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Sdk;

namespace MockGen.Tests
{
    public class MockSourceGeneratorTests
    {
        [Fact]
        public void Should_find_all_types_namespaces()
        {
            var source = @"
using MockGen;
using MethodParameterNamespace.Model;
using ReturnNamespace.Model;
using CtorNamespace.Model;

namespace MethodParameterNamespace.Model
{
    public class ParameterModel {}
}
namespace ReturnNamespace.Model
{
    public class ReturnModel {}
}
namespace CtorNamespace.Model
{
    public class CtorParameterModel
}
namespace MyLib.Tests
{
    public class SomeDependency 
    {
        // We should extract namespace from ctor parameter
        public SomeDependency(CtorParameterModel model) { }

        // We should extract namespaces MethodParameterNamespace.Model and ReturnNamespace.Model
        public virtual ReturnModel DoSomething(ParameterModel model) {}
    }

    public class Generators
    {
        public void Generate()
        {
            MockGenerator.Generate<SomeDependency>();
        }
    }
}
";
            // When
            var (generator, diagnostics) = CompileSource(source);

            // Then
            if (diagnostics.Any())
            {
                throw new XunitException($"Compilation error happened, check diagnostic message: {diagnostics.First().Descriptor.Description}");
            }
            generator.TypesToMock.Should().HaveCount(1);
            generator.TypesToMock[0].Namespaces.Should().Contain(new string[] 
            { 
                "MethodParameterNamespace.Model", "ReturnNamespace.Model", "CtorNamespace.Model", "MyLib.Tests" 
            });
        }

        [Fact]
        public void Should_differentiate_two_types_having_the_same_name_by_adding_parts_of_the_fully_qualified_name()
        {
            var source = @"
using MockGen;
using MethodParameterNamespace.Model;
using ReturnNamespace.Model;
using CtorNamespace.Model;

namespace Sut.Namespace1
{
    public interface IDependency {}
}
namespace Sut.Namespace2
{
    public interface IDependency {}
}
namespace MyLib.Tests
{
    public class Generators
    {
        public void Generate()
        {
            MockGenerator.Generate<Sut.Namespace1.IDependency>();
            MockGenerator.Generate<Sut.Namespace2.IDependency>();
        }
    }
}
";
            // When
            var (generator, diagnostics) = CompileSource(source);

            // Then
            if (diagnostics.Any())
            {
                throw new XunitException($"Compilation error happened, check diagnostic message: {diagnostics.First().Descriptor.Description}");
            }
            generator.TypesToMock.Should().HaveCount(2);
            generator.TypesToMock[0].TypeToMock.Should().Be("IDependencyNamespace1");
            generator.TypesToMock[1].TypeToMock.Should().Be("IDependencyNamespace2");
        }

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
            var (_, diagnostics) = CompileSource(source);

            // Then
            diagnostics.Should().HaveCount(1).And.ContainSingle(d => d.Severity == DiagnosticSeverity.Error && d.Id == "MG0002");
        }

        private (MockSourceGeneratorSpy generator, IEnumerable<Diagnostic> diagnostics) CompileSource(string source)
        {
            var rootNode = CSharpSyntaxTree.ParseText(source).GetRoot();
            var syntaxReceiver = new SyntaxReceiver();
            var syntaxTreeVisitor = new MockGeneratorSyntaxWalker(syntaxReceiver);
            syntaxTreeVisitor.Visit(rootNode);
            
            var compilation = CSharpCompilation.Create(
                    "TestCodeInMemoryAssembly",
                    new[] { rootNode.SyntaxTree},
                    null,
                    new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            var generator = new MockSourceGeneratorSpy();

            var driver = CSharpGeneratorDriver.Create(generator);
            driver.RunGeneratorsAndUpdateCompilation(compilation, out var _, out var generateDiagnostics);

            return (generator, generateDiagnostics);
        }
    }
}
