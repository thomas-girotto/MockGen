using System.Collections.Generic;
using System.Linq;

namespace MockGen.Model
{
    public static class MocksExtensions
    {
        public static IEnumerable<TypedParameterMethod> GetAllMethodsGroupedByTypeParameter(this IEnumerable<MockDescriptor> mocks)
        {
            var initTypedMethodsFromProperties = new List<TypedParameterMethod>();

            if (mocks.Any(m => m.Properties.Any()))
            {
                // set property is configured via MethodSetupVoid<T>
                initTypedMethodsFromProperties.Add(new TypedParameterMethod(1, true, false));
                // get property is configured via MethodSetupReturn<T> (here T is the return type and not a parameter type)
                initTypedMethodsFromProperties.Add(new TypedParameterMethod(0, false, true));
            }

            return mocks
                .SelectMany(mock => mock.Methods)
                .Select(m => new TypedParameterMethod(m.Parameters.Count, m.ReturnsVoid, !m.ReturnsVoid))
                .Union(initTypedMethodsFromProperties)
                .GroupBy(
                    m => m.NumberOfTypedParameters,
                    (n, methodsInGroup) => new TypedParameterMethod(n, methodsInGroup.Any(m => m.HasMethodThatReturnsVoid), methodsInGroup.Any(m => m.HasMethodThatReturns))
                )
                .Distinct();
        }
    }
}
