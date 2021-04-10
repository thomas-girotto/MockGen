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
            var mock = new Mock
            {
                TypeToMock = new Type("MyType", typeToMockNamespace),
                Methods = new List<Method>
                {
                    new Method
                    {
                        Name = "MethodTParamTReturn",
                        ReturnType = new ReturnType("Type1", TaskInfo.NotATask, returnedTypeNamespace),
                        Parameters = new List<Parameter>
                        {
                            new Parameter(new Type("Type2", parameterTypeNamespace), "param1", false),
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
            var mock = new Mock
            {
                TypeToMock = new Type("MyType", typeToMockNamespace),
                Methods = new List<Method>
                {
                    new Method
                    {
                        Name = "MethodTParamTReturn",
                        ReturnType = new ReturnType("Type1", TaskInfo.NotATask, typeToMockNamespace),
                        Parameters = new List<Parameter>
                        {
                            new Parameter(new Type("Type1", typeToMockNamespace), "param1", false),
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

        [Fact]
        public void Should_add_callBase_parameter_to_all_constructors_When_mocking_a_concrete_class()
        {
            var mock = new MockView(new Mock
            {
                IsInterface = false,
                TypeToMock = new Type("SomeType"),
                Ctors = new List<Ctor>
                {
                    new Ctor(),
                    new Ctor { Parameters = new List<Parameter> { new Parameter(new Type("int"), "param1", false) } },
                }
            });

            mock.CtorsParameters.Should().HaveCount(2);
            mock.CtorsParameters[0].MockConstructorParameterNames.Should().Be("methods, callBase");
            mock.CtorsParameters[0].MockConstructorParameterDeclaration.Should().Be("SomeTypeMethodsSetup methods, bool callBase");
            mock.CtorsParameters[0].MockBuilderConstructorParameterDeclaration.Should().Be("bool callBase");
            mock.CtorsParameters[1].MockConstructorParameterNames.Should().Be("methods, callBase, param1");
            mock.CtorsParameters[1].MockConstructorParameterDeclaration.Should().Be("SomeTypeMethodsSetup methods, bool callBase, int param1");
            mock.CtorsParameters[1].MockBuilderConstructorParameterDeclaration.Should().Be("bool callBase, int param1");
        }
    }
}
