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
                TypeToMockOriginalNamespace = "SomeLib.TypeNamespace",
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
                            new ParameterDescriptor("Type1", "param1", "SomeLib.Namespace1"),
                        },
                    }
                }
            };
        }

        [Fact]
        public void Should_create_a_default_empty_constructor_When_null_constructors_given()
        {
            // Given
            var mock = new MockDescriptor();
            // When
            mock.Ctors = null;
            // Then
            mock.Ctors.Should().HaveCount(1).And.Contain(CtorDescriptor.EmptyCtor);
        }

        [Fact]
        public void Should_create_a_default_empty_constructor_When_empty_list_constructors_given()
        {
            // Given
            var mock = new MockDescriptor();
            // When
            mock.Ctors = new List<CtorDescriptor>();
            // Then
            mock.Ctors.Should().HaveCount(1).And.Contain(CtorDescriptor.EmptyCtor);
        }

        [Fact]
        public void Should_give_all_namespaces_from_all_types()
        {
            var mock = GetDefaultDescriptor();
            mock.Ctors.Add(new CtorDescriptor
            {
                Parameters = new List<ParameterDescriptor>
                {
                    new ParameterDescriptor("Type2", "param2", "SomeLib.OtherNamespace"),
                }
            });

            var namespaces = mock.Namespaces;

            namespaces.Should().HaveCount(3);
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
