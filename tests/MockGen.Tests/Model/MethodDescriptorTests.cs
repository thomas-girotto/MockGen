using FluentAssertions;
using MockGen.Model;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MockGen.Tests.Model
{
    public class MethodDescriptorTests
    {
        private MethodDescriptor MethodVoid => new MethodDescriptor
        {
            Name = "MethodVoid",
            ReturnType = ReturnType.Void,
        };

        private MethodDescriptor MethodVoidWithParam => new MethodDescriptor
        {
            Name = "MethodVoidWithParam",
            ReturnType = ReturnType.Void,
            Parameters = new List<ParameterDescriptor> { new ParameterDescriptor(new Type("Type1", "SomeLib.Namespace"), "param1", false) },
        };

        private MethodDescriptor MethodVoidWithTwoParam => new MethodDescriptor
        {
            Name = "MethodVoidWithTwoParam",
            ReturnType = ReturnType.Void,
            Parameters = new List<ParameterDescriptor>
            {
                new ParameterDescriptor(new Type("Type1", "SomeLib.Namespace"), "param1", false),
                new ParameterDescriptor(new Type("Type2", "SomeLib.Namespace"), "param2", false),
            },
        };

        private MethodDescriptor MethodThatReturnsWithoutParameter => new MethodDescriptor
        {
            Name = "Method1",
            ReturnType = new ReturnType("int", false),
        };

        private MethodDescriptor MethodThatReturnsWithOneParameter => new MethodDescriptor
        {
            Name = "Method2",
            ReturnType = new ReturnType("int", false),
            Parameters = new List<ParameterDescriptor> { new ParameterDescriptor(new Type("Type1", "SomeLib.Namespace"), "param1", false) }
        };

        private MethodDescriptor MethodThatReturnsWithTwoParameters => new MethodDescriptor
        {
            Name = "Method3",
            ReturnType = new ReturnType("int", false),
            Parameters = new List<ParameterDescriptor>
            {
                new ParameterDescriptor(new Type("Type1", "SomeLib.Namespace"), "param1", false),
                new ParameterDescriptor(new Type("Type2", "SomeLib.Namespace"), "param2", false),
            }
        };



        [Fact]
        public void AddMethod_Should_set_a_unique_name_for_methods_overload()
        {
            var mock = new MockDescriptor();
            var method1 = new MethodDescriptor { Name = "DoSomething" };
            var method2 = new MethodDescriptor { Name = "DoSomething" };
            var method3 = new MethodDescriptor { Name = "DoSomething" };
            
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
            MethodThatReturnsWithoutParameter.MethodSetupWithTypedParameters.Should().Be("MethodSetupReturn<int>");
            MethodThatReturnsWithOneParameter.MethodSetupWithTypedParameters.Should().Be("MethodSetupReturn<Type1, int>");
            MethodThatReturnsWithTwoParameters.MethodSetupWithTypedParameters.Should().Be("MethodSetupReturn<Type1, Type2, int>");
        }

        [Fact]
        public void IMethodSetupWithTypedParameters_Should_build_IMethodSetup_with_generic_typed_parameters()
        {
            MethodVoid.IMethodSetupWithTypedParameters.Should().Be("IMethodSetup");
            MethodVoidWithParam.IMethodSetupWithTypedParameters.Should().Be("IMethodSetup<Type1>");
            MethodVoidWithTwoParam.IMethodSetupWithTypedParameters.Should().Be("IMethodSetup<Type1, Type2>");
            MethodThatReturnsWithoutParameter.IMethodSetupWithTypedParameters.Should().Be("IMethodSetupReturn<int>");
            MethodThatReturnsWithOneParameter.IMethodSetupWithTypedParameters.Should().Be("IMethodSetupReturn<Type1, int>");
            MethodThatReturnsWithTwoParameters.IMethodSetupWithTypedParameters.Should().Be("IMethodSetupReturn<Type1, Type2, int>");
        }

        [Fact]
        public void ParametersDeclaration_Should_concat_parameters_type_and_names()
        {
            MethodThatReturnsWithoutParameter.ParametersDeclaration.Should().BeEmpty();
            MethodThatReturnsWithOneParameter.ParametersDeclaration.Should().Be("Type1 param1");
            MethodThatReturnsWithTwoParameters.ParametersDeclaration.Should().Be("Type1 param1, Type2 param2");
        }

        [Fact]
        public void ParametersDeclarationWithArg_Should_build_method_parameter_declaration_with_Arg()
        {
            MethodThatReturnsWithoutParameter.ParametersDeclarationWithArg.Should().BeEmpty();
            MethodThatReturnsWithOneParameter.ParametersDeclarationWithArg.Should().Be("Arg<Type1> param1");
            MethodThatReturnsWithTwoParameters.ParametersDeclarationWithArg.Should().Be("Arg<Type1> param1, Arg<Type2> param2");
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
