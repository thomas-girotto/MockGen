using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MockGen
{
    public class MockDescriptor
    {
        public string TypeToMock { get; set; }
        public string TypeToMockOriginalNamespace { get; set; }
        public List<MethodDescriptor> Methods { get; set; }
        public string GeneratedNamespace => $"{TypeToMock}Ns";
        public string MockCtorArgumentListDefinition => string.Join(
            ", ", 
            Methods.Select(m => $"{m.MethodSetupWithTypedParameters} {m.NameCamelCase}Setup"));

        public string MockCtorParameters => string.Join(", ", Methods.Select(m => $"{m.NameCamelCase}Setup"));


    }
}
