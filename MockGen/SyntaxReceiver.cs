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
            if (syntaxNode is GenericNameSyntax genericNameSyntax && genericNameSyntax.Identifier.ValueText == "Mock")
            {
                if (genericNameSyntax.TypeArgumentList.Arguments.Count == 1)
                {
                    if (genericNameSyntax.TypeArgumentList.Arguments[0] is IdentifierNameSyntax typeToMockInGeneric)
                    {
                        if (!typesToMock.ContainsKey(typeToMockInGeneric.Identifier.ValueText))
                        {
                            typesToMock.Add(typeToMockInGeneric.Identifier.ValueText, typeToMockInGeneric);
                        }
                    }
                }
            }
        }
    }
}
