using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace MockGen.Tests.Utils
{
    /// <summary>
    /// Helps visiting the code syntax tree from the given node as root.
    /// </summary>
    public class MockGeneratorSyntaxWalker : CSharpSyntaxWalker
    {
        private readonly SyntaxReceiver syntaxReceiver;

        public MockGeneratorSyntaxWalker(SyntaxReceiver syntaxReceiver)
        {
            this.syntaxReceiver = syntaxReceiver;
        }

        public override void Visit(SyntaxNode node)
        {
            // call our syntax receiver so that we can test it
            syntaxReceiver.OnVisitSyntaxNode(node);
            base.Visit(node);
        }
    }
}
