using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using MockGen.Tests.Utils;
using Xunit;

namespace MockGen.Tests
{
    public class SyntaxReceiverTests
    {
        [Theory]
        [InlineData("MockG.Generate<IDependency>();", "IDependency")]
        [InlineData("MockG.Generate<SomeNamespace.IDependency>();", "SomeNamespace.IDependency")]
        [InlineData("MockGen.MockG.Generate<IDependency>();", "IDependency")]
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
MockG.Generate<IDependency1>();
MockG.Generate<IDependency2>();
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
        [InlineData("MockG.Generate<IDependency>")]
        [InlineData("MockG.Generate<IDependency>(1)")]
        [InlineData("MockG.WrongName<IDependency>()")]
        [InlineData("WrongClass.Generate<IDependency>()")]
        [InlineData("// MockG.Generate<IDependency>()")]
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
