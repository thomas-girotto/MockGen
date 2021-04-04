using MockGen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public string MethodSetupWithTypedParameters =>
            (method.ReturnsVoid, method.ReturnType.IsTask, method.ParametersWithoutOutParams.Count) switch
            {
                (true, _, 0) => "MethodSetupVoid",
                (true, _, > 0) => $"MethodSetupVoid<{method.ParameterTypesWithoutOutParameters}>",
                (false, false, 0) => $"MethodSetupReturn<{method.ReturnType.Name}>",
                (false, false, > 0) => $"MethodSetupReturn<{method.ParameterTypesWithoutOutParameters}, {method.ReturnType.Name}>",
                (false, true, 0) => $"MethodSetupReturnTask<{method.ReturnType.Name}>",
                (false, true, > 0) => $"MethodSetupReturnTask<{method.ParameterTypesWithoutOutParameters}, {method.ReturnType.Name}>",
                (_, _, _) => throw new NotImplementedException(),
            };

        public string IMethodSetupWithTypedParameters =>
            (method.ReturnsVoid, method.ParametersWithoutOutParams.Count) switch
            {
                (true, 0) => "IMethodSetup",
                (true, > 0) => $"IMethodSetup<{method.ParameterTypesWithoutOutParameters}>",
                (false, 0) => $"IMethodSetupReturn<{method.ReturnType.Name}>",
                (false, > 0) => $"IMethodSetupReturn<{method.ParameterTypesWithoutOutParameters}, {method.ReturnType.Name}>",
                (_, _) => throw new NotImplementedException(),
            };
    }
}
