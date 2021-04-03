using FluentAssertions;
using MockGen.Model;
using MockGen.ViewModel;
using System.Collections.Generic;
using Xunit;

namespace MockGen.Tests.ViewModel
{
    public class MockViewTests
    {
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
                        ReturnType = new ReturnType("Type1", false, returnedTypeNamespace),
                        Parameters = new List<ParameterDescriptor>
                        {
                            new ParameterDescriptor(new Type("Type2", parameterTypeNamespace), "param1", false),
                        },
                    }
                }
            };
            var view = new MockView(mock);

            // When
            var namespaces = view.Namespaces;

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
                        ReturnType = new ReturnType("Type1", false, typeToMockNamespace),
                        Parameters = new List<ParameterDescriptor>
                        {
                            new ParameterDescriptor(new Type("Type1", typeToMockNamespace), "param1", false),
                        },
                    }
                }
            };
            var view = new MockView(mock);

            // When
            var namespaces = view.Namespaces;

            // Then
            namespaces.Should().HaveCount(1).And.Contain(typeToMockNamespace);
        }
    }
}
