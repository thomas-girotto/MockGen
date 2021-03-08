using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using MockGen.Diagnostics;
using MockGen.Model;
using MockGen.Templates;
using MockGen.Templates.Matcher;
using MockGen.Templates.Setup;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MockGen
{
    [Generator]
    public class MockSourceGenerator : ISourceGenerator
    {
        // public member for test purpose only
        public List<MockDescriptor> TypesToMock { get; private set; } = new List<MockDescriptor>();

        public void Execute(GeneratorExecutionContext context)
        {
            try
            {
                if (context.SyntaxReceiver is not SyntaxReceiver receiver)
                {
                    return;
                }

                if (!receiver.TypesToMockSyntax.Any())
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

                // Then classes that depends on types to mock declared by the user
                foreach (var typeSyntax in receiver.TypesToMockSyntax)
                {
                    var descriptorForTemplate = MockDescriptorBuilder.FromSemanticModel(context, context.Compilation, typeSyntax);

                    if (descriptorForTemplate != null)
                    {
                        TypesToMock.Add(descriptorForTemplate);

                        var methodsSetupTemplate = new MethodsSetupTextTemplate(descriptorForTemplate);
                        AddSourceToBuildContext(context, $"{typeSyntax.Identifier.ValueText}MethodsSetup.cs", methodsSetupTemplate.TransformText());

                        var mockBuilderTemplate = new MockBuilderTextTemplate(descriptorForTemplate);
                        AddSourceToBuildContext(context, $"{typeSyntax.Identifier.ValueText}MockBuilder.cs", mockBuilderTemplate.TransformText());

                        var mockTemplate = new MockTextTemplate(descriptorForTemplate);
                        AddSourceToBuildContext(context, $"{typeSyntax.Identifier.ValueText}Mock.cs", mockTemplate.TransformText());
                    }
                }

                var mockStaticTemplate = new MockStaticTextTemplate(TypesToMock);
                AddSourceToBuildContext(context, "Mock.cs", mockStaticTemplate.TransformText());

                // Finally classes that only depend on the number of generic types in methods that we would mock
                foreach (var genericTypeDescriptor in TypesToMock.GetAllMethodsGroupedByTypeParameter())
                {
                    var template = new GenericTypesDescriptor(genericTypeDescriptor);
                    var iMethodSetupPn = new IMethodSetupPnTextTemplate();
                    iMethodSetupPn.Descriptor = template;
                    AddSourceToBuildContext(context, $"IMethodSetup{template.FileSuffix}.cs", iMethodSetupPn.TransformText());

                    var methodSetupPnTemplate = new MethodSetupPnTextTemplate();
                    methodSetupPnTemplate.Descriptor = template;
                    AddSourceToBuildContext(context, $"MethodSetup{template.FileSuffix}.cs", methodSetupPnTemplate.TransformText());

                    if (genericTypeDescriptor.HasMethodThatReturnsVoid)
                    {
                        var actionSpecificationTemplate = new ActionSpecificationTextTemplate();
                        actionSpecificationTemplate.Descriptor = template;
                        AddSourceToBuildContext(context, $"ActionSpecification{template.FileSuffix}.cs", actionSpecificationTemplate.TransformText());

                        var methodSetupVoidPnTemplate = new MethodSetupVoidPnTextTemplate();
                        methodSetupVoidPnTemplate.Descriptor = template;
                        AddSourceToBuildContext(context, $"MethodSetupVoid{template.FileSuffix}.cs", methodSetupVoidPnTemplate.TransformText());
                    }
                    if (genericTypeDescriptor.HasMethodThatReturns)
                    {
                        var funcSpecificationTemplate = new FuncSpecificationTextTemplate();
                        funcSpecificationTemplate.Descriptor = template;
                        AddSourceToBuildContext(context, $"FuncSpecification{template.FileSuffix}.cs", funcSpecificationTemplate.TransformText());

                        var iMethodSetupReturnPn = new IMethodSetupReturnPnTextTemplate();
                        iMethodSetupReturnPn.Descriptor = template;
                        AddSourceToBuildContext(context, $"IMethodSetupReturn{template.FileSuffix}.cs", iMethodSetupReturnPn.TransformText());
                            
                        var methodSetupReturnPnTemplate = new MethodSetupReturnPnTextTemplate();
                        methodSetupReturnPnTemplate.Descriptor = template;
                        AddSourceToBuildContext(context, $"MethodSetupReturn{template.FileSuffix}.cs", methodSetupReturnPnTemplate.TransformText());
                    }
                }
            }
            catch(AddSourceToBuildContextException ex)
            {
                context.ReportDiagnostic(Diagnostic.Create(DiagnosticResources.UnableToAddSourceToContext(ex.InnerException, ex.FileName), Location.None));
            }
            catch (Exception ex)
            {
                context.ReportDiagnostic(Diagnostic.Create(DiagnosticResources.TechnicalError(ex), Location.None));
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            // Register a syntax receiver that will be created for each generation pass
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        private void AddSourceToBuildContext(GeneratorExecutionContext context, string fileName, string source)
        {
            try
            {
                context.AddSource(fileName, SourceText.From(source, Encoding.UTF8));
            }
            catch (Exception ex)
            {
                throw new AddSourceToBuildContextException(fileName, ex);
            }
        }
    }
}
