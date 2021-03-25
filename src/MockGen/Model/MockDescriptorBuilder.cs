﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MockGen.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
                descriptorForTemplate.TypeToMock = GetType(namedTypeSymbol);
                descriptorForTemplate.TypeToMockOriginalName = descriptorForTemplate.TypeToMock.Name;
                descriptorForTemplate.Ctors = namedTypeSymbol.InstanceConstructors
                    .Select(c => new CtorDescriptor
                    {
                        Parameters = c.Parameters.Select(p => new ParameterDescriptor(GetType(p.Type), p.Name)).ToList()
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
                        ReturnType = m.ReturnsVoid ? Type.Void : GetType(m.ReturnType),
                        Parameters = m.Parameters
                            .Select(p => new ParameterDescriptor(GetType(p.Type), p.Name))
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
                        Type = GetType(p.Type),
                        HasGetter = p.GetMethod != null,
                        HasSetter = p.SetMethod != null,
                    })
                    .ToList();
            }

            return descriptorForTemplate;
        }

        private static Type GetType(ITypeSymbol typeSymbol)
        {
            var name = typeSymbol.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat);
            var fullyQualifiedName = typeSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat).Replace("global::", string.Empty);
            var namespaces = ExtractAllNamespaces(fullyQualifiedName);
            return new Type(name, namespaces);
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
