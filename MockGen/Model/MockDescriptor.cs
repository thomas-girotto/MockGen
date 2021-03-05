using System.Collections.Generic;
using System.Linq;

namespace MockGen.Model
{
    public class MockDescriptor
    {
        private List<CtorDescriptor> _ctors = new List<CtorDescriptor>();

        public string TypeToMock { get; set; }
        public string TypeToMockOriginalNamespace { get; set; }
        public List<MethodDescriptor> Methods { get; set; }
        public List<CtorDescriptor> Ctors
        {
            get => _ctors;
            set
            {
                _ctors = (value == null || value.Count == 0)
                    ? new List<CtorDescriptor> { CtorDescriptor.EmptyCtor }
                    : value;
            }
        }
        public bool IsInterface { get; set; }

        public IEnumerable<string> Namespaces => new List<string> { TypeToMockOriginalNamespace }
            .Concat(Methods.SelectMany(m => m.Parameters.Select(p => p.Namespace)))
            .Concat(Ctors.SelectMany(c => c.Parameters.Select(p => p.Namespace)))
            .Distinct();

        public string CallBaseCtorIfNeeded => IsInterface ? "" : " : base({0})";

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
