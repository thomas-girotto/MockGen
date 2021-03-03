using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using MockGen.Model;
using MockGen.Templates;
using MockGen.Templates.Matcher;
using MockGen.Templates.Setup;
using MockGen.Utils;
using System;
using System.Collections.Generic;
using System.IO;
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
                AddSourceToBuildContext(context, "MockGenerator.cs", mockGeneratorTemplate.TransformText());

                var argTemplate = new ArgTextTemplate();
                AddSourceToBuildContext(context, "Arg.cs", argTemplate.TransformText());

                var argMatcherTemplate = new ArgMatcherTextTemplate();
                AddSourceToBuildContext(context, "ArgMatcher.cs", argMatcherTemplate.TransformText());

                var anyArgMatcherTemplate = new AnyArgMatcherTextTemplate();
                AddSourceToBuildContext(context, "AnyArgMatcher.cs", anyArgMatcherTemplate.TransformText());

                var equalityArgMatcherTemplate = new EqualityArgMatcherTextTemplate();
                AddSourceToBuildContext(context, "EqualityArgMatcher.cs", equalityArgMatcherTemplate.TransformText());

                var predicateArgMatcherTemplate = new PredicateArgMatcherTextTemplate();
                AddSourceToBuildContext(context, "PredicateArgMatcher.cs", predicateArgMatcherTemplate.TransformText());

                var iMethodSetupBaseTemplate = new IMethodSetupBaseTextTemplate();
                AddSourceToBuildContext(context, "IMethodSetupBase.cs", iMethodSetupBaseTemplate.TransformText());

                var iMethodSetupTemplate = new IMethodSetupTextTemplate();
                AddSourceToBuildContext(context, "IMethodSetup.cs", iMethodSetupTemplate.TransformText());

                var iMethodSetupVoidTextTemplate = new IMethodSetupVoidTextTemplate();
                AddSourceToBuildContext(context, "IMethodSetupVoid.cs", iMethodSetupVoidTextTemplate.TransformText());

                var iMethodSetupReturnTextTemplate = new IMethodSetupReturnTextTemplate();
                AddSourceToBuildContext(context, "IMethodSetupReturn.cs", iMethodSetupReturnTextTemplate.TransformText());

                var methodSetupBaseTemplate = new MethodSetupBaseTextTemplate();
                AddSourceToBuildContext(context, "MethodSetupBase.cs", methodSetupBaseTemplate.TransformText());

                var methodSetupTemplate = new MethodSetupTextTemplate();
                AddSourceToBuildContext(context, "MethodSetup.cs", methodSetupTemplate.TransformText());

                var methodSetupReturnTemplate = new MethodSetupReturnTextTemplate();
                AddSourceToBuildContext(context, "MethodSetupReturn.cs", methodSetupReturnTemplate.TransformText());

                var methodSetupVoidTemplate = new MethodSetupVoidTextTemplate();
                AddSourceToBuildContext(context, "MethodSetupVoid.cs", methodSetupVoidTemplate.TransformText());

                // Then classes that depends on the types we found that we should mock
                var allTypesDescriptor = new List<MockDescriptor>();

                foreach (var typeSyntax in receiver.TypesToMockSyntax)
                {
                    var descriptorForTemplate = BuildModelFromTypeSyntax(context, typeSyntax);
                    allTypesDescriptor.Add(descriptorForTemplate);

                    var mockBuilderTemplate = new MockBuilderTextTemplate(descriptorForTemplate);
                    AddSourceToBuildContext(context, $"{typeSyntax.Identifier.ValueText}MockBuilder.cs", mockBuilderTemplate.TransformText());

                    var mockTemplate = new MockTextTemplate(descriptorForTemplate);
                    AddSourceToBuildContext(context, $"{typeSyntax.Identifier.ValueText}Mock.cs", mockTemplate.TransformText());
                }

                var mockStaticTemplate = new MockStaticTextTemplate(allTypesDescriptor);
                AddSourceToBuildContext(context, "Mock.cs", mockStaticTemplate.TransformText());

                // Finally classes that only depends on the number of generic types in methods
                foreach (var genericTypeDescriptor in allTypesDescriptor
                    .SelectMany(mock => mock.NumberOfParametersInMethods)
                    .Distinct())
                {
                    if (genericTypeDescriptor.NumberOfTypes > 0)
                    {
                        var iMethodSetupPn = new IMethodSetupPnTextTemplate();
                        iMethodSetupPn.Descriptor = genericTypeDescriptor;
                        AddSourceToBuildContext(context, $"IMethodSetup{genericTypeDescriptor.FileSuffix}.cs", iMethodSetupPn.TransformText());

                        var methodSetupPnTemplate = new MethodSetupPnTextTemplate();
                        methodSetupPnTemplate.Descriptor = genericTypeDescriptor;
                        AddSourceToBuildContext(context, $"MethodSetup{genericTypeDescriptor.FileSuffix}.cs", methodSetupPnTemplate.TransformText());

                        if (genericTypeDescriptor.HasMethodThatReturnsVoid)
                        {
                            var actionSpecificationTemplate = new ActionSpecificationTextTemplate();
                            actionSpecificationTemplate.Descriptor = genericTypeDescriptor;
                            AddSourceToBuildContext(context, $"ActionSpecification{genericTypeDescriptor.FileSuffix}.cs", actionSpecificationTemplate.TransformText());

                            var methodSetupVoidPnTemplate = new MethodSetupVoidPnTextTemplate();
                            methodSetupVoidPnTemplate.Descriptor = genericTypeDescriptor;
                            AddSourceToBuildContext(context, $"MethodSetupVoid{genericTypeDescriptor.FileSuffix}.cs", methodSetupVoidPnTemplate.TransformText());
                        }
                        if (genericTypeDescriptor.HasMethodThatReturns)
                        {
                            var funcSpecificationTemplate = new FuncSpecificationTextTemplate();
                            funcSpecificationTemplate.Descriptor = genericTypeDescriptor;
                            AddSourceToBuildContext(context, $"FuncSpecification{genericTypeDescriptor.FileSuffix}.cs", funcSpecificationTemplate.TransformText());

                            var iMethodSetupReturnPn = new IMethodSetupReturnPnTextTemplate();
                            iMethodSetupReturnPn.Descriptor = genericTypeDescriptor;
                            AddSourceToBuildContext(context, $"IMethodSetupReturn{genericTypeDescriptor.FileSuffix}.cs", iMethodSetupReturnPn.TransformText());
                            
                            var methodSetupReturnPnTemplate = new MethodSetupReturnPnTextTemplate();
                            methodSetupReturnPnTemplate.Descriptor = genericTypeDescriptor;
                            AddSourceToBuildContext(context, $"MethodSetupReturn{genericTypeDescriptor.FileSuffix}.cs", methodSetupReturnPnTemplate.TransformText());
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

        private void AddSourceToBuildContext(GeneratorExecutionContext context, string fileName, string source)
        {
            // For Debug purpose, it helps to write classes in the MockGen.Generated project and compile from there
            //File.WriteAllText(Path.Combine(CsprojLocator.GeneratedProjectPath, fileName), source);
            context.AddSource(fileName, SourceText.From(source, Encoding.UTF8));
        }
    }
}
