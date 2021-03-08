using FluentAssertions;
using MockGen.Model;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MockGen.Tests.Model
{
    public class MockDescriptorTests
    {
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
            // Given
            var typeToMockNamespace = "SomeLib.TypeNamespace";
            var returnedTypeNamespace = "SomeLib.Namespace.For.ReturnedType";
            var parameterTypeNamespace = "SomeLib.Namespace.For.ParameterType";
            var mock = new MockDescriptor
            {
                TypeToMockNamespace = typeToMockNamespace,
                Methods = new List<MethodDescriptor>
                {
                    new MethodDescriptor
                    {
                        Name = "MethodTParamTReturn",
                        ReturnType = "Type1",
                        ReturnTypeNamespace = returnedTypeNamespace,
                        Parameters = new List<ParameterDescriptor>
                        {
                            new ParameterDescriptor("Type2", "param1", parameterTypeNamespace),
                        },
                    }
                }
            };

            // When
            var namespaces = mock.Namespaces;

            // Then
            namespaces.Should().HaveCount(3).And.Contain(typeToMockNamespace, returnedTypeNamespace, parameterTypeNamespace);
        }

        [Fact]
        public void Should_only_return_same_namespace_once()
        {
            // Given
            var typeToMockNamespace = "SomeLib.TypeNamespace";
            var mock = new MockDescriptor
            {
                TypeToMockNamespace = typeToMockNamespace,
                Methods = new List<MethodDescriptor>
                {
                    new MethodDescriptor
                    {
                        Name = "MethodTParamTReturn",
                        ReturnType = "Type1",
                        ReturnTypeNamespace = typeToMockNamespace,
                        Parameters = new List<ParameterDescriptor>
                        {
                            new ParameterDescriptor("Type1", "param1", typeToMockNamespace),
                        },
                    }
                }
            };

            // When
            var namespaces = mock.Namespaces;

            // Then
            namespaces.Should().HaveCount(1).And.Contain(typeToMockNamespace);
        }
    }
}
