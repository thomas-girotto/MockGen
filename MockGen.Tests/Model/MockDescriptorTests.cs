using FluentAssertions;
using MockGen.Model;
using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public void Should_group_methods_by_number_of_parameters_and_returns_void()
        {
            var model = GetDefaultDescriptor();
            var numberOfParameters = model.NumberOfParametersInMethods.ToList();

            numberOfParameters.Should().HaveCount(2);
            numberOfParameters[0].NumberOfTypes.Should().Be(0);
            numberOfParameters[0].HasMethodThatReturnsVoid.Should().BeTrue();
            numberOfParameters[0].HasMethodThatReturnsVoid.Should().BeTrue();
            numberOfParameters[1].NumberOfTypes.Should().Be(1);
            numberOfParameters[1].HasMethodThatReturnsVoid.Should().BeFalse();
            numberOfParameters[1].HasMethodThatReturns.Should().BeTrue();
        }
    }
}
