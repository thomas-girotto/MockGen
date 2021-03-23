using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MockGen.Tests.Fixtures
{
    public class LoadMetadataReferenceFixture
    {
        public List<MetadataReference> MetadataReferences { get; } = new List<MetadataReference>();

        public LoadMetadataReferenceFixture()
        {
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
