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
                        Name = "Method1",
                        ReturnType = "int",
                    },
                    new MethodDescriptor
                    {
                        Name = "Method2",
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
        public void Should_build_typed_parameters_for_a_given_method()
        {
            var model = GetDefaultDescriptor();
            string method1TypedParameters = model.GetTypedParameters(model.Methods[0]);
            string method2TypedParameters = model.GetTypedParameters(model.Methods[1]);

            // Assert
            method1TypedParameters.Should().Be("int");
            method2TypedParameters.Should().Be("int, Type1");
        }

        [Fact]
        public void Should_build_Mock_Ctor_parameter_list_definition()
        {
            var model = GetDefaultDescriptor();
            var mockCtorArgumentListDefinition = model.MockCtorArgumentListDefinition;

            mockCtorArgumentListDefinition.Should().Be("MethodSetup<int> method1Setup, MethodSetup<int, Type1> method2Setup");
        }

        [Fact]
        public void Should_build_Mock_Ctor_parameters_to_pass()
        {
            var model = GetDefaultDescriptor();
            var mockCtorParameters = model.MockCtorParameters;

            mockCtorParameters.Should().Be("method1Setup, method2Setup");
        }
    }
}
