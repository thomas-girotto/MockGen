using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using MockGen.Templates;
using System;
using System.Linq;
using System.Text;

namespace MockGen
{
    [Generator]
    public class MockGenerator : ISourceGenerator
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
                var argTemplate = new ArgTextTemplate();
                context.AddSource("Arg.cs", SourceText.From(argTemplate.TransformText(), Encoding.UTF8));

                var methodSetupTemplate = new MethodSetupTextTemplate();
                context.AddSource("MethodSetup.cs", SourceText.From(methodSetupTemplate.TransformText(), Encoding.UTF8));

                var methodSpyTemplate = new MethodSpyTextTemplate();
                context.AddSource("MethodSpy.cs", SourceText.From(methodSpyTemplate.TransformText(), Encoding.UTF8));

                // Then classes that depends on the types we found that we should mock
                foreach (var typeSyntax in receiver.TypesToMockSyntax)
                {
                    var descriptorForTemplate = BuildModelFromTypeSyntax(context, typeSyntax);

                    var mockStaticTemplate = new MockStaticTextTemplate(descriptorForTemplate);
                    context.AddSource("Mock.cs", SourceText.From(mockStaticTemplate.TransformText(), Encoding.UTF8));

                    var mockBuilderTemplate = new MockBuilderTextTemplate(descriptorForTemplate);
                    context.AddSource($"{typeSyntax.Identifier.ValueText}MockBuilder.cs", SourceText.From(mockBuilderTemplate.TransformText(), Encoding.UTF8));

                    var mockTemplate = new MockTextTemplate(descriptorForTemplate);
                    context.AddSource($"{typeSyntax.Identifier.ValueText}Mock.cs", SourceText.From(mockTemplate.TransformText(), Encoding.UTF8));
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
