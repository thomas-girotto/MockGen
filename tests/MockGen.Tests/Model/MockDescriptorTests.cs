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
                TypeToMock = new Type("MyType", typeToMockNamespace),
                Methods = new List<MethodDescriptor>
                {
                    new MethodDescriptor
                    {
                        Name = "MethodTParamTReturn",
                        ReturnType = new Type("Type1", returnedTypeNamespace),
                        Parameters = new List<ParameterDescriptor>
                        {
                            new ParameterDescriptor(new Type("Type2", parameterTypeNamespace), "param1"),
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
                TypeToMock = new Type("MyType", typeToMockNamespace),
                Methods = new List<MethodDescriptor>
                {
                    new MethodDescriptor
                    {
                        Name = "MethodTParamTReturn",
                        ReturnType = new Type("Type1", typeToMockNamespace),
                        Parameters = new List<ParameterDescriptor>
                        {
                            new ParameterDescriptor(new Type("Type1", typeToMockNamespace), "param1"),
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
