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
            Methods.Select(m => @$"MethodSetup<{GetTypedParameters(m)}> {m.NameCamelCase}Setup"));

        public string MockCtorParameters => string.Join(", ", Methods.Select(m => $"{m.NameCamelCase}Setup"));

        public string GetTypedParameters(MethodDescriptor m) => string.Join(", ", string.Join(", ", new List<string> { m.ReturnType }.Concat(m?.Parameters.Select(p => p.Type)).Where(x => x != null)));

    }
}
