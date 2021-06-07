using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using MockGen.Model;
using MockGen.Tests.Fixtures;
using MockGen.Tests.Utils;
using MockGen.ViewModel;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MockGen.Tests
{
    public class MockSourceGeneratorDiagnosticsTests : IClassFixture<LoadMetadataReferenceFixture>
    {
        private readonly LoadMetadataReferenceFixture metadata;

        public MockSourceGeneratorDiagnosticsTests(LoadMetadataReferenceFixture metadata)
        {
            this.metadata = metadata;
        }

        [Fact]
        public void Should_generate_a_diagnostic_error_When_trying_to_generate_a_mock_for_a_sealed_class()
        {
            // Given
            string source = @"
using MockGen;
namespace MockGen.Tests
{
    public sealed class SealedClass { }
    public class Generators
    {
        public void GenerateMocks()
        {
            MockG.Generate<SealedClass>();
        }
    }
}";
            // When
            var (_, diagnostics) = SourceCompiler.Compile(source, metadata.MetadataReferences);

            // Then
            diagnostics.Should().HaveCount(1).And.ContainSingle(d => d.Severity == DiagnosticSeverity.Error && d.Id == "MG0002");
        }

        [Fact]
        public void Should_generate_a_diagnostic_error_When_trying_to_mock_an_unknown_type()
        {
            // Given
            string source = @"
using MockGen;
namespace MockGen.Tests
{
    public class Generators
    {
        public void GenerateMocks()
        {
            MockG.Generate<IDependency>(); // IDependency is unknown here
        }
    }
}";
            // When
            var (_, diagnostics) = SourceCompiler.Compile(source, metadata.MetadataReferences);

            // Then
            diagnostics.Should().Contain(d => d.Severity == DiagnosticSeverity.Error && d.Id == "MG0004");
        }
    }
}
