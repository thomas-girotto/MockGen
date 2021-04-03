using FluentAssertions;
using MockGen.Model;
using System.Collections.Generic;
using Xunit;

namespace MockGen.Tests.Model
{
    public class MethodTests
    {
        private Method MethodVoid => new Method
        {
            Name = "MethodVoid",
            ReturnType = ReturnType.Void,
        };

        private Method MethodVoidWithParam => new Method
        {
            Name = "MethodVoidWithParam",
            ReturnType = ReturnType.Void,
            Parameters = new List<ParameterDescriptor> { new ParameterDescriptor(new Type("Type1", "SomeLib.Namespace"), "param1", false) },
        };

        private Method MethodVoidWithTwoParam => new Method
        {
            Name = "MethodVoidWithTwoParam",
            ReturnType = ReturnType.Void,
            Parameters = new List<ParameterDescriptor>
            {
                new ParameterDescriptor(new Type("Type1", "SomeLib.Namespace"), "param1", false),
                new ParameterDescriptor(new Type("Type2", "SomeLib.Namespace"), "param2", false),
            },
        };

        private Method MethodThatReturnsWithoutParameter => new Method
        {
            Name = "Method1",
            ReturnType = new ReturnType("int", false),
        };

        private Method MethodThatReturnsWithOneParameter => new Method
        {
            Name = "Method2",
            ReturnType = new ReturnType("int", false),
            Parameters = new List<ParameterDescriptor> { new ParameterDescriptor(new Type("Type1", "SomeLib.Namespace"), "param1", false) }
        };

        private Method MethodThatReturnsWithTwoParameters => new Method
        {
            Name = "Method3",
            ReturnType = new ReturnType("int", false),
            Parameters = new List<ParameterDescriptor>
            {
                new ParameterDescriptor(new Type("Type1", "SomeLib.Namespace"), "param1", false),
                new ParameterDescriptor(new Type("Type2", "SomeLib.Namespace"), "param2", false),
            }
        };

        private Method MethodVoidWithOneOutParameter => new Method
        {
            Name = "Method4",
            ReturnType = ReturnType.Void,
            Parameters = new List<ParameterDescriptor>
            {
                new ParameterDescriptor(new Type("TypeOut", "SomeLib.Namespace"), "param1", true),
            }
        };

        private Method MethodVoidWithTwoOutParameters => new Method
        {
            Name = "Method4",
            ReturnType = ReturnType.Void,
            Parameters = new List<ParameterDescriptor>
            {
                new ParameterDescriptor(new Type("TypeOut1", "SomeLib.Namespace"), "out1", true),
                new ParameterDescriptor(new Type("TypeOut2", "SomeLib.Namespace"), "out2", true),
            }
        };

        private Method MethodVoidWithOneParameterAndOneOutParameter => new Method
        {
            Name = "Method5",
            ReturnType = ReturnType.Void,
            Parameters = new List<ParameterDescriptor>
            {
                new ParameterDescriptor(new Type("Type1", "SomeLib.Namespace"), "param1", false),
                new ParameterDescriptor(new Type("TypeOut", "SomeLib.Namespace"), "param2", true),
            }
        };

        private Method MethodVoidWithOneParameterAndTwoOutParameters => new Method
        {
            Name = "Method5",
            ReturnType = ReturnType.Void,
            Parameters = new List<ParameterDescriptor>
            {
                new ParameterDescriptor(new Type("Type1", "SomeLib.Namespace"), "param1", false),
                new ParameterDescriptor(new Type("TypeOut1", "SomeLib.Namespace"), "out1", true),
                new ParameterDescriptor(new Type("TypeOut2", "SomeLib.Namespace"), "out2", true),
            }
        };

        private Method MethodThatReturnsWithOneOutParameter => new Method
        {
            Name = "Method4",
            ReturnType = new ReturnType("int", false),
            Parameters = new List<ParameterDescriptor>
            {
                new ParameterDescriptor(new Type("TypeOut", "SomeLib.Namespace"), "param1", true),
            }
        };

