using FluentAssertions;
using Microsoft.CodeAnalysis;
using MockGen.Tests.Fixtures;
using MockGen.Tests.Utils;
using System.Linq;
using Xunit;
using Xunit.Sdk;

namespace MockGen.Tests
{
    public class MockSourceGeneratorClassTests : IClassFixture<LoadMetadataReferenceFixture>
    {
        private readonly LoadMetadataReferenceFixture metadata;

        public MockSourceGeneratorClassTests(LoadMetadataReferenceFixture metadata)
        {
            this.metadata = metadata;
        }

        [Fact]
        public void Should_not_mock_non_virtual_methods()
        {
            var source = @"
using MockGen;
namespace MyLib.Tests
{
    public class SomeDependency 
    {
        public void DoSomethingPublic() { }
        protected void DoSomethingProtected() { }
    }
    public class Generators
    {
        public void Generate()
        {
            MockG.Generate<SomeDependency>();
        }
    }
}
";
            // When
            var (generator, diagnostics) = SourceCompiler.Compile(source, metadata.MetadataReferences);

            // Then
            CompilationCheck.NoError(diagnostics);
            generator.TypesToMock.Should().HaveCount(1);
            var mock = generator.TypesToMock[0];
            mock.Methods.Should().BeEmpty();
        }

        [Fact]
        public void Should_mock_protected_virtual_methods()
        {
            var source = @"
using MockGen;
namespace MyLib.Tests
{
    public class SomeDependency 
    {
        protected virtual void DoSomething() { }
    }
    public class Generators
    {
        public void Generate()
        {
            MockG.Generate<SomeDependency>();
        }
    }
}
";
            // When
            var (generator, diagnostics) = SourceCompiler.Compile(source, metadata.MetadataReferences);

            // Then
            CompilationCheck.NoError(diagnostics);
            generator.TypesToMock.Should().HaveCount(1);
            var mock = generator.TypesToMock[0];
            mock.Methods.Should().ContainSingle(m => m.Name == "DoSomething" && m.IsVirtual && m.IsProtected);
        }

        [Fact]
        public void Should_mock_protected_override_methods()
        {
            var source = @"
using MockGen;
namespace MyLib.Tests
{
    public abstract class SomeBaseClass { protected abstract void DoSomething(); }
    public class SomeDependency : SomeBaseClass
    {
        protected override void DoSomething() { }
    }
    public class Generators
    {
        public void Generate()
        {
            MockG.Generate<SomeDependency>();
        }
    }
}
";
            // When
            var (generator, diagnostics) = SourceCompiler.Compile(source, metadata.MetadataReferences);

            // Then
            CompilationCheck.NoError(diagnostics);
            generator.TypesToMock.Should().HaveCount(1);
            var mock = generator.TypesToMock[0];
            mock.Methods.Should().ContainSingle(m => m.Name == "DoSomething");
        }
    }
}
