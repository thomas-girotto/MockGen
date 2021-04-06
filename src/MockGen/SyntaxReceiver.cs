using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MockGen.Model;
using System.Collections.Generic;
using System.Linq;

namespace MockGen
{
    /// <summary>
    /// Created on demand before each generation pass
    /// </summary>
    public class SyntaxReceiver : ISyntaxReceiver
    {
        public List<MockTypeSyntax> TypesToMock = new List<MockTypeSyntax>();

        /// <summary>
        /// Called for every syntax node in the compilation, we can inspect the nodes and save any information useful for generation
        /// </summary>
        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is IdentifierNameSyntax maybeMockGenerator
                && maybeMockGenerator.Identifier.ValueText == "MockGenerator"
                && maybeMockGenerator.Parent is MemberAccessExpressionSyntax mockGeneratorParent)
            {
                // Parent of MockGenerator identifier can be either directly the call to Generate method
                // or if we use the full type it can be another MemberAccessExpression (i.e. MockGen.), and only then the
                // parent will be the call to Generate method
                MemberAccessExpressionSyntax candidateForGenerateMethod = null;
                if (mockGeneratorParent.Parent is InvocationExpressionSyntax)
                {
                    candidateForGenerateMethod = mockGeneratorParent;
                }
                else if (mockGeneratorParent.Expression is IdentifierNameSyntax mockGenIdentifier
                    && mockGenIdentifier.Identifier.ValueText == "MockGen"
                    && mockGeneratorParent.Parent is MemberAccessExpressionSyntax mockGeneratorGrandParent)
                {
                    candidateForGenerateMethod = mockGeneratorGrandParent;
                }

                if (candidateForGenerateMethod != null
                    && candidateForGenerateMethod.Name.Identifier.ValueText == "Generate"
                    && candidateForGenerateMethod.Parent is InvocationExpressionSyntax generateInvocationSyntax
                    && generateInvocationSyntax.ArgumentList.Arguments.Count == 0
                    && candidateForGenerateMethod.Name is GenericNameSyntax genericNameSyntax
                    && genericNameSyntax.TypeArgumentList.Arguments.Count == 1)
                {
                    var syntax = genericNameSyntax.TypeArgumentList.Arguments[0];
                    var typeName = syntax.ToString();

                    TypesToMock.Add(new MockTypeSyntax(typeName, syntax));
                }
            }
        }
    }
}
