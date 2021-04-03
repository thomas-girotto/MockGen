using System;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Model
{
    public class Ctor
    {
        public static Ctor EmptyCtor = new Ctor();
        public List<ParameterDescriptor> Parameters { get; set; } = new List<ParameterDescriptor>();
        public List<ParameterDescriptor> ParametersWithoutOutParams => Parameters.Where(p => !p.IsOutParameter).ToList();
        public List<ParameterDescriptor> OutParameters => Parameters.Where(p => p.IsOutParameter).ToList();
        
        public string ParametersDeclaration =>
            string.Join(", ", ParametersWithoutOutParams.Select(p => $"{p.Type.Name} {p.Name}"));

        public string ParametersDeclarationWithOutParameters =>
            string.Join(", ", Parameters.Select(p => !p.IsOutParameter
                ? $"{p.Type.Name} {p.Name}"
                : $"out {p.Type.Name} {p.Name}"));

        public string ConcatParametersDeclarationWith(string firstParameter) =>
            string.Join(", ", new List<string> { firstParameter }
                .Concat(Parameters.Select(p => $"{p.Type.Name} {p.Name}"))
                .Where(s => !string.IsNullOrEmpty(s)));

        public string ConcatParametersNameWith(string firstParameter) =>
            string.Join(", ", new List<string> { firstParameter }
                .Concat(Parameters.Select(p => $"{p.Name}"))
                .Where(s => !string.IsNullOrEmpty(s)));

        public string ParameterNames => string.Join(", ", ParametersWithoutOutParams.Select(p => p.Name));
        
        public string OutParameterNames => OutParameters.Count == 1 ? OutParameters.First().Name : $"({string.Join(", ", OutParameters.Select(p => p.Name))})";

        public string ParameterTypes => string.Join(", ", Parameters.Select(p => p.Type.Name));
        
        public string OutParameterTypes => string.Join(", ", OutParameters.Select(p => p.Type.Name));

        public string OutParameterTypesDefault => OutParameters.Count == 1 ? OutParameters.First().Type.Name : $"({OutParameterTypes})";
        
        public string ParameterNamesWithoutOutParameters => string.Join(", ", ParametersWithoutOutParams.Select(p => p.Name));
        
        public string ParameterTypesWithoutOutParameters => string.Join(", ", ParametersWithoutOutParams.Select(p => p.Type.Name));

        public string DiscardParameters => string.Join(", ", ParametersWithoutOutParams.Select(p => "_"));

        public string OutParameterSetupFunc =>
            (ParametersWithoutOutParams.Count, OutParameters.Count) switch
            {
                (_, 0) => string.Empty,
                (_, 1) => $"Func<{string.Join(", ", ParametersWithoutOutParams.Select(p => p.Type.Name).Concat(new string[] { OutParameters.First().Type.Name }))}>",
                (0, > 1) => $"Func<({string.Join(", ", OutParameters.Select(p => $"{p.Type.Name} {p.Name}"))})>",
                ( > 0, > 1) => $"Func<{string.Join(", ", ParametersWithoutOutParams.Select(p => p.Type.Name))}, ({string.Join(", ", OutParameters.Select(p => $"{p.Type.Name} {p.Name}"))})>",
                _ => throw new NotImplementedException(),
            };

        public string ParametersDeclarationWithArg =>
            string.Join(", ", ParametersWithoutOutParams
                .Select(p => $"Arg<{p.Type.Name}> {p.Name}"));

        public string ParametersDeclarationWithArgAndOutParameters =>
            string.Join(", ", ParametersWithoutOutParams
                .Select(p => $"Arg<{p.Type.Name}> {p.Name}")
                .Concat(new string[] { OutParameters.Any() ? $"{OutParameterSetupFunc} setupOutParameter" : string.Empty })
                .Where(s => !string.IsNullOrEmpty(s)));
    }
}
