using FluentAssertions;
using MockGen.Model;
using System.Collections.Generic;
using Xunit;

namespace MockGen.Tests.Model
{
    public class MockDescriptorSanitizerTests
    {
        [Theory]
        [InlineData("TypeName", "Sut.Namespace1", "Sut.Namespace2", "TypeNameNamespace1", "TypeNameNamespace2")]
        [InlineData("TypeName", "Sut1.Namespace", "Sut2.Namespace", "TypeNameNamespaceSut1", "TypeNameNamespaceSut2")]
        [InlineData("TypeName", "Sut.Namespace.SubPath", "Sut.Namespace", "TypeNameSubPath", "TypeNameNamespace")]
        [InlineData("TypeName", "Namespace.SubPath", "Sut.Namespace.SubPath", "TypeNameSubPathNamespace", "TypeNameSubPathNamespaceSut")]
        public void Should_avoid_collision_between_types_by_suffixing_with_parts_of_namespace(string type, string namespace1, string namespace2, string expectedType1, string expectedType2)
        {
            // Given
            var mocks = new List<MockDescriptor>
            {
                new MockDescriptor
                {
                    TypeToMock = type,
                    TypeToMockNamespace = namespace1,
                },
                new MockDescriptor
                {
                    TypeToMock = type,
                    TypeToMockNamespace = namespace2,
                }
            };

            // When
            MockDescriptorSanitizer.Sanitize(mocks);

            // Then
            mocks[0].TypeToMock.Should().Be(expectedType1);
            mocks[1].TypeToMock.Should().Be(expectedType2);
        }
    }
}
