using System.Collections.Generic;

namespace MockGen
{
    public class MethodDescriptor
    {
        public string ReturnType { get; set; }

        public string Name { get; set; }

        public string NameCamelCase => string.IsNullOrEmpty(Name) 
            ? string.Empty
            : char.ToLower(Name[0]) + Name.Substring(1);

        public List<ParameterDescriptor> Parameters { get; set; }
    }
}
