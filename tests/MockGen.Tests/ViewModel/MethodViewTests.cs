using FluentAssertions;
using MockGen.Model;
using MockGen.ViewModel;
using System.Collections.Generic;
using Xunit;

namespace MockGen.Tests.ViewModel
{
    public class MethodViewTests
    {
        private MethodView MethodVoid => new MethodView(new Method
        {
            Name = "MethodVoid",
            ReturnType = ReturnType.Void,
        });

        private MethodView MethodVoidWithParam => new MethodView(new Method
        {
            Name = "MethodVoidWithParam",
            ReturnType = ReturnType.Void,
            Parameters = new List<Parameter> { new Parameter(new Type("Type1", "SomeLib.Namespace"), "param1", false) },
        });

        private MethodView MethodVoidWithTwoParam => new MethodView(new Method
        {
            Name = "MethodVoidWithTwoParam",
            ReturnType = ReturnType.Void,
            Parameters = new List<Parameter>
            {
                new Parameter(new Type("Type1", "SomeLib.Namespace"), "param1", false),
                new Parameter(new Type("Type2", "SomeLib.Namespace"), "param2", false),
            },
        });

        private MethodView MethodThatReturnsWithoutParameter => new MethodView(new Method
        {
            Name = "Method1",
            ReturnType = new ReturnType("int", TaskInfo.NotATask),
        });

        private MethodView MethodThatReturnsWithOneParameter => new MethodView(new Method
        {
            Name = "Method2",
            ReturnType = new ReturnType("int", TaskInfo.NotATask),
            Parameters = new List<Parameter> { new Parameter(new Type("Type1", "SomeLib.Namespace"), "param1", false) }
        });

        private MethodView MethodThatReturnsWithTwoParameters => new MethodView(new Method
        {
            Name = "Method3",
            ReturnType = new ReturnType("int", TaskInfo.NotATask),
            Parameters = new List<Parameter>
            {
                new Parameter(new Type("Type1", "SomeLib.Namespace"), "param1", false),
                new Parameter(new Type("Type2", "SomeLib.Namespace"), "param2", false),
            }
        });

        private MethodView MethodVoidWithOneOutParameter => new MethodView(new Method
        {
            Name = "Method4",
            ReturnType = ReturnType.Void,
            Parameters = new List<Parameter>
            {
                new Parameter(new Type("TypeOut", "SomeLib.Namespace"), "param1", true),
            }
        });

        private MethodView MethodVoidWithOneParameterAndOneOutParameter => new MethodView(new Method
        {
            Name = "Method5",
            ReturnType = ReturnType.Void,
            Parameters = new List<Parameter>
            {
                new Parameter(new Type("Type1", "SomeLib.Namespace"), "param1", false),
                new Parameter(new Type("TypeOut", "SomeLib.Namespace"), "param2", true),
            }
        });

        private MethodView MethodThatReturnsWithOneOutParameter => new MethodView(new Method
        {
            Name = "Method4",
            ReturnType = new ReturnType("int", TaskInfo.NotATask),
            Parameters = new List<Parameter>
            {
                new Parameter(new Type("TypeOut", "SomeLib.Namespace"), "param1", true),
            }
        });

        private MethodView MethodThatReturnsWithOneParameterAndOneOutParameter => new MethodView(new Method
        {
            Name = "Method5",
            ReturnType = new ReturnType("int", TaskInfo.NotATask),
            Parameters = new List<Parameter>
            {
                new Parameter(new Type("Type1", "SomeLib.Namespace"), "param1", false),
                new Parameter(new Type("TypeOut", "SomeLib.Namespace"), "param2", true),
            }
        });

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
    }
}
