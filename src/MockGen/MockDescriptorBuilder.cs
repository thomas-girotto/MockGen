using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MockGen.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Model
{
    public class MockDescriptorBuilder
    {
        public static MockDescriptor FromSemanticModel(GeneratorExecutionContext diagnosticReporter, Compilation compilation, IdentifierNameSyntax typeIdentifierSyntax)
        {
            var semanticModel = compilation.GetSemanticModel(typeIdentifierSyntax.SyntaxTree);
            var symbol = semanticModel.GetSymbolInfo(typeIdentifierSyntax).Symbol;

            if (symbol.IsSealed)
            {
                diagnosticReporter.ReportDiagnostic(Diagnostic.Create(
                    DiagnosticResources.ErrorMockSealedClass(symbol.Name),
                    symbol.Locations.Length > 0 ? symbol.Locations[0] : Location.None));

                return null;
            }

            var cachedNamespace = new Dictionary<string, string>();

            var descriptorForTemplate = new MockDescriptor();
            if (symbol is INamedTypeSymbol namedTypeSymbol)
            {
                descriptorForTemplate.IsInterface = namedTypeSymbol.TypeKind == TypeKind.Interface;
                descriptorForTemplate.TypeToMock = typeIdentifierSyntax.Identifier.ValueText;
                descriptorForTemplate.TypeToMockOriginalNamespace = GetNamespace(namedTypeSymbol.ContainingNamespace);

                descriptorForTemplate.Ctors = namedTypeSymbol.InstanceConstructors
                    .Select(c => new CtorDescriptor
                    {
                        Parameters = c.Parameters
                            .Select(p =>
                            {
                                var parameterNamespace = GetNamespace(cachedNamespace, typeIdentifierSyntax.SyntaxTree, semanticModel, p.Type.Name);
                                return new ParameterDescriptor(p.Type.Name, p.Name, parameterNamespace);
                            }).ToList()
                    }).ToList();

                descriptorForTemplate.Methods = namedTypeSymbol.GetMembers()
                    .OfType<IMethodSymbol>()
                    .Where(m => m.IsAbstract || m.IsVirtual)
                    .Select(m => new MethodDescriptor
                    {
                        Name = m.Name,
                        ReturnType = m.ReturnType.Name,
                        ReturnTypeNamespace = m.ReturnType.Name == "Void" 
                            ? string.Empty 
                            : GetNamespace(cachedNamespace, typeIdentifierSyntax.SyntaxTree, semanticModel, m.ReturnType.Name),
                        ReturnsVoid = m.ReturnsVoid,
                        Parameters = m.Parameters.Select(p =>
                        { 
                            var parameterNamespace = GetNamespace(cachedNamespace, typeIdentifierSyntax.SyntaxTree, semanticModel, p.Type.Name);
                            return new ParameterDescriptor(p.Type.Name, p.Name, parameterNamespace); 
                        }).ToList(),
                        ShouldBeOverriden = namedTypeSymbol.TypeKind == TypeKind.Class && (m.IsVirtual || m.IsAbstract)
                    })
                    .ToList();
            }

            return descriptorForTemplate;
        }

        private static string GetNamespace(INamespaceSymbol namespaceSymbol)
        {
            return namespaceSymbol.IsGlobalNamespace
                ? "System"
                : namespaceSymbol
                    .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
                    .Replace("global::", string.Empty);
        }

        private static string GetNamespace(Dictionary<string, string> cachedNamespace, SyntaxTree syntax, SemanticModel semanticModel, string typeToFind)
        {
            if (cachedNamespace.ContainsKey(typeToFind))
            {
                return cachedNamespace[typeToFind];
            }

            var paramTypeSyntax = syntax.GetRoot()
                .DescendantNodes()
                .OfType<IdentifierNameSyntax>()
                .FirstOrDefault(node => node.Identifier.ValueText == typeToFind);

            // TODO: seems wrong to do that, need to get back on that one
            if (paramTypeSyntax == null)
            {
                return "System";
            }
            var typeNamespace = GetNamespace(semanticModel.GetTypeInfo(paramTypeSyntax).Type.ContainingNamespace);
            
            cachedNamespace.Add(typeToFind, typeNamespace);
            
            return typeNamespace;
        }
    }
}
