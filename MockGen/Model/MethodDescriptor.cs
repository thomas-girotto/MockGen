using System;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Model
{
    public class MethodDescriptor
    {
        private string returnType;

        public string ReturnType
        {
            get => ReturnsVoid ? "void" : returnType;
            set => returnType = value;
        }

        public bool ReturnsVoid { get; set; }

        public string Name { get; set; }

        public List<ParameterDescriptor> Parameters { get; set; } = new List<ParameterDescriptor>();

        public string NameCamelCase => string.IsNullOrEmpty(Name)
           ? string.Empty
           : char.ToLower(Name[0]) + Name.Substring(1);

        public string ParametersDeclaration =>
            string.Join(", ", Parameters.Select(p => $"{p.Type} {p.Name}"));

        public string ParametersDeclarationWithArg =>
            string.Join(", ", Parameters.Select(p => $"Arg<{p.Type}> {p.Name}"));

        /// <summary>
        /// Give the concatenated list of parameters name, so that we can use them to call a method
        /// </summary>
        public string ParameterNames => string.Join(", ", Parameters.Select(p => p.Name));

        private string ParameterTypes => string.Join(", ", Parameters.Select(p => p.Type));

        public string MethodSetupWithTypedParameters =>
            (ReturnsVoid, Parameters.Count) switch
            {
                (true, 0) => "MethodSetupVoid",
                (true, > 0) => $"MethodSetupVoid<{ParameterTypes}>",
                (false, 0) => $"MethodSetupReturn<{ReturnType}>",
                (false, > 0) => $"MethodSetupReturn<{ParameterTypes}, {ReturnType}>",
                (_, _) => throw new NotImplementedException($"Case not implemented for values: {nameof(ReturnsVoid)}: {ReturnsVoid} and {nameof(Parameters.Count)}: {Parameters.Count}"),
            };

        public string IMethodSetupWithTypedParameters =>
            (ReturnsVoid, Parameters.Count) switch
            {
                (true, 0) => "IMethodSetup",
                (true, > 0) => $"IMethodSetup<{ParameterTypes}>",
                (false, _) => $"IMethodSetupReturn<{ReturnType}>",
                (_, _) => throw new NotImplementedException($"Case not implemented for values: {nameof(ReturnsVoid)}: {ReturnsVoid} and {nameof(Parameters.Count)}: {Parameters.Count}"),
            };

        public string CallForParameterMethod => string.Join(", ", Parameters.Select(p => $"{p.Name} ?? Arg<{p.Type}>.Null"));
    }
}
