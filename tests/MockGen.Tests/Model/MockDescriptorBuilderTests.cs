using FluentAssertions;
using MockGen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MockGen.Tests.Model
{
    public class MockDescriptorBuilderTests
    {
        [Theory]
        [MemberData(nameof(ExtractNamespacesTestValues))]
        public void Should_extract_all_namespaces_from_a_complex_type(string fullName, IEnumerable<string> expectedNamespaces)
        {
            var namespaces = MockDescriptorBuilder.ExtractAllNamespaces(fullName);
            
            namespaces.Should().Equal(expectedNamespaces);
        }

        public static IEnumerable<object[]> ExtractNamespacesTestValues =>
            new List<object[]>
            {
                new object[] { "int", Enumerable.Empty<string>() },
                new object[] { "System.Int32", new string[] { "System" } },
                new object[] { "System.Threading.Task<System.Net.HttpStatusCode>", new string[] { "System.Threading", "System.Net" } },
                new object[] { "System.Func<System.Collections.List<Whatever.SomeType>, System.Threading.Task<System.Net.HttpStatusCode>>", new string[] { "System", "System.Collections", "Whatever", "System.Threading", "System.Net" } },
            };
    }
}
