using MockGen.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.ViewModel
{
    public class ParametersView
    {
        private readonly List<Parameter> allParameters = new List<Parameter>();
        private readonly List<Parameter> classicParameters = new List<Parameter>();
        private readonly List<Parameter> outParameters = new List<Parameter>();

        public ParametersView(List<Parameter> parameters)
        {
            foreach (var parameter in parameters)
            {
                allParameters.Add(parameter);
                if (parameter.IsOutParameter)
                {
                    outParameters.Add(parameter);
                }
                else
                {
                    classicParameters.Add(parameter);
                }
            }
        }

        public bool HasOutParameter => outParameters.Count > 0;

        public int NumberOfClassicParameters => classicParameters.Count;

        public bool HasClassicParameter => classicParameters.Count > 0;

        public string Names => string.Join(", ", classicParameters.Select(p => p.Name));

        public string TypesAndNames => string.Join(", ", classicParameters.Select(p => $"{p.Type.Name} {p.Name}"));

        public string Types => string.Join(", ", classicParameters.Select(p => p.Type.Name));
        
        public string Discard => string.Join(", ", classicParameters.Select(p => "_"));

        public string OutParameterTypes => string.Join(", ", outParameters.Select(p => p.Type.Name));
        
        public string OutParameterNames => outParameters.Count == 1 ? outParameters.First().Name : $"({string.Join(", ", outParameters.Select(p => p.Name))})";

        public string OutParameterTypesDefault => outParameters.Count == 1 ? outParameters.First().Type.Name : $"({OutParameterTypes})";

        public string ConcatParametersDeclarationWith(string firstParameter) =>
            string.Join(", ", new List<string> { firstParameter }
                .Concat(classicParameters.Select(p => $"{p.Type.Name} {p.Name}"))
                .Where(s => !string.IsNullOrEmpty(s)));

        public string ConcatParametersNameWith(string firstParameter) =>
            string.Join(", ", new List<string> { firstParameter }
                .Concat(classicParameters.Select(p => $"{p.Name}"))
                .Where(s => !string.IsNullOrEmpty(s)));

        public string OutParameterSetupFunc =>
            (classicParameters.Count, outParameters.Count) switch
            {
                (_, 0) => string.Empty,
                (_, 1) => $"Func<{string.Join(", ", classicParameters.Select(p => p.Type.Name).Concat(new string[] { outParameters.First().Type.Name }))}>",
                (0, > 1) => $"Func<({string.Join(", ", outParameters.Select(p => $"{p.Type.Name} {p.Name}"))})>",
                ( > 0, > 1) => $"Func<{string.Join(", ", classicParameters.Select(p => p.Type.Name))}, ({string.Join(", ", outParameters.Select(p => $"{p.Type.Name} {p.Name}"))})>",
                _ => throw new NotImplementedException(),
            };

        public string ParametersDeclarationWithArg =>
            string.Join(", ", classicParameters.Select(p => $"Arg<{p.Type.Name}> {p.Name}"));

        public string ParametersDeclarationWithArgAndOutParameters =>
            string.Join(", ", classicParameters
                .Select(p => $"Arg<{p.Type.Name}> {p.Name}")
                .Concat(new string[] { outParameters.Any() ? $"{OutParameterSetupFunc} setupOutParameter" : string.Empty })
                .Where(s => !string.IsNullOrEmpty(s)));

        public string ParametersDeclarationWithOutParameters =>
            string.Join(", ", allParameters.Select(p => !p.IsOutParameter
                ? $"{p.Type.Name} {p.Name}"
                : $"out {p.Type.Name} {p.Name}"));

        public string CallForParameterMethod => string.Join(", ", classicParameters.Select(p => $"{p.Name} ?? Arg<{p.Type.Name}>.Null"));
    }
}
