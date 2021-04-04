using FluentAssertions;
using MockGen.Model;
using System.Collections.Generic;
using Xunit;

namespace MockGen.Tests.Model
{
    public class MethodTests
    {
        [Fact]
        public void AddMethod_Should_set_a_unique_name_for_methods_overload()
        {
            var mock = new Mock();
            var method1 = new Method { Name = "DoSomething" };
            var method2 = new Method { Name = "DoSomething" };
            var method3 = new Method { Name = "DoSomething" };
            
            mock.AddMethod(method1);
            method1.UniqueName.Should().Be("DoSomething");

            mock.AddMethod(method2);
            method2.UniqueName.Should().Be("DoSomething1");

            mock.AddMethod(method3);
            method3.UniqueName.Should().Be("DoSomething2");
        }
    }
}
