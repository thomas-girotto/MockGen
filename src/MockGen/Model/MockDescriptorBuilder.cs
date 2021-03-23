using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MockGen.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Model
{
    public class MockDescriptorBuilder
    {
        public static MockDescriptor FromSemanticModel(GeneratorExecutionContext diagnosticReporter, Compilation compilation, TypeSyntax typeSyntax)
        {
            var semanticModel = compilation.GetSemanticModel(typeSyntax.SyntaxTree);
            var symbol = semanticModel.GetSymbolInfo(typeSyntax).Symbol;

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
                descriptorForTemplate.TypeToMock = GetTypeName(namedTypeSymbol);
                descriptorForTemplate.TypeToMockOriginalName = descriptorForTemplate.TypeToMock;
                descriptorForTemplate.TypeToMockNamespace = GetNamespace(namedTypeSymbol);
                descriptorForTemplate.Ctors = namedTypeSymbol.InstanceConstructors
                    .Select(c => new CtorDescriptor
                    {
                        Parameters = c.Parameters.Select(p =>
                        {
                            var parameterNamespace = GetNamespace(p.Type);
                            return new ParameterDescriptor(GetTypeName(p.Type), p.Name, parameterNamespace);
                        }).ToList()
                    }).ToList();

                var methods = namedTypeSymbol.GetMembers()
                    .OfType<IMethodSymbol>()
                    .Where(m => 
                        (m.IsAbstract || m.IsVirtual) 
                        && m.MethodKind != MethodKind.PropertyGet 
                        && m.MethodKind != MethodKind.PropertySet)
                    .Select(m => new MethodDescriptor
                    {
                        Name = m.Name,
                        ReturnType = GetTypeName(m.ReturnType),
                        ReturnTypeNamespace = m.ReturnType.Name == "Void" 
                            ? string.Empty 
                            : GetNamespace(m.ReturnType),
                        ReturnsVoid = m.ReturnsVoid,
                        Parameters = m.Parameters
                            .Select(p => new ParameterDescriptor(GetTypeName(p.Type), p.Name, GetNamespace(p.Type)))
                            .ToList(),
                        ShouldBeOverriden = namedTypeSymbol.TypeKind == TypeKind.Class && (m.IsVirtual || m.IsAbstract)
                    });

                foreach(var method in methods)
                {
                    descriptorForTemplate.AddMethod(method);
                }

                descriptorForTemplate.Properties = namedTypeSymbol.GetMembers()
                    .OfType<IPropertySymbol>()
                    .Where(p => p.IsAbstract || p.IsVirtual)
                    .Select(p => new PropertyDescriptor
                    {
                        Name = p.Name,
                        Namespace = GetNamespace(p.Type),
                        Type = GetTypeName(p.Type),
                        HasGetter = p.GetMethod != null,
                        HasSetter = p.SetMethod != null,
                    })
                    .ToList();
            }

            return descriptorForTemplate;
        }

        private static string GetFullyQualifiedName(ITypeSymbol typeSymbol)
        {
            return typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Replace("global::", string.Empty);
        }

        private static string GetTypeName(ITypeSymbol typeSymbol)
        {
            return typeSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
        }

        private static string GetNamespace(ITypeSymbol typeSymbol)
        {
            var fullyQualifiedName = GetFullyQualifiedName(typeSymbol);
            var lastDotIndex = fullyQualifiedName.LastIndexOf(".");
            return lastDotIndex == -1
                ? string.Empty
                : fullyQualifiedName.Substring(0, lastDotIndex);
        }
    }
}
