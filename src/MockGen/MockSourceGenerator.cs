using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using MockGen.Diagnostics;
using MockGen.Model;
using MockGen.Templates;
using MockGen.Templates.Matcher;
using MockGen.Templates.Setup;
using MockGen.ViewModel;
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
        public void Execute(GeneratorExecutionContext context)
        {
            try
            {
                if (context.SyntaxReceiver is not SyntaxReceiver receiver)
                {
                    return;
                }

                // Generate MockGenerator class even when no type to mock yet
                var mockGeneratorTemplate = new MockGeneratorTextTemplate();
                AddSourceToBuildContext(context, "MockGenerator.cs", mockGeneratorTemplate.TransformText());

                if (!receiver.TypesToMock.Any())
                {
                    return;
                }

                // Sanitize types to mock to handle type collision and ensure that we generate a mock only once for each type
                var sanityzedMocks = SanityzeMocks(
                    receiver.TypesToMock
                        .Select(mock => MockBuilder.FromSemanticModel(context, context.Compilation, mock))
                        .Where(m => m != null));

                // First inject helper classes that doesn't depend on user's code
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

                var actionConfigurationBaseTemplate = new ActionConfigurationBaseTextTemplate();
                AddSourceToBuildContext(context, "ActionConfigurationBase.cs", actionConfigurationBaseTemplate.TransformText());

                var iMethodSetupBaseTemplate = new IMethodSetupBaseTextTemplate();
                AddSourceToBuildContext(context, "IMethodSetupBase.cs", iMethodSetupBaseTemplate.TransformText());

                var methodSetupBaseTemplate = new MethodSetupBaseTextTemplate();
                AddSourceToBuildContext(context, "MethodSetupBase.cs", methodSetupBaseTemplate.TransformText());

                // Generate PropertySetup only When properties exists
                if (sanityzedMocks.Any(m => m.Properties.Any()))
                {
                    var propertySetupTemplate = new PropertySetupTextTemplate();
                    AddSourceToBuildContext(context, "PropertySetup.cs", propertySetupTemplate.TransformText());
                }

                // Then classes that depends on types to mock declared by the user
                foreach (var mock in sanityzedMocks)
                {
                    var methodsSetupTemplate = new MethodsSetupTextTemplate(mock);
                    AddSourceToBuildContext(context, $"{mock.TypeToMock.Name}MethodsSetup.cs", methodsSetupTemplate.TransformText());

                    var mockBuilderTemplate = new MockBuilderTextTemplate(mock);
                    AddSourceToBuildContext(context, $"{mock.TypeToMock.Name}MockBuilder.cs", mockBuilderTemplate.TransformText());

                    var mockTemplate = new MockTextTemplate(mock);
                    AddSourceToBuildContext(context, $"{mock.TypeToMock.Name}Mock.cs", mockTemplate.TransformText());
                }

                var mockStaticTemplate = new MockStaticTextTemplate(sanityzedMocks);
                AddSourceToBuildContext(context, "Mock.cs", mockStaticTemplate.TransformText());

                // Finally classes that only depend on the number of generic types in methods that we would mock
                foreach (var methodsInfo in sanityzedMocks.GetAllMethodsGroupedByTypeParameter())
                {
                    var methodsInfoView = new MethodsInfoView(methodsInfo);

                    var actionConfigurationPnTemplate = new ActionConfigurationPnTextTemplate(methodsInfoView);
                    AddSourceToBuildContext(context, $"ActionConfiguration{methodsInfoView.FileSuffix}.cs", actionConfigurationPnTemplate.TransformText());

                    var iMethodSetupPn = new IMethodSetupPnTextTemplate(methodsInfoView);
                    AddSourceToBuildContext(context, $"IMethodSetup{methodsInfoView.FileSuffix}.cs", iMethodSetupPn.TransformText());

                    var methodSetupPnTemplate = new MethodSetupPnTextTemplate(methodsInfoView);
                    AddSourceToBuildContext(context, $"MethodSetup{methodsInfoView.FileSuffix}.cs", methodSetupPnTemplate.TransformText());

                    if (methodsInfo.HasMethodThatReturnsVoid)
                    {
                        var methodSetupVoidPnTemplate = new MethodSetupVoidPnTextTemplate(methodsInfoView);
                        AddSourceToBuildContext(context, $"MethodSetupVoid{methodsInfoView.FileSuffix}.cs", methodSetupVoidPnTemplate.TransformText());
                    }
                    if (methodsInfo.HasMethodThatReturns || methodsInfo.HasMethodThatReturnsTask || methodsInfo.HasMethodThatReturnsValueTask)
                    {
                        var actionConfigurationWithReturnPnTemplate = new ActionConfigurationWithReturnPnTextTemplate(methodsInfoView);
                        AddSourceToBuildContext(context, $"ActionConfigurationWithReturn{methodsInfoView.FileSuffix}.cs", actionConfigurationWithReturnPnTemplate.TransformText());

                        var iMethodSetupReturnPn = new IMethodSetupReturnPnTextTemplate(methodsInfoView);
                        AddSourceToBuildContext(context, $"IMethodSetupReturn{methodsInfoView.FileSuffix}.cs", iMethodSetupReturnPn.TransformText());
                    }
                    if (methodsInfo.HasMethodThatReturnsTask)
                    {
                        var methodSetupReturnTaskPnTemplate = new MethodSetupReturnTaskPnTextTemplate(methodsInfoView, TaskContext.Task);
                        AddSourceToBuildContext(context, $"MethodSetupReturnTask{methodsInfoView.FileSuffix}.cs", methodSetupReturnTaskPnTemplate.TransformText());
                    }
                    if (methodsInfo.HasMethodThatReturnsValueTask)
                    {
                        var methodSetupReturnTaskPnTemplate = new MethodSetupReturnTaskPnTextTemplate(methodsInfoView, TaskContext.ValueTask);
                        AddSourceToBuildContext(context, $"MethodSetupReturnValueTask{methodsInfoView.FileSuffix}.cs", methodSetupReturnTaskPnTemplate.TransformText());
                    }
                    if (methodsInfo.HasMethodThatReturns)
                    {
                        var methodSetupReturnPnTemplate = new MethodSetupReturnPnTextTemplate(methodsInfoView);
                        AddSourceToBuildContext(context, $"MethodSetupReturn{methodsInfoView.FileSuffix}.cs", methodSetupReturnPnTemplate.TransformText());
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

        /// <summary>
        /// It's an extension point from where we can spy the types to mock generated by this source generator.
        /// I originally tried to expose this list as public member, but it lead to unexpected behavior when
        /// Visual Studio use this source generator when building the sample project (like having A LOT of times
        /// the same type in this list, leading to having same method declared several times in generated Mock.cs).
        /// </summary>
        /// <param name="allMocksFoundFromSyntax"></param>
        protected virtual IEnumerable<Mock> SanityzeMocks(IEnumerable<Mock> allMocksFoundFromSyntax)
        {
            return MockSanitizer.Sanitize(allMocksFoundFromSyntax);
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
