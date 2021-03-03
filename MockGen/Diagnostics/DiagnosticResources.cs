using Microsoft.CodeAnalysis;
using System;

namespace MockGen.Diagnostics
{
    public static class DiagnosticResources
    {
        private const string DiagnosticPrefix = "[MockGen]";
        private const string DiagnosticCategory = "MockGen";

        public static DiagnosticDescriptor TechnicalError(Exception exception) => new DiagnosticDescriptor(
            "MG0001",
            $"{DiagnosticPrefix}: Technical exception while trying to generate code",
            $"{DiagnosticPrefix}: Code generation failed with the following exception: {exception}",
            DiagnosticCategory,
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public static DiagnosticDescriptor ErrorMockSealedClass(string sealedClass) => new DiagnosticDescriptor(
            "MG0002",
            $"{DiagnosticPrefix}: Cannot create a mock for a sealed class",
            $"{DiagnosticPrefix}: '{sealedClass}' class is marked as sealed - cannot generate a mock for a sealed class.",
            DiagnosticCategory,
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);
    }
}
