using System;
using System.Linq;

namespace MockGen.Model
{
    public class MethodDescriptor : CtorDescriptor
    {
        private string returnType;

        public string ReturnType
        {
            get => ReturnsVoid ? "void" : returnType;
            set => returnType = value;
        }

        public string ReturnTypeNamespace { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Make sure that we have a unique name (in case of method overload).
        /// For instance when method name is used to build some class attribute, it must be unique
        /// </summary>
        public string UniqueName { get; set; }

        public bool ShouldBeOverriden { get; set; }

        public bool ReturnsVoid { get; set; }

        public string AddOverrideKeywordIfNeeded => ShouldBeOverriden ? "override " : string.Empty;

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
                (false, 0) => $"IMethodSetupReturn<{ReturnType}>",
                (false, > 0) => $"IMethodSetupReturn<{ParameterTypes}, {ReturnType}>",
                (_, _) => throw new NotImplementedException($"Case not implemented for values: {nameof(ReturnsVoid)}: {ReturnsVoid} and {nameof(Parameters.Count)}: {Parameters.Count}"),
            };

        public string CallForParameterMethod => string.Join(", ", Parameters.Select(p => $"{p.Name} ?? Arg<{p.Type}>.Null"));
    }
}
