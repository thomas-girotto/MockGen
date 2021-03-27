using FluentAssertions;
using MockGen.Model;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MockGen.Tests.Model
{
    public class MethodDescriptorTests
    {
        private MethodDescriptor GetMethodVoid() => new MethodDescriptor
        {
            Name = "MethodVoid",
            ReturnType = ReturnType.Void,
        };

        private MethodDescriptor GetMethodVoidWithParam() => new MethodDescriptor
        {
            Name = "MethodVoidWithParam",
            ReturnType = ReturnType.Void,
            Parameters = new List<ParameterDescriptor> { new ParameterDescriptor(new Type("Type1", "SomeLib.Namespace"), "param1") },
        };

        private MethodDescriptor GetMethodVoidWithTwoParam() => new MethodDescriptor
        {
            Name = "MethodVoidWithTwoParam",
            ReturnType = ReturnType.Void,
            Parameters = new List<ParameterDescriptor> 
            { 
                new ParameterDescriptor(new Type("Type1", "SomeLib.Namespace"), "param1"),
                new ParameterDescriptor(new Type("Type2", "SomeLib.Namespace"), "param2"),
            },
        };

        private MethodDescriptor GetMethodWithoutParameter() => new MethodDescriptor
        {
            Name = "Method1",
            ReturnType = new ReturnType("int", false),
        };

        private MethodDescriptor GetMethodWithOneParameter() => new MethodDescriptor
        {
            Name = "Method2",
            ReturnType = new ReturnType("int", false),
            Parameters = new List<ParameterDescriptor> { new ParameterDescriptor(new Type("Type1", "SomeLib.Namespace"), "param1") }
        };

        private MethodDescriptor GetMethodWithTwoParameters() => new MethodDescriptor
        {
            Name = "Method3",
            ReturnType = new ReturnType("int", false),
            Parameters = new List<ParameterDescriptor> 
            { 
                new ParameterDescriptor(new Type("Type1", "SomeLib.Namespace"), "param1"), 
                new ParameterDescriptor(new Type("Type2", "SomeLib.Namespace"), "param2"),
            }
        };

        [Fact]
        public void Should_set_a_unique_name_for_methods_overload()
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
        public void Should_build_MethodSetup_with_generic_typed_parameters()
        {
            string methodSetupVoid = GetMethodVoid().MethodSetupWithTypedParameters;
            string methodSetupVoidWithParam = GetMethodVoidWithParam().MethodSetupWithTypedParameters;
            string methodSetupTReturn = GetMethodWithoutParameter().MethodSetupWithTypedParameters;
            string methodSetupTReturnTParam = GetMethodWithOneParameter().MethodSetupWithTypedParameters;
            string methodSetupTReturnTParam1TParam2 = GetMethodWithTwoParameters().MethodSetupWithTypedParameters;
            
            // Assert
            methodSetupVoid.Should().Be("MethodSetupVoid");
            methodSetupVoidWithParam.Should().Be("MethodSetupVoid<Type1>");
            methodSetupTReturn.Should().Be("MethodSetupReturn<int>");
            methodSetupTReturnTParam.Should().Be("MethodSetupReturn<Type1, int>");
            methodSetupTReturnTParam1TParam2.Should().Be("MethodSetupReturn<Type1, Type2, int>");
        }

        [Fact]
        public void Should_build_IMethodSetup_with_generic_typed_parameters()
        {
            string methodSetupVoid = GetMethodVoid().IMethodSetupWithTypedParameters;
            string methodSetupVoidWithParam = GetMethodVoidWithParam().IMethodSetupWithTypedParameters;
            string methodSetupVoidWithTwoParam = GetMethodVoidWithTwoParam().IMethodSetupWithTypedParameters;
            string methodSetupTReturn = GetMethodWithoutParameter().IMethodSetupWithTypedParameters;
            string methodSetupTReturnTParam = GetMethodWithOneParameter().IMethodSetupWithTypedParameters;
            string methodSetupTReturnTParam1TParam2 = GetMethodWithTwoParameters().IMethodSetupWithTypedParameters;

            // Assert
            methodSetupVoid.Should().Be("IMethodSetup");
            methodSetupVoidWithParam.Should().Be("IMethodSetup<Type1>");
            methodSetupVoidWithTwoParam.Should().Be("IMethodSetup<Type1, Type2>");
            methodSetupTReturn.Should().Be("IMethodSetupReturn<int>");
            methodSetupTReturnTParam.Should().Be("IMethodSetupReturn<Type1, int>");
            methodSetupTReturnTParam1TParam2.Should().Be("IMethodSetupReturn<Type1, Type2, int>");
        }

        [Fact]
        public void Should_build_method_parameter_declaration()
        {
            string parametersDefinition1 = GetMethodWithoutParameter().ParametersDeclaration;
            string parametersDefinition2 = GetMethodWithOneParameter().ParametersDeclaration;
            string parametersDefinition3 = GetMethodWithTwoParameters().ParametersDeclaration;
            var test = string.Join(", ", "methods", null);
            // Assert
            parametersDefinition1.Should().BeEmpty();
            parametersDefinition2.Should().Be("Type1 param1");
            parametersDefinition3.Should().Be("Type1 param1, Type2 param2");
        }

        [Fact]
        public void Should_build_method_parameter_declaration_with_Arg()
        {
            string parametersDefinition1 = GetMethodWithoutParameter().ParametersDeclarationWithArg;
            string parametersDefinition2 = GetMethodWithOneParameter().ParametersDeclarationWithArg;
            string parametersDefinition3 = GetMethodWithTwoParameters().ParametersDeclarationWithArg;

            // Assert
            parametersDefinition1.Should().BeEmpty();
            parametersDefinition2.Should().Be("Arg<Type1> param1");
            parametersDefinition3.Should().Be("Arg<Type1> param1, Arg<Type2> param2");
        }

        [Fact]
        public void Should_build_method_parameters()
        {
            string parameters1 = GetMethodWithoutParameter().ParameterNames;
            string parameters2 = GetMethodWithOneParameter().ParameterNames;
            string parameters3 = GetMethodWithTwoParameters().ParameterNames;

            // Assert
            parameters1.Should().BeEmpty();
            parameters2.Should().Be("param1");
            parameters3.Should().Be("param1, param2");
        }

        [Fact]
        public void Should_add_ForParameter_method_When_parameter_exists()
        {
            string forParameterCall1 = GetMethodWithoutParameter().CallForParameterMethod;
            string forParameterCall2 = GetMethodWithOneParameter().CallForParameterMethod;
            string forParameterCall3 = GetMethodWithTwoParameters().CallForParameterMethod;

            // Assert
            forParameterCall1.Should().BeEmpty();
            forParameterCall2.Should().Be("param1 ?? Arg<Type1>.Null");
            forParameterCall3.Should().Be("param1 ?? Arg<Type1>.Null, param2 ?? Arg<Type2>.Null");
        }
    }
}
