using System;
using System.Linq;

namespace MockGen.Model
{
    public class Method : CtorDescriptor
    {
        public ReturnType ReturnType { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Make sure that we have a unique name (in case of method overload).
        /// For instance when method name is used to build some class attribute, it must be unique
        /// </summary>
        public string UniqueName { get; set; }

        public bool ShouldBeOverriden { get; set; }

        public bool ReturnsVoid => ReturnType == ReturnType.Void;

        public string AddOverrideKeywordIfNeeded => ShouldBeOverriden ? "override " : string.Empty;

        public string MethodSetupWithTypedParameters =>
            (ReturnsVoid, ReturnType.IsTask, ParametersWithoutOutParams.Count) switch
            {
                (true, _, 0) => "MethodSetupVoid",
                (true, _, > 0) => $"MethodSetupVoid<{ParameterTypesWithoutOutParameters}>",
                (false, false, 0) => $"MethodSetupReturn<{ReturnType.Name}>",
                (false, false, > 0) => $"MethodSetupReturn<{ParameterTypesWithoutOutParameters}, {ReturnType.Name}>",
                (false, true, 0) => $"MethodSetupReturnTask<{ReturnType.Name}>",
                (false, true, > 0) => $"MethodSetupReturnTask<{ParameterTypesWithoutOutParameters}, {ReturnType.Name}>",
                (_, _, _) => throw new NotImplementedException($"Case not implemented for values: {nameof(ReturnsVoid)}: {ReturnsVoid} and {nameof(Parameters.Count)}: {Parameters.Count}"),
            };

        public string IMethodSetupWithTypedParameters =>
            (ReturnsVoid, ParametersWithoutOutParams.Count) switch
            {
                (true, 0) => "IMethodSetup",
                (true, > 0) => $"IMethodSetup<{ParameterTypesWithoutOutParameters}>",
                (false, 0) => $"IMethodSetupReturn<{ReturnType.Name}>",
                (false, > 0) => $"IMethodSetupReturn<{ParameterTypesWithoutOutParameters}, {ReturnType.Name}>",
                (_, _) => throw new NotImplementedException($"Case not implemented for values: {nameof(ReturnsVoid)}: {ReturnsVoid} and {nameof(Parameters.Count)}: {Parameters.Count}"),
            };

        public string CallForParameterMethod => string.Join(", ", ParametersWithoutOutParams.Select(p => $"{p.Name} ?? Arg<{p.Type.Name}>.Null"));
    }
}
