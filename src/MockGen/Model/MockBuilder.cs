using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MockGen.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MockGen.Model
{
    public class MockBuilder
    {
        public static Mock FromSemanticModel(GeneratorExecutionContext diagnosticReporter, Compilation compilation, TypeSyntax typeSyntax)
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

            var descriptorForTemplate = new Mock();
            if (symbol is INamedTypeSymbol namedTypeSymbol)
            {
                descriptorForTemplate.IsInterface = namedTypeSymbol.TypeKind == TypeKind.Interface;
                descriptorForTemplate.TypeToMock = GetType(namedTypeSymbol);
                descriptorForTemplate.TypeToMockOriginalName = descriptorForTemplate.TypeToMock.Name;
                descriptorForTemplate.Ctors = namedTypeSymbol.InstanceConstructors
                    .Select(c => new Ctor
                    {
                        Parameters = c.Parameters
                            .Select(p => new Parameter(GetType(p.Type), p.Name, p.RefKind == RefKind.Out))
                            .ToList()
                    }).ToList();

                var methods = namedTypeSymbol.GetMembers()
                    .OfType<IMethodSymbol>()
                    .Where(m => 
                        (m.IsAbstract || m.IsVirtual) 
                        && m.MethodKind != MethodKind.PropertyGet 
                        && m.MethodKind != MethodKind.PropertySet)
                    .Select(m => new Method
                    {
                        Name = m.Name,
                        ReturnType = m.ReturnsVoid ? ReturnType.Void : GetType(m.ReturnType),
                        Parameters = m.Parameters
                            .Select(p => new Parameter(GetType(p.Type), p.Name, p.RefKind == RefKind.Out))
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
                    .Select(p => new Property
                    {
                        Name = p.Name,
                        Type = GetType(p.Type),
                        HasGetter = p.GetMethod != null,
                        HasSetter = p.SetMethod != null,
                    })
                    .ToList();
            }

            return descriptorForTemplate;
        }

        private static ReturnType GetType(ITypeSymbol typeSymbol)
        {
            var name = typeSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
            var fullyQualifiedName = typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Replace("global::", string.Empty);
            var namespaces = ExtractAllNamespaces(fullyQualifiedName);
            if (name.StartsWith("Task<"))
            {
                // Get the T of Task<T>
                var taskOfT = name.Substring(5, name.Length - 6);
                return new ReturnType(taskOfT, true, namespaces);
            }
            return new ReturnType(name, false, namespaces);
        }

        public static List<string> ExtractAllNamespaces(string fullName)
        {
            var types = fullName.Split(new char[] { '<', '>', ',' }, System.StringSplitOptions.RemoveEmptyEntries);

            return types
                .Where(n => n.IndexOf(".") != -1)
                .Select(n => n.Substring(0, n.LastIndexOf(".")).TrimStart())
                .ToList();
        }
    }
}
