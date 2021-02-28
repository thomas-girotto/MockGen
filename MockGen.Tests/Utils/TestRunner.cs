using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace MockGen.Tests
{
    public class TestRunner
    {
        private const string TargetNamespaceForTests = "MockGen.Specs";
        private readonly ITestOutputHelper output;
        private List<MetadataReference> metadataReferences;

        public TestRunner(ITestOutputHelper output, List<MetadataReference> metadataReferences)
        {
            this.output = output;
            this.metadataReferences = metadataReferences;
        }

        public void RunTest(List<string> sources, string targetTestClass, string targetTestMethodToRun)
        {
            var sourceCompilation = CompileSources(sources);
            var sourceAndGeneratedCodeCompilation = AddGeneratedSourcesToCompilation(sourceCompilation);
            var assembly = GenerateAssemblyFromCompilation(sourceAndGeneratedCodeCompilation);
            ExecuteRemoteTestByReflection(assembly, targetTestClass, targetTestMethodToRun);
        }

        private Compilation CompileSources(List<string> sources)
        {
            var syntaxTrees = sources.Select(s => CSharpSyntaxTree.ParseText(s));

            var compilation = CSharpCompilation.Create(
                    "TestCodeInMemoryAssembly",
                    syntaxTrees,
                    metadataReferences,
                    new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            return compilation;
        }

        private Compilation AddGeneratedSourcesToCompilation(Compilation sourceCompilation)
        {
            ISourceGenerator generator = new MockSourceGenerator();

            var driver = CSharpGeneratorDriver.Create(generator);
            driver.RunGeneratorsAndUpdateCompilation(sourceCompilation, out var sourceAndGeneratedCodeCompilation, out var generatedDiagnostics);

            if (generatedDiagnostics != null && generatedDiagnostics.Any(d => d.Severity == DiagnosticSeverity.Error || d.Severity == DiagnosticSeverity.Warning))
            {
                throw new XunitException("Generated code compilation failed: " + generatedDiagnostics.FirstOrDefault()?.GetMessage());
            }

            var outputSyntaxTrees = sourceAndGeneratedCodeCompilation.SyntaxTrees.ToArray();

            output.WriteLine("Generated code:");
            foreach (var outputSyntaxTree in outputSyntaxTrees)
            {
                output.WriteLine($"\r\n===\r\n{outputSyntaxTree}\r\n===\r\n");
            }

            return sourceAndGeneratedCodeCompilation;
        }

        private Assembly GenerateAssemblyFromCompilation(Compilation sourceAndGeneratedCodeCompilation)
        {
            using var memoryStream = new MemoryStream();
            EmitResult result = sourceAndGeneratedCodeCompilation.Emit(memoryStream);

            if (!result.Success)
            {
                throw new XunitException($"Compilation did not succeed:\r\n{string.Join("\r\n", result.Diagnostics.Select(d => $"{Enum.GetName(typeof(DiagnosticSeverity), d.Severity)} ({d.Location}) - {d.GetMessage()}"))}");
            }

            memoryStream.Seek(0, SeekOrigin.Begin);

            return Assembly.Load(memoryStream.ToArray());
        }

        private void ExecuteRemoteTestByReflection(Assembly assembly, string targetTestClass, string targetTestMethodToRun)
        {
            var targetClassFullName = $"{TargetNamespaceForTests}.{targetTestClass}";

            var testClassType = assembly.GetType(targetClassFullName);
            if (testClassType == null)
            {
                throw new XunitException($"Could not find the target test class {targetClassFullName}");
            }

            var method = testClassType.GetMethod(targetTestMethodToRun);
            if (method == null)
            {
                throw new XunitException($"Could not find test method {targetTestMethodToRun}");
            }

            // Run the test method from the test class in specs project
            try
            {
                method.Invoke(Activator.CreateInstance(testClassType), null);
            }
            catch (TargetInvocationException ex)
            {
                // In case of TargetInvocationException, the exception was raised by the code invoked
                // so this is the interesting exception 
                throw ex.InnerException;
            }
        }
    }
}
