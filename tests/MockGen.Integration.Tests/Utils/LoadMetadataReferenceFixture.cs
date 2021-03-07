using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace MockGen.Integration.Tests
{

    [CollectionDefinition("Load Metadata References")]
    public class LoadMetadataReferenceFixture : ICollectionFixture<LoadMetadataReferenceFixture>
    {
        public List<MetadataReference> MetadataReferences { get; } = new List<MetadataReference>();
        public List<string> CommonSources { get; } = new List<string>();

        public LoadMetadataReferenceFixture()
        {
            // Load metadata from assemblies
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                if (!assembly.IsDynamic)
                {
                    MetadataReferences.Add(MetadataReference.CreateFromFile(assembly.Location));
                }
            }

            
        }
    }
}
