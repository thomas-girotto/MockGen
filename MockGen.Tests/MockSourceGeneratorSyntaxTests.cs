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
    public class MockSourceGeneratorSyntaxTests
    {
        [Fact]
        public void Should_find_method_parameters_namespaces()
        {
            var source = @"
using MockGen;
using ExternalDependency.Model;

namespace ExternalDependency.Model
{
    public class Model1 {}
}
namespace MyLib.Tests
{
    public interface ISomeDependency 
    {
        // We should find out that we need to include namespace ExternalDependency.Model in our implementation when using Model1 type
        void DoSomething(Model1 model);
    }

    public class Generators
    {
        public void Generate()
        {
            MockGenerator.Generate<ISomeDependency>();
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
            generator.TypesToMock[0].Namespaces.Should().Contain(new string[] { "ExternalDependency.Model", "MyLib.Tests" });
        }

        [Fact]
        public void Should_find_ctor_parameters_namespaces()
        {
            var source = @"
using MockGen;
using ExternalDependency;
using ExternalDependency.Model;

namespace ExternalDependency.Model
{
    public class Model1 {}
}
namespace ExternalDependency
{
    public class SomeDependency
    {
        // We should find out that we need to include namespace ExternalDependency.Model in our implementation when using Model1 type
        public SomeDependency(Model1 model) { }
    }
}
namespace MyLib.Tests
{
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
            generator.TypesToMock[0].Namespaces.Should().Contain(new string[] { "ExternalDependency.Model", "ExternalDependency" });
        }

        [Fact]
        public void Should_find_method_return_type_namespaces()
        {
            var source = @"
using MockGen;
using ExternalDependency.Model;

namespace ExternalDependency.Model
{
    public class Model1 {}
}
namespace MyLib.Tests
{
    public interface ISomeDependency 
    {
        // We should find out that we need to include namespace ExternalDependency.Model in our implementation when using Model1 type
        Model1 DoSomething();
    }

    public class Generators
    {
        public void Generate()
        {
            MockGenerator.Generate<ISomeDependency>();
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
            generator.TypesToMock[0].Namespaces.Should().Contain(new string[] { "ExternalDependency.Model", "MyLib.Tests" });
        }

        private (MockSourceGenerator generator, IEnumerable<Diagnostic> diagnostics) CompileSource(string source)
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

            var generator = new MockSourceGenerator();

            var driver = CSharpGeneratorDriver.Create(generator);
            driver.RunGeneratorsAndUpdateCompilation(compilation, out var _, out var generateDiagnostics);

            return (generator, generateDiagnostics);
        }
    }
}
