using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace MockGen
{
    /// <summary>
    /// Created on demand before each generation pass
    /// </summary>
    public class SyntaxReceiver : ISyntaxReceiver
    {
        private Dictionary<string, IdentifierNameSyntax> typesToMock = new Dictionary<string, IdentifierNameSyntax>();
        public IEnumerable<IdentifierNameSyntax> TypesToMockSyntax => typesToMock.Select(kvp => kvp.Value);

        /// <summary>
        /// Called for every syntax node in the compilation, we can inspect the nodes and save any information useful for generation
        /// </summary>
        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is MemberAccessExpressionSyntax memberAccessSyntax
                && memberAccessSyntax.Parent is InvocationExpressionSyntax invocationSyntax
                && invocationSyntax.ArgumentList.Arguments.Count == 0
                && memberAccessSyntax.Expression is IdentifierNameSyntax maybeMockGenerator
                && maybeMockGenerator.Identifier.ValueText == "MockGenerator"
                && memberAccessSyntax.Name is GenericNameSyntax genericNameSyntax
                && memberAccessSyntax.Name.Identifier.ValueText == "Generate"
                && genericNameSyntax.TypeArgumentList.Arguments.Count == 1
                && genericNameSyntax.TypeArgumentList.Arguments[0] is IdentifierNameSyntax typeToMockInGeneric)
            {
                if (!typesToMock.ContainsKey(typeToMockInGeneric.Identifier.ValueText))
                {
                    typesToMock.Add(typeToMockInGeneric.Identifier.ValueText, typeToMockInGeneric);
                }
            }
        }
    }
}
