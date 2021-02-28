﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using MockGen.Model;
using MockGen.Templates;
using MockGen.Templates.Matcher;
using MockGen.Templates.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MockGen
{
    [Generator]
    public class MockSourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            try
            {
                if (context.SyntaxReceiver is not SyntaxReceiver receiver)
                {
                    return;
                }

                // First inject helper classes that doesn't depend on user's code
                var mockGeneratorTemplate = new MockGeneratorTextTemplate();
                context.AddSource("MockGenerator.cs", SourceText.From(mockGeneratorTemplate.TransformText(), Encoding.UTF8));

                var argTemplate = new ArgTextTemplate();
                context.AddSource("Arg.cs", SourceText.From(argTemplate.TransformText(), Encoding.UTF8));

                var argMatcherTemplate = new ArgMatcherTextTemplate();
                context.AddSource("ArgMatcher.cs", SourceText.From(argMatcherTemplate.TransformText(), Encoding.UTF8));

                var anyArgMatcherTemplate = new AnyArgMatcherTextTemplate();
                context.AddSource("AnyArgMatcher.cs", SourceText.From(anyArgMatcherTemplate.TransformText(), Encoding.UTF8));

                var equalityArgMatcherTemplate = new EqualityArgMatcherTextTemplate();
                context.AddSource("EqualityArgMatcher.cs", SourceText.From(equalityArgMatcherTemplate.TransformText(), Encoding.UTF8));

                var predicateArgMatcherTemplate = new PredicateArgMatcherTextTemplate();
                context.AddSource("PredicateArgMatcher.cs", SourceText.From(predicateArgMatcherTemplate.TransformText(), Encoding.UTF8));

                var methodSetupTemplate = new IMethodSetupTextTemplate();
                context.AddSource("IMethodSetup.cs", SourceText.From(methodSetupTemplate.TransformText(), Encoding.UTF8));

                var iMethodSetupVoidTextTemplate = new IMethodSetupVoidTextTemplate();
                context.AddSource("IMethodSetupVoid.cs", SourceText.From(iMethodSetupVoidTextTemplate.TransformText(), Encoding.UTF8));

                var iMethodSetupReturnTextTemplate = new IMethodSetupReturnTextTemplate();
                context.AddSource("IMethodSetupReturn.cs", SourceText.From(iMethodSetupReturnTextTemplate.TransformText(), Encoding.UTF8));

                var methodSetupReturnTemplate = new MethodSetupReturnTextTemplate();
                context.AddSource("MethodSetupReturn.cs", SourceText.From(methodSetupReturnTemplate.TransformText(), Encoding.UTF8));

                var methodSetupVoidTemplate = new MethodSetupVoidTextTemplate();
                context.AddSource("MethodSetupVoid.cs", SourceText.From(methodSetupVoidTemplate.TransformText(), Encoding.UTF8));

                // Then classes that depends on the types we found that we should mock
                var allTypesDescriptor = new List<MockDescriptor>();

                foreach (var typeSyntax in receiver.TypesToMockSyntax)
                {
                    var descriptorForTemplate = BuildModelFromTypeSyntax(context, typeSyntax);
                    allTypesDescriptor.Add(descriptorForTemplate);

                    var mockStaticTemplate = new MockStaticTextTemplate(descriptorForTemplate);
                    context.AddSource("Mock.cs", SourceText.From(mockStaticTemplate.TransformText(), Encoding.UTF8));

                    var mockBuilderTemplate = new MockBuilderTextTemplate(descriptorForTemplate);
                    context.AddSource($"{typeSyntax.Identifier.ValueText}MockBuilder.cs", SourceText.From(mockBuilderTemplate.TransformText(), Encoding.UTF8));

                    var mockTemplate = new MockTextTemplate(descriptorForTemplate);
                    context.AddSource($"{typeSyntax.Identifier.ValueText}Mock.cs", SourceText.From(mockTemplate.TransformText(), Encoding.UTF8));
                }

                // Finally classes that only depends on the number of generic types in methods
                foreach (var genericTypeDescriptor in allTypesDescriptor
                    .SelectMany(mock => mock.NumberOfParametersInMethods)
                    .Distinct())
                {
                    if (genericTypeDescriptor.NumberOfTypes > 0)
                    {
                        var iMethodSetupPn = new IMethodSetupPnTextTemplate();
                        iMethodSetupPn.Descriptor = genericTypeDescriptor;
                        context.AddSource($"IMethodSetup{genericTypeDescriptor.FileSuffix}.cs", SourceText.From(iMethodSetupPn.TransformText(), Encoding.UTF8));

                        var methodSetupPnTemplate = new MethodSetupPnTextTemplate();
                        methodSetupPnTemplate.Descriptor = genericTypeDescriptor;
                        context.AddSource($"MethodSetup{genericTypeDescriptor.FileSuffix}.cs", SourceText.From(methodSetupPnTemplate.TransformText(), Encoding.UTF8));

                        if (genericTypeDescriptor.HasMethodThatReturnsVoid)
                        {
                            var actionSpecificationTemplate = new ActionSpecificationTextTemplate();
                            actionSpecificationTemplate.Descriptor = genericTypeDescriptor;
                            context.AddSource($"ActionSpecification{genericTypeDescriptor.FileSuffix}.cs", SourceText.From(actionSpecificationTemplate.TransformText(), Encoding.UTF8));

                            var methodSetupVoidPnTemplate = new MethodSetupVoidPnTextTemplate();
                            methodSetupVoidPnTemplate.Descriptor = genericTypeDescriptor;
                            context.AddSource($"MethodSetupVoid{genericTypeDescriptor.FileSuffix}.cs", SourceText.From(methodSetupVoidPnTemplate.TransformText(), Encoding.UTF8));
                        }
                        if (genericTypeDescriptor.HasMethodThatReturns)
                        {
                            var funcSpecificationTemplate = new FuncSpecificationTextTemplate();
                            funcSpecificationTemplate.Descriptor = genericTypeDescriptor;
                            context.AddSource($"FuncSpecification{genericTypeDescriptor.FileSuffix}.cs", SourceText.From(funcSpecificationTemplate.TransformText(), Encoding.UTF8));

                            var iMethodSetupReturnPn = new IMethodSetupReturnPnTextTemplate();
                            iMethodSetupReturnPn.Descriptor = genericTypeDescriptor;
                            context.AddSource($"IMethodSetupReturn{genericTypeDescriptor.FileSuffix}.cs", SourceText.From(iMethodSetupReturnPn.TransformText(), Encoding.UTF8));
                            
                            var methodSetupReturnPnTemplate = new MethodSetupReturnPnTextTemplate();
                            methodSetupReturnPnTemplate.Descriptor = genericTypeDescriptor;
                            context.AddSource($"MethodSetupReturn{genericTypeDescriptor.FileSuffix}.cs", SourceText.From(methodSetupReturnPnTemplate.TransformText(), Encoding.UTF8));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                context.ReportDiagnostic(Diagnostic.Create(
                    new DiagnosticDescriptor(
                        "MG0001",
                        "Technical exception while trying to generate code",
                        $"Code generation failed with the following exception: {ex}",
                        "MocksSourceGenerator",
                        DiagnosticSeverity.Error,
                        isEnabledByDefault: true),
                    Location.None));
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            // Register a syntax receiver that will be created for each generation pass
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        private MockDescriptor BuildModelFromTypeSyntax(GeneratorExecutionContext context, IdentifierNameSyntax typeIdentifierSyntax)
        {
            var descriptorForTemplate = new MockDescriptor();
            var model = context.Compilation.GetSemanticModel(typeIdentifierSyntax.SyntaxTree);

            var symbol = model.GetSymbolInfo(typeIdentifierSyntax).Symbol;
            if (symbol is INamedTypeSymbol namedTypeSymbol)
            {
                descriptorForTemplate.TypeToMock = typeIdentifierSyntax.Identifier.ValueText;
                descriptorForTemplate.TypeToMockOriginalNamespace = namedTypeSymbol.ContainingNamespace
                    .ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)
                    .Replace("global::", string.Empty);

                descriptorForTemplate.Methods = namedTypeSymbol.GetMembers()
                    .OfType<IMethodSymbol>()
                    .Select(m => new MethodDescriptor
                    {
                        Name = m.Name,
                        ReturnType = m.ReturnType.Name,
                        ReturnsVoid = m.ReturnsVoid,
                        Parameters = m.Parameters.Select(p => new ParameterDescriptor(p.Type.Name, p.Name)).ToList(),
                    })
                    .ToList();
            }

            return descriptorForTemplate;
        }
    }
}