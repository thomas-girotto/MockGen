using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using MockGen.Tests.Utils;
using Xunit;

namespace MockGen.Tests
{
    public class SyntaxReceiverTests
    {
        [Theory]
        [InlineData("MockGenerator.Generate<IDependency>();", "IDependency")]
        [InlineData("MockGenerator.Generate<SomeNamespace.IDependency>();", "SomeNamespace.IDependency")]
        [InlineData("MockGen.MockGenerator.Generate<IDependency>();", "IDependency")]
        public void Should_recognize_type_to_mock(string mockGenerationSyntax, string expectedTypeName)
        {
            // Given
            var rootNode = CSharpSyntaxTree.ParseText(mockGenerationSyntax).GetRoot();
            var syntaxReceiver = new SyntaxReceiver();
            var syntaxTreeVisitor = new MockGeneratorSyntaxWalker(syntaxReceiver);

            // When
            syntaxTreeVisitor.Visit(rootNode);

            // Then
            syntaxReceiver.TypesToMock.Should().HaveCount(1);
            syntaxReceiver.TypesToMock[0].TypeName.Should().Be(expectedTypeName);
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
            syntaxReceiver.TypesToMock.Should().HaveCount(2);
            syntaxReceiver.TypesToMock[0].TypeName.Should().Be("IDependency1");
            syntaxReceiver.TypesToMock[1].TypeName.Should().Be("IDependency2");
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
            syntaxReceiver.TypesToMock.Should().BeEmpty();
        }
    }
}
