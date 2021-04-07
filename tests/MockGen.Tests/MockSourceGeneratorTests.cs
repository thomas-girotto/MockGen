using FluentAssertions;
using Microsoft.CodeAnalysis;
using MockGen.Model;
using MockGen.Tests.Fixtures;
using MockGen.Tests.Utils;
using System.Linq;
using Xunit;
using Xunit.Sdk;

namespace MockGen.Tests
{
    public class MockSourceGeneratorTests : IClassFixture<LoadMetadataReferenceFixture>
    {
        private readonly LoadMetadataReferenceFixture metadata;

        public MockSourceGeneratorTests(LoadMetadataReferenceFixture metadata)
        {
            this.metadata = metadata;
        }

        [Fact]
        public void Should_find_all_namespaces_in_generic_type()
        {
            // Given
            string source = @"
using System.Net;
using System.Collections.Generic;
namespace MockGen.Tests
{
    public interface IDependency 
    {
        List<HttpStatusCode> GetSomeStatusCodes();
    }
    public class Generators
    {
        public void GenerateMocks()
        {
            MockGenerator.Generate<IDependency>();
        }
    }
}";
            // When
            var (generator, diagnostics) = SourceCompiler.Compile(source, metadata.MetadataReferences);

            // Then
            if (diagnostics.Any())
            {
                throw new XunitException($"Compilation error happened, check diagnostic message: {diagnostics.First().Descriptor.Description}");
            }
            generator.TypesToMock.Should().HaveCount(1);
            generator.TypesToMock[0].Methods.Should().HaveCount(1);
            generator.TypesToMock[0].Methods[0].ReturnType.Name.Should().Be("List<HttpStatusCode>");
            generator.TypesToMock[0].Methods[0].ReturnType.Namespaces.Should().ContainInOrder("System.Collections.Generic", "System.Net");
        }

        [Fact]
        public void Should_handle_special_case_of_return_type_being_a_Task_of_T()
        {
            // Given
            string source = @"
using System.Net;
using System.Threading.Tasks;
namespace MockGen.Tests
{
    public interface IDependency 
    {
        Task<HttpStatusCode> GetSomeStatusCodeAsync();
    }
    public class Generators
    {
        public void GenerateMocks()
        {
            MockGenerator.Generate<IDependency>();
        }
    }
}";
            // When
            var (generator, diagnostics) = SourceCompiler.Compile(source, metadata.MetadataReferences);

            // Then
            if (diagnostics.Any())
            {
                throw new XunitException($"Compilation error happened, check diagnostic message: {diagnostics.First().Descriptor.Description}");
            }
            generator.TypesToMock.Should().HaveCount(1);
            generator.TypesToMock[0].Methods.Should().HaveCount(1);
            generator.TypesToMock[0].Methods[0].ReturnType.TaskInfo.Should().Be(TaskInfo.Task);
            generator.TypesToMock[0].Methods[0].ReturnType.Name.Should().Be("HttpStatusCode"); // And not Task<HttpStatusCode>
        }

        [Fact]
        public void Should_handle_special_case_of_return_type_being_a_ValueTask_of_T()
        {
            // Given
            string source = @"
using System.Net;
using System.Threading.Tasks;
namespace MockGen.Tests
{
    public interface IDependency 
    {
        ValueTask<HttpStatusCode> GetSomeStatusCodeAsync();
    }
    public class Generators
    {
        public void GenerateMocks()
        {
            MockGenerator.Generate<IDependency>();
        }
    }
}";
            // When
            var (generator, diagnostics) = SourceCompiler.Compile(source, metadata.MetadataReferences);

            // Then
            if (diagnostics.Any())
            {
                throw new XunitException($"Compilation error happened, check diagnostic message: {diagnostics.First().Descriptor.Description}");
            }
            generator.TypesToMock.Should().HaveCount(1);
            generator.TypesToMock[0].Methods.Should().HaveCount(1);
            generator.TypesToMock[0].Methods[0].ReturnType.TaskInfo.Should().Be(TaskInfo.ValueTask);
            generator.TypesToMock[0].Methods[0].ReturnType.Name.Should().Be("HttpStatusCode"); // And not ValueTask<HttpStatusCode>
        }

        [Fact]
        public void Should_differentiate_two_types_having_the_same_name_by_adding_parts_of_the_fully_qualified_name()
        {
            var source = @"
using MockGen;
namespace Sut.Namespace1
{
    public interface IDependency {}
}
namespace Sut.Namespace2
{
    public interface IDependency {}
}
namespace MyLib.Tests
{
    public class Generators
    {
        public void Generate()
        {
            MockGenerator.Generate<Sut.Namespace1.IDependency>();
            MockGenerator.Generate<Sut.Namespace2.IDependency>();
        }
    }
}
";
            // When
            var (generator, diagnostics) = SourceCompiler.Compile(source, metadata.MetadataReferences);

            // Then
            if (diagnostics.Any())
            {
                throw new XunitException($"Compilation error happened, check diagnostic message: {diagnostics.First().Descriptor.Description}");
            }
            generator.TypesToMock.Should().HaveCount(2);
            generator.TypesToMock[0].Properties.Should().BeEmpty();
            generator.TypesToMock[0].TypeToMock.Name.Should().Be("IDependencyNamespace1");
            generator.TypesToMock[1].TypeToMock.Name.Should().Be("IDependencyNamespace2");
        }

        [Fact]
        public void Should_handle_methods_overload()
        {
            // Given
            string source = @"
using MockGen;
namespace MockGen.Tests
{
    public interface IDependency 
    {
        void DoSomething();
        void DoSomething(int value);
    }
    public class Generators
    {
        public void GenerateMocks()
        {
            MockGenerator.Generate<IDependency>();
        }
    }
}";
            // When
            var (generator, diagnostics) = SourceCompiler.Compile(source, metadata.MetadataReferences);

            // Then
            if (diagnostics.Any())
            {
                throw new XunitException($"Compilation error happened, check diagnostic message: {diagnostics.First().Descriptor.Description}");
            }
            generator.TypesToMock.Should().HaveCount(1);
            generator.TypesToMock[0].Methods.Should().HaveCount(2);
        }

        [Fact]
        public void Should_recognize_get_property_from_method()
        {
            // Given
            string source = @"
using MockGen;
namespace MockGen.Tests
{
    public interface IDependency 
    {
        int GetProperty { get; }
    }
    public class Generators
    {
        public void GenerateMocks()
        {
            MockGenerator.Generate<IDependency>();
        }
    }
}";
            // When
            var (generator, diagnostics) = SourceCompiler.Compile(source, metadata.MetadataReferences);

            // Then
            if (diagnostics.Any())
            {
                throw new XunitException($"Compilation error happened, check diagnostic message: {diagnostics.First().Descriptor.Description}");
            }
            generator.TypesToMock.Should().HaveCount(1);
            generator.TypesToMock[0].Methods.Should().BeEmpty();
            generator.TypesToMock[0].Properties.Should().HaveCount(1);
            generator.TypesToMock[0].Properties[0].HasGetter.Should().BeTrue();
            generator.TypesToMock[0].Properties[0].HasSetter.Should().BeFalse();
        }
    }
}
