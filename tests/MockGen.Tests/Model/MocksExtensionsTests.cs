using FluentAssertions;
using MockGen.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MockGen.Tests.Model
{
    public class MocksExtensionsTests
    {
        [Fact]
        public void Should_group_all_mocks_methods_by_number_of_typed_parameters_whatever_types_are()
        {
            // Given
            var mocks = new List<MockDescriptor> {
                new MockDescriptor
                {
                    TypeToMockNamespace = "SomeLib.Namespace",
                    Methods = new List<MethodDescriptor>
                    {
                        new MethodDescriptor
                        {
                            Name = "MethodWithOneTypedParameterAndReturnSomething1",
                            ReturnType = "ReturnedType1",
                            Parameters = new List<ParameterDescriptor>
                            {
                                new ParameterDescriptor("Type1", "param1", "SomeLib.Namespace"),
                            },
                        },
                        new MethodDescriptor
                        {
                            Name = "MethodWithOneTypedParameterThatReturnsVoid1",
                            ReturnsVoid = true,
                            Parameters = new List<ParameterDescriptor>
                            {
                                new ParameterDescriptor("Type3", "param1", "SomeLib.Namespace"),
                            },
                        },
                    }
                },
                new MockDescriptor
                {
                    Methods = new List<MethodDescriptor>
                    {
                        new MethodDescriptor
                        {
                            Name = "MethodWithOneTypedParameterAndReturnSomething2",
                            ReturnType = "ReturnedType2",
                            Parameters = new List<ParameterDescriptor>
                            {
                                new ParameterDescriptor("Type2", "param1", "SomeLib.Namespace"),
                            },
                        },
                        new MethodDescriptor
                        {
                            Name = "MethodWithOneTypedParameterThatReturnsVoid2",
                            ReturnsVoid = true,
                            Parameters = new List<ParameterDescriptor>
                            {
                                new ParameterDescriptor("Type4", "param1", "SomeLib.Namespace"),
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
                .And.Contain(new TypedParameterMethod(1, true, true));
        }
    }
}
