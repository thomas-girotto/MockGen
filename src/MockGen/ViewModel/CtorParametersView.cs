using MockGen.Model;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.ViewModel
{
    public class CtorParametersView : ParametersView
    {
        private readonly bool isTypeToMockAnInterface;
        private readonly string typeToMock;

        public CtorParametersView(bool isTypeToMockAnInterface, string typeToMock, List<Parameter> parameters) : base(parameters)
        {
            this.isTypeToMockAnInterface = isTypeToMockAnInterface;
            this.typeToMock = typeToMock;
        }

        public string MockBuilderConstructorParameterDeclaration => isTypeToMockAnInterface
            ? TypesAndNames
            : string.Join(", ", new string[] { "bool callBase", TypesAndNames }.Where(s => !string.IsNullOrEmpty(s)));

        public string MockBuilderConstructorParameterNames => isTypeToMockAnInterface
            ? Names
            : string.Join(", ", new string[] { "callBase", Names }.Where(s => !string.IsNullOrEmpty(s)));

        public string MockBuilderConstructorParameterNamesWithDefaultCallBaseValue => isTypeToMockAnInterface
            ? Names
            : string.Join(", ", new string[] { "false", Names }.Where(s => !string.IsNullOrEmpty(s)));

        public string MockConstructorParameterDeclaration => isTypeToMockAnInterface
            ? string.Join(", ", new string[] { $"{typeToMock}MethodsSetup methods", TypesAndNames }.Where(s => !string.IsNullOrEmpty(s)))
            : string.Join(", ", new string[] { $"{typeToMock}MethodsSetup methods", "bool callBase", TypesAndNames }.Where(s => !string.IsNullOrEmpty(s)));

        public string MockConstructorParameterNames => isTypeToMockAnInterface
            ? string.Join(", ", new string[] { "methods", Names }.Where(s => !string.IsNullOrEmpty(s)))
            : string.Join(", ", new string[] { "methods", "callBase", Names }.Where(s => !string.IsNullOrEmpty(s)));

    }
}
