using System.Collections.Generic;
using System.Linq;

namespace MockGen.Model
{
    public class CtorDescriptor
    {
        public static CtorDescriptor EmptyCtor = new CtorDescriptor();
        public List<ParameterDescriptor> Parameters { get; set; } = new List<ParameterDescriptor>();

        public string ParametersDeclaration =>
            string.Join(", ", Parameters.Select(p => $"{p.Type.Name} {p.Name}"));

        public string ConcatParametersDeclarationWith(string firstParameter) =>
            string.Join(", ", new List<string> { firstParameter }
                .Concat(Parameters.Select(p => $"{p.Type.Name} {p.Name}"))
                .Where(s => !string.IsNullOrEmpty(s)));

        public string ConcatParametersNameWith(string firstParameter) =>
            string.Join(", ", new List<string> { firstParameter }
                .Concat(Parameters.Select(p => $"{p.Name}"))
                .Where(s => !string.IsNullOrEmpty(s)));

        public string ParametersDeclarationWithArg =>
            string.Join(", ", Parameters.Select(p => $"Arg<{p.Type.Name}> {p.Name}"));

        /// <summary>
        /// Give the concatenated list of parameters name, so that we can use them to call a method
        /// </summary>
        public string ParameterNames => string.Join(", ", Parameters.Select(p => p.Name));

        public string ParameterTypes => string.Join(", ", Parameters.Select(p => p.Type.Name));
    }
}
