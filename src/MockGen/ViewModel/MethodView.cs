using MockGen.Model;

namespace MockGen.ViewModel
{
    public class MethodView
    {
        private readonly Method method;
        public ParametersView Parameters { get; private set; }

        public MethodView(Method method)
        {
            this.method = method;
            Parameters = new ParametersView(method.Parameters);
        }

        public string Name => method.Name;

        public string UniqueName => method.UniqueName;

        public ReturnType ReturnType => method.ReturnType;

        public bool ReturnsVoid => method.ReturnsVoid;

        public string MethodSetupWithTypedParameters =>
            (ReturnsVoid, ReturnType.IsTask, method.ParametersWithoutOutParams.Count) switch
            {
                (true, _, 0) => "MethodSetupVoid",
                (true, _, > 0) => $"MethodSetupVoid<{method.ParameterTypesWithoutOutParameters}>",
                (false, false, 0) => $"MethodSetupReturn<{method.ReturnType.Name}>",
                (false, false, > 0) => $"MethodSetupReturn<{method.ParameterTypesWithoutOutParameters}, {method.ReturnType.Name}>",
                (false, true, 0) => $"MethodSetupReturnTask<{method.ReturnType.Name}>",
                (false, true, > 0) => $"MethodSetupReturnTask<{method.ParameterTypesWithoutOutParameters}, {method.ReturnType.Name}>",
                (_, _, _) => throw new System.NotImplementedException(),
            };

        public string IMethodSetupWithTypedParameters =>
            (ReturnsVoid, method.ParametersWithoutOutParams.Count) switch
            {
                (true, 0) => "IMethodSetup",
                (true, > 0) => $"IMethodSetup<{method.ParameterTypesWithoutOutParameters}>",
                (false, 0) => $"IMethodSetupReturn<{method.ReturnType.Name}>",
                (false, > 0) => $"IMethodSetupReturn<{method.ParameterTypesWithoutOutParameters}, {method.ReturnType.Name}>",
                (_, _) => throw new System.NotImplementedException(),
            };

        public string AddOverrideKeywordIfNeeded => method.ShouldBeOverriden ? "override " : string.Empty;
    }
}
