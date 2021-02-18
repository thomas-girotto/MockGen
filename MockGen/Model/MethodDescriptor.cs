﻿using System.Collections.Generic;
using System.Linq;

namespace MockGen
{
    public class MethodDescriptor
    {
        public string ReturnType { get; set; }

        public string Name { get; set; }

        public List<ParameterDescriptor> Parameters { get; set; } = new List<ParameterDescriptor>();

        public string NameCamelCase => string.IsNullOrEmpty(Name)
           ? string.Empty
           : char.ToLower(Name[0]) + Name.Substring(1);

        public string ParametersDeclaration =>
            string.Join(", ", Parameters.Select(p => $"{p.Type} {p.Name}"));

        public string ParametersDeclarationWithArg =>
            string.Join(", ", Parameters.Select(p => $"Arg<{p.Type}> {p.Name}"));

        /// <summary>
        /// Give the concatenated list of parameters name, so that we can use them to call a method
        /// </summary>
        public string ParameterNames => string.Join(", ", Parameters.Select(p => p.Name));

        public string CallForParameterMethod => Parameters.Count == 0
            ? string.Empty
            : $".ForParameter({ParameterNames})";

    }
}
