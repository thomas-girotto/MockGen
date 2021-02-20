using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace MockGen.Tests.Model
{
    public class MockDescriptorTests
    {
        public MockDescriptor GetDefaultDescriptor()
        {
            return new MockDescriptor
            {
                Methods = new List<MethodDescriptor>
                {
                    new MethodDescriptor
                    {
                        Name = "MethodVoid",
                        ReturnsVoid = true,
                        ReturnType = string.Empty,
                    },
                    new MethodDescriptor
                    {
                        Name = "MethodTReturn",
                        ReturnType = "int",
                    },
                    new MethodDescriptor
                    {
                        Name = "MethodTParamTReturn",
                        ReturnType = "int",
                        Parameters = new List<ParameterDescriptor>
                        {
                            new ParameterDescriptor("Type1", "param1"),
                        },
                    }
                }
            };
        }

        [Fact]
        public void Should_build_Mock_Ctor_parameter_list_definition()
        {
            var model = GetDefaultDescriptor();
            var mockCtorArgumentListDefinition = model.MockCtorArgumentListDefinition;

            mockCtorArgumentListDefinition.Should().Be("MethodSetupVoid methodVoidSetup, MethodSetupReturn<int> methodTReturnSetup, MethodSetupReturn<Type1, int> methodTParamTReturnSetup");
        }

        [Fact]
        public void Should_build_Mock_Ctor_parameters_to_pass()
        {
            var model = GetDefaultDescriptor();
            var mockCtorParameters = model.MockCtorParameters;

            mockCtorParameters.Should().Be("methodVoidSetup, methodTReturnSetup, methodTParamTReturnSetup");
        }
    }
}
