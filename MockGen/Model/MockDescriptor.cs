﻿using System.Collections.Generic;
using System.Linq;

namespace MockGen
{
    public class MockDescriptor
    {
        public string TypeToMock { get; set; }
        public string TypeToMockNamespace { get; set; }
        public List<MethodDescriptor> Methods { get; set; }
        public string MockCtorArgumentListDefinition => string.Join(", ", Methods.Select(m => $"MethodSetup<{m.ReturnType}> {m.NameCamelCase}Setup"));
        public string MockCtorParameters => string.Join(", ", Methods.Select(m => $"{m.NameCamelCase}Setup"));
    }
}
