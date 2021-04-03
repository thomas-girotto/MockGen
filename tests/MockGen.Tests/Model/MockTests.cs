using FluentAssertions;
using MockGen.Model;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MockGen.Tests.Model
{
    public class MockTests
    {
        [Fact]
        public void Should_create_a_default_empty_constructor_When_null_constructors_given()
        {
            // Given
            var mock = new Mock();
            // When
            mock.Ctors = null;
            // Then
            mock.Ctors.Should().HaveCount(1).And.Contain(CtorDescriptor.EmptyCtor);
        }

        [Fact]
        public void Should_create_a_default_empty_constructor_When_empty_list_constructors_given()
        {
            // Given
            var mock = new Mock();
            // When
            mock.Ctors = new List<CtorDescriptor>();
            // Then
            mock.Ctors.Should().HaveCount(1).And.Contain(CtorDescriptor.EmptyCtor);
        }
    }
}
