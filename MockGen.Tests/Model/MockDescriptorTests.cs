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
