using FluentAssertions;
using MockGen.Model;
using MockGen.ViewModel;
using System.Collections.Generic;
using Xunit;

namespace MockGen.Tests.ViewModel
{
    public class ParametersViewTests
    {
        private ParametersView NoParameter => new ParametersView(new List<Parameter>());
        
        private ParametersView OneParameter => new ParametersView(new List<Parameter>
        {
            new Parameter(new Type("Type1", "SomeLib.Namespace"), "param1", false),
        });

        private ParametersView TwoParameters => new ParametersView(new List<Parameter>
        {
            new Parameter(new Type("Type1", "SomeLib.Namespace"), "param1", false),
            new Parameter(new Type("Type2", "SomeLib.Namespace"), "param2", false),
        });

        private ParametersView OneOutParameter => new ParametersView(new List<Parameter> 
        { 
            new Parameter(new Type("TypeOut", "SomeLib.Namespace"), "param1", true),
        });

        private ParametersView TwoOutParameters => new ParametersView(new List<Parameter>
        {
            new Parameter(new Type("TypeOut1", "SomeLib.Namespace"), "out1", true),
            new Parameter(new Type("TypeOut2", "SomeLib.Namespace"), "out2", true),
        });

        private ParametersView OneParameterAndOneOutParameter => new ParametersView(new List<Parameter>
        {
            new Parameter(new Type("Type1", "SomeLib.Namespace"), "param1", false),
            new Parameter(new Type("TypeOut1", "SomeLib.Namespace"), "out1", true),
        });

        private ParametersView OneParameterAndTwoOutParameters => new ParametersView(new List<Parameter>
        {
            new Parameter(new Type("Type1", "SomeLib.Namespace"), "param1", false),
            new Parameter(new Type("TypeOut1", "SomeLib.Namespace"), "out1", true),
            new Parameter(new Type("TypeOut2", "SomeLib.Namespace"), "out2", true),
        });

        [Fact]
        public void Names_Should_concat_names_of_parameters()
        {
            NoParameter.Names.Should().BeEmpty();
            OneParameter.Names.Should().Be("param1");
            TwoParameters.Names.Should().Be("param1, param2");
        }

        [Fact]
        public void TypesAndNames_Should_concat_parameters_type_and_names()
        {
            NoParameter.TypesAndNames.Should().BeEmpty();
            OneParameter.TypesAndNames.Should().Be("Type1 param1");
            TwoParameters.TypesAndNames.Should().Be("Type1 param1, Type2 param2");
        }

        [Fact]
        public void CallForParameterMethod_Should_concat_parameters_with_null_check()
        {
            NoParameter.CallForParameterMethod.Should().BeEmpty();
            OneParameter.CallForParameterMethod.Should().Be("param1 ?? Arg<Type1>.Null");
            TwoParameters.CallForParameterMethod.Should().Be("param1 ?? Arg<Type1>.Null, param2 ?? Arg<Type2>.Null");
        }

        [Fact]
        public void OutParameterSetupFunc_Should_build_func_method_used_to_setup_out_parameter()
        {
            OneOutParameter.OutParameterSetupFunc.Should().Be("Func<TypeOut>");
            TwoOutParameters.OutParameterSetupFunc.Should().Be("Func<(TypeOut1 out1, TypeOut2 out2)>");
            OneParameterAndOneOutParameter.OutParameterSetupFunc.Should().Be("Func<Type1, TypeOut1>");
            OneParameterAndTwoOutParameters.OutParameterSetupFunc.Should().Be("Func<Type1, (TypeOut1 out1, TypeOut2 out2)>");
        }

        [Fact]
        public void ParametersDeclarationWithArgAndOutParameters_Should_build_method_parameter_declaration_with_Arg()
        {
            NoParameter.ParametersDeclarationWithArgAndOutParameters.Should().BeEmpty();
            OneParameter.ParametersDeclarationWithArgAndOutParameters.Should().Be("Arg<Type1> param1");
            TwoParameters.ParametersDeclarationWithArgAndOutParameters.Should().Be("Arg<Type1> param1, Arg<Type2> param2");
            OneOutParameter.ParametersDeclarationWithArgAndOutParameters.Should().Be("Func<TypeOut> setupOutParameter");
            OneParameterAndOneOutParameter.ParametersDeclarationWithArgAndOutParameters.Should().Be("Arg<Type1> param1, Func<Type1, TypeOut1> setupOutParameter");
            TwoOutParameters.ParametersDeclarationWithArgAndOutParameters.Should().Be("Func<(TypeOut1 out1, TypeOut2 out2)> setupOutParameter");
        }
    }
}
