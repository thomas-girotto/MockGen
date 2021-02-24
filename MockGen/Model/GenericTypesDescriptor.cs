using System;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Model
{
    public struct GenericTypesDescriptor
    {
        private int numberOfTypes;
        public List<int> EnumerateNumbers { get; private set; }

        public int NumberOfTypes 
        {
            get => numberOfTypes;
            set
            {
                numberOfTypes = value;
                EnumerateNumbers = Enumerable.Range(1, value).ToList();
            }
        }
        public bool HasMethodThatReturnsVoid { get; set; }
        public bool HasMethodThatReturns { get; set; }
        public string FileSuffix => NumberOfTypes switch
        {
            0 => string.Empty,
            _ => EnumerateNumbers
                    .Select(n => $"P{n}")
                    .Aggregate(string.Empty, (suffix, next) => suffix + next)
        };

        public string GenericTypes => NumberOfTypes switch
        {
            0 => string.Empty,
            _ => $"<{string.Join(", ", EnumerateNumbers.Select(n => $"TParam{n}"))}>",
        };

        public string DiscardParameters => $"({string.Join(", ", EnumerateNumbers.Select(_ => "_"))})";

        private IEnumerable<string> ClassByParameterType(string className) => EnumerateNumbers.Select(n => $"{className}<TParam{n}>"); 
        
        public string ParametersTypesWithName(string parameterName) => string.Join(", ", EnumerateNumbers.Select(n => $"TParam{n} {parameterName + n}"));
        public string ParametersNames => string.Join(", ", EnumerateNumbers.Select(n => $"param{n}"));

        public string ConcatClassByParameterType(string className) => NumberOfTypes switch
        {
            0 => string.Empty,
            _ => string.Join(", ", ClassByParameterType(className)),
        };

        public string ConcatNewClassByParameterType(string className) => string.Join(", ", ClassByParameterType(className).Select(x => $"new {x}()"));
        public string ConcatClassParameterByParameterType(string className, string instanceName) => string.Join(", ", ClassByParameterType(className).Select((x, i) => $"{x} {instanceName + (i + 1)}"));
        public string ConcatMatcherCalls(string matcherName, string parameterName) => string.Join(" && ", EnumerateNumbers.Select(n => $"{matcherName + n}.Match({parameterName + n})"));
    }
}
