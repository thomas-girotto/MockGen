using System.Collections.Generic;
using System.Linq;

namespace MockGen.Model
{
    public class MockDescriptor
    {
        public string TypeToMock { get; set; }
        public string TypeToMockOriginalNamespace { get; set; }
        public List<MethodDescriptor> Methods { get; set; }
        public string MockCtorArgumentListDefinition => string.Join(
            ", ", 
            Methods.Select(m => $"{m.MethodSetupWithTypedParameters} {m.NameCamelCase}Setup"));

        public string MockCtorParameters => string.Join(", ", Methods.Select(m => $"{m.NameCamelCase}Setup"));

        public IEnumerable<GenericTypesDescriptor> NumberOfParametersInMethods => 
            Methods.GroupBy(
                m => m.Parameters.Count,
                (n, methodsInGroup) => new GenericTypesDescriptor
                {
                    NumberOfTypes = n,
                    HasMethodThatReturnsVoid = methodsInGroup.Any(m => m.ReturnsVoid),
                    HasMethodThatReturns = methodsInGroup.Any(m => !m.ReturnsVoid),
                });
    }
}