        private Method MethodThatReturnsWithOneParameterAndOneOutParameter => new Method
        {
            Name = "Method5",
            ReturnType = new ReturnType("int", false),
            Parameters = new List<ParameterDescriptor>
            {
                new ParameterDescriptor(new Type("Type1", "SomeLib.Namespace"), "param1", false),
                new ParameterDescriptor(new Type("TypeOut", "SomeLib.Namespace"), "param2", true),
            }
        };

        [Fact]
        public void AddMethod_Should_set_a_unique_name_for_methods_overload()
        {
            var mock = new Mock();
            var method1 = new Method { Name = "DoSomething" };
            var method2 = new Method { Name = "DoSomething" };
            var method3 = new Method { Name = "DoSomething" };
            
            mock.AddMethod(method1);
            method1.UniqueName.Should().Be("DoSomething");

            mock.AddMethod(method2);
            method2.UniqueName.Should().Be("DoSomething1");

            mock.AddMethod(method3);
            method3.UniqueName.Should().Be("DoSomething2");
        }

        [Fact]
        public void MethodSetupWithTypedParameters_Should_build_MethodSetup_with_generic_typed_parameters()
        {
            MethodVoid.MethodSetupWithTypedParameters.Should().Be("MethodSetupVoid");
            MethodVoidWithParam.MethodSetupWithTypedParameters.Should().Be("MethodSetupVoid<Type1>");
            MethodVoidWithTwoParam.MethodSetupWithTypedParameters.Should().Be("MethodSetupVoid<Type1, Type2>");
            MethodVoidWithOneOutParameter.MethodSetupWithTypedParameters.Should().Be("MethodSetupVoid");
            MethodVoidWithOneParameterAndOneOutParameter.MethodSetupWithTypedParameters.Should().Be("MethodSetupVoid<Type1>");
            MethodThatReturnsWithoutParameter.MethodSetupWithTypedParameters.Should().Be("MethodSetupReturn<int>");
            MethodThatReturnsWithOneParameter.MethodSetupWithTypedParameters.Should().Be("MethodSetupReturn<Type1, int>");
            MethodThatReturnsWithTwoParameters.MethodSetupWithTypedParameters.Should().Be("MethodSetupReturn<Type1, Type2, int>");
            MethodThatReturnsWithOneOutParameter.MethodSetupWithTypedParameters.Should().Be("MethodSetupReturn<int>");
            MethodThatReturnsWithOneParameterAndOneOutParameter.MethodSetupWithTypedParameters.Should().Be("MethodSetupReturn<Type1, int>");
        }

        [Fact]
        public void IMethodSetupWithTypedParameters_Should_build_IMethodSetup_with_generic_typed_parameters()
        {
            MethodVoid.IMethodSetupWithTypedParameters.Should().Be("IMethodSetup");
            MethodVoidWithParam.IMethodSetupWithTypedParameters.Should().Be("IMethodSetup<Type1>");
            MethodVoidWithTwoParam.IMethodSetupWithTypedParameters.Should().Be("IMethodSetup<Type1, Type2>");
            MethodVoidWithOneOutParameter.IMethodSetupWithTypedParameters.Should().Be("IMethodSetup");
            MethodVoidWithOneParameterAndOneOutParameter.IMethodSetupWithTypedParameters.Should().Be("IMethodSetup<Type1>");
            MethodThatReturnsWithoutParameter.IMethodSetupWithTypedParameters.Should().Be("IMethodSetupReturn<int>");
            MethodThatReturnsWithOneParameter.IMethodSetupWithTypedParameters.Should().Be("IMethodSetupReturn<Type1, int>");
            MethodThatReturnsWithTwoParameters.IMethodSetupWithTypedParameters.Should().Be("IMethodSetupReturn<Type1, Type2, int>");
            MethodThatReturnsWithOneOutParameter.IMethodSetupWithTypedParameters.Should().Be("IMethodSetupReturn<int>");
            MethodThatReturnsWithOneParameterAndOneOutParameter.IMethodSetupWithTypedParameters.Should().Be("IMethodSetupReturn<Type1, int>");
        }

