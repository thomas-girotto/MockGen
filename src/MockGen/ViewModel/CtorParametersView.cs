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
            : new string[] { "bool callBase", TypesAndNames }.JoinAsParameters();

        public string MockBuilderConstructorParameterNames => isTypeToMockAnInterface
            ? Names
            : new string[] { "callBase", Names }.JoinAsParameters();

        public string MockBuilderConstructorParameterNamesWithDefaultCallBaseValue => isTypeToMockAnInterface
            ? Names
            : new string[] { "false", Names }.JoinAsParameters();

        public string MockConstructorParameterDeclaration => isTypeToMockAnInterface
            ? new string[] { $"{typeToMock}MethodsSetup methods", TypesAndNames }.JoinAsParameters()
            : new string[] { $"{typeToMock}MethodsSetup methods", "bool callBase", TypesAndNames }.JoinAsParameters();

        public string MockConstructorParameterNames => isTypeToMockAnInterface
            ? new string[] { "methods", Names }.JoinAsParameters()
            : new string[] { "methods", "callBase", Names }.JoinAsParameters();
    }
}
