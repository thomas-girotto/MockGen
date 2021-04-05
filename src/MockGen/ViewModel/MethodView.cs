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
            (ReturnsVoid, ReturnType.TaskInfo, Parameters.NumberOfClassicParameters) switch
            {
                (true, _, 0) => "MethodSetupVoid",
                (true, _, > 0) => $"MethodSetupVoid<{Parameters.Types}>",
                (false, TaskInfo.NotATask, 0) => $"MethodSetupReturn<{ReturnType.Name}>",
                (false, TaskInfo.NotATask, > 0) => $"MethodSetupReturn<{Parameters.Types}, {ReturnType.Name}>",
                (false, TaskInfo.Task, 0) => $"MethodSetupReturnTask<{ReturnType.Name}>",
                (false, TaskInfo.Task, > 0) => $"MethodSetupReturnTask<{Parameters.Types}, {ReturnType.Name}>",
                (_, _, _) => throw new System.NotImplementedException(),
            };

        public string IMethodSetupWithTypedParameters =>
            (ReturnsVoid, Parameters.NumberOfClassicParameters) switch
            {
                (true, 0) => "IMethodSetup",
                (true, > 0) => $"IMethodSetup<{Parameters.Types}>",
                (false, 0) => $"IMethodSetupReturn<{ReturnType.Name}>",
                (false, > 0) => $"IMethodSetupReturn<{Parameters.Types}, {ReturnType.Name}>",
                (_, _) => throw new System.NotImplementedException(),
            };

        public string AddOverrideKeywordIfNeeded => method.IsVirtual ? "override " : string.Empty;
    }
}
