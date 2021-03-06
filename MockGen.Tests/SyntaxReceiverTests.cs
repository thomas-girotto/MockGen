using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using MockGen.Tests.Utils;
using Xunit;

namespace MockGen.Tests
{
    public class SyntaxReceiverTests
    {
        [Fact]
        public void Should_extract_the_type_to_mock()
        {
            // Given
            var source = "MockGenerator.Generate<IDependency>();";
            var rootNode = CSharpSyntaxTree.ParseText(source).GetRoot();
            var syntaxReceiver = new SyntaxReceiver();
            var syntaxTreeVisitor = new MockGeneratorSyntaxWalker(syntaxReceiver);

            // When
            syntaxTreeVisitor.Visit(rootNode);

            // Then
            syntaxReceiver.TypesToMockSyntax.Should().ContainSingle().Which.Identifier.ValueText.Should().Be("IDependency");
        }

        [Fact]
        public void Should_only_add_same_type_to_mock_once()
        {
            // Given
            var source = @"
MockGenerator.Generate<IDependency>();
MockGenerator.Generate<IDependency>();
";
            var rootNode = CSharpSyntaxTree.ParseText(source).GetRoot();
            var syntaxReceiver = new SyntaxReceiver();
            var syntaxTreeVisitor = new MockGeneratorSyntaxWalker(syntaxReceiver);

            // When
            syntaxTreeVisitor.Visit(rootNode);

            // Then
            syntaxReceiver.TypesToMockSyntax.Should().HaveCount(1);
        }

        [Fact]
        public void Should_add_several_types_When_several_declarations()
        {
            // Given
            var source = @"
MockGenerator.Generate<IDependency1>();
MockGenerator.Generate<IDependency2>();
";
            var rootNode = CSharpSyntaxTree.ParseText(source).GetRoot();
            var syntaxReceiver = new SyntaxReceiver();
            var syntaxTreeVisitor = new MockGeneratorSyntaxWalker(syntaxReceiver);

            // When
            syntaxTreeVisitor.Visit(rootNode);

            // Then
            syntaxReceiver.TypesToMockSyntax.Should().HaveCount(2);
        }

        [Fact]
        public void Should_recognize_usage_with_full_type()
        {
            // Given
            var source = "MockGen.MockGenerator.Generate<IDependency1>();";
            var rootNode = CSharpSyntaxTree.ParseText(source).GetRoot();
            var syntaxReceiver = new SyntaxReceiver();
            var syntaxTreeVisitor = new MockGeneratorSyntaxWalker(syntaxReceiver);

            // When
            syntaxTreeVisitor.Visit(rootNode);

            // Then
            syntaxReceiver.TypesToMockSyntax.Should().HaveCount(1);
        }

        [Theory]
        [InlineData("MockGenerator.Generate<IDependency>")]
        [InlineData("MockGenerator.Generate<IDependency>(1)")]
        [InlineData("MockGenerator.WrongName<IDependency>()")]
        [InlineData("WrongClass.Generate<IDependency>()")]
        [InlineData("// MockGenerator.Generate<IDependency>()")]
        public void Should_not_find_type_to_mock_When_syntax_is_wrong(string source)
        {
            // Given
            var rootNode = CSharpSyntaxTree.ParseText(source).GetRoot();
            var syntaxReceiver = new SyntaxReceiver();
            var syntaxTreeVisitor = new MockGeneratorSyntaxWalker(syntaxReceiver);

            // When
            syntaxTreeVisitor.Visit(rootNode);

            // Then
            syntaxReceiver.TypesToMockSyntax.Should().BeEmpty();
        }
    }
}