        [Fact]
        public void OutParameterFunc_Should_build_func_method_used_to_setup_out_parameter()
        {
            MethodVoidWithOneOutParameter.OutParameterSetupFunc.Should().Be("Func<TypeOut>");
            MethodVoidWithTwoOutParameters.OutParameterSetupFunc.Should().Be("Func<(TypeOut1 out1, TypeOut2 out2)>");
            MethodVoidWithOneParameterAndOneOutParameter.OutParameterSetupFunc.Should().Be("Func<Type1, TypeOut>");
            MethodVoidWithOneParameterAndTwoOutParameters.OutParameterSetupFunc.Should().Be("Func<Type1, (TypeOut1 out1, TypeOut2 out2)>");
            MethodThatReturnsWithOneOutParameter.OutParameterSetupFunc.Should().Be("Func<TypeOut>");
        }

        [Fact]
        public void ParametersDeclaration_Should_concat_parameters_type_and_names()
        {
            MethodThatReturnsWithoutParameter.ParametersDeclaration.Should().BeEmpty();
            MethodThatReturnsWithOneParameter.ParametersDeclaration.Should().Be("Type1 param1");
            MethodThatReturnsWithTwoParameters.ParametersDeclaration.Should().Be("Type1 param1, Type2 param2");
        }

        [Fact]
        public void ParametersDeclarationWithArgAndOutParameters_Should_build_method_parameter_declaration_with_Arg()
        {
            MethodVoid.ParametersDeclarationWithArgAndOutParameters.Should().BeEmpty();
            MethodVoidWithParam.ParametersDeclarationWithArgAndOutParameters.Should().Be("Arg<Type1> param1");
            MethodVoidWithTwoOutParameters.ParametersDeclarationWithArgAndOutParameters.Should().Be("Func<(TypeOut1 out1, TypeOut2 out2)> setupOutParameter");
            MethodThatReturnsWithoutParameter.ParametersDeclarationWithArgAndOutParameters.Should().BeEmpty();
            MethodThatReturnsWithOneParameter.ParametersDeclarationWithArgAndOutParameters.Should().Be("Arg<Type1> param1");
            MethodThatReturnsWithTwoParameters.ParametersDeclarationWithArgAndOutParameters.Should().Be("Arg<Type1> param1, Arg<Type2> param2");
            MethodThatReturnsWithOneOutParameter.ParametersDeclarationWithArgAndOutParameters.Should().Be("Func<TypeOut> setupOutParameter");
            MethodThatReturnsWithOneParameterAndOneOutParameter.ParametersDeclarationWithArgAndOutParameters.Should().Be("Arg<Type1> param1, Func<Type1, TypeOut> setupOutParameter");
        }

        [Fact]
        public void ParameterNames_Should_concat_names_of_parameters()
        {
            MethodThatReturnsWithoutParameter.ParameterNames.Should().BeEmpty();
            MethodThatReturnsWithOneParameter.ParameterNames.Should().Be("param1");
            MethodThatReturnsWithTwoParameters.ParameterNames.Should().Be("param1, param2");
        }

        [Fact]
        public void CallForParameterMethod_Should_concat_parameters_with_null_check()
        {
            MethodThatReturnsWithoutParameter.CallForParameterMethod.Should().BeEmpty();
            MethodThatReturnsWithOneParameter.CallForParameterMethod.Should().Be("param1 ?? Arg<Type1>.Null");
            MethodThatReturnsWithTwoParameters.CallForParameterMethod.Should().Be("param1 ?? Arg<Type1>.Null, param2 ?? Arg<Type2>.Null");
        }
    }
}
