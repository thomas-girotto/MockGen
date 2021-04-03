using FluentAssertions;
using MockGen.Model;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MockGen.Tests.Model
{
    public class MocksExtensionsTests
    {
        [Fact]
        public void Should_group_all_mocks_methods_by_number_of_typed_parameters_whatever_types_are()
        {
            // Given
            var mocks = new List<Mock> {
                new Mock
                {
                    TypeToMock = new Type("IDependency", "SomeLib.Namespace"),
                    Methods = new List<Method>
                    {
                        new Method
                        {
                            Name = "MethodWithOneTypedParameterAndReturnSomething1",
                            ReturnType = new ReturnType("ReturnedType1", false, "SomeLib.Namespace"),
                            Parameters = new List<Parameter>
                            {
                                new Parameter(new Type("Type1", "SomeLib.Namespace"), "param1", false),
                            },
                        },
                        new Method
                        {
                            Name = "MethodWithOneTypedParameterThatReturnsVoid1",
                            ReturnType = ReturnType.Void,
                            Parameters = new List<Parameter>
                            {
                                new Parameter(new Type("Type3", "SomeLib.Namespace"), "param1", false),
                            },
                        },
                    }
                },
                new Mock
                {
                    Methods = new List<Method>
                    {
                        new Method
                        {
                            Name = "MethodWithOneTypedParameterAndReturnSomething2",
                            ReturnType = new ReturnType("ReturnedType2", false, "SomeLib.Namespace"),
                            Parameters = new List<Parameter>
                            {
                                new Parameter(new Type("Type2", "SomeLib.Namespace"), "param1", false),
                            },
                        },
                        new Method
                        {
                            Name = "MethodWithOneTypedParameterThatReturnsVoid2",
                            ReturnType = ReturnType.Void,
                            Parameters = new List<Parameter>
                            {
                                new Parameter(new Type("Type4", "SomeLib.Namespace"), "param1", false),
                            },
                        },
                    }
                }
            };

            // When
            var numberOfParameters = mocks.GetAllMethodsGroupedByTypeParameter();

            // Then
            numberOfParameters.Should()
                .HaveCount(1)
                .And.Contain(new TypedParameterMethod(1, true, true, false));
        }

        [Fact]
        public void Should_create_one_method_with_one_parameter_and_one_method_returning_something_When_property_exists()
        {
            // That's because we're using MethodSetupVoid<T> to setup the Set property and MethodSetupReturn<T>
            // to setup Get property => we make sur that we generate those classes
            // Given
            var mocks = new List<Mock> {
                new Mock
                {
                    TypeToMock = new Type("IDependency", "SomeLib.Namespace"),
                    Properties = new List<PropertyDescriptor>
                    {
                        new PropertyDescriptor
                        {
                            Name = "GetSetProperty",
                            HasGetter = true,
                            HasSetter = true,
                            Type = new ReturnType("SomeModel", false, "SomeLib.Namespace")
                        },
                    }
                },
            };

            // When
            var numberOfParameters = mocks.GetAllMethodsGroupedByTypeParameter();

            // Then
            numberOfParameters.Should()
                .HaveCount(2)
                .And.Contain(new TypedParameterMethod(0, false, true, false))
                .And.Contain(new TypedParameterMethod(1, true, false, false));
        }
    }
}
