using System.Collections.Generic;
using System.Linq;

namespace MockGen.Model
{
    public static class MocksExtensions
    {
        public static IEnumerable<TypedParameterMethod> GetAllMethodsGroupedByTypeParameter(this IEnumerable<MockDescriptor> mocks)
        {
            return mocks.SelectMany(mock => mock.Methods)
                    .Where(m => m.Parameters.Count > 0)
                    .GroupBy(
                        m => m.Parameters.Count,
                        (n, methodsInGroup) => new TypedParameterMethod(n, methodsInGroup.Any(m => m.ReturnsVoid), methodsInGroup.Any(m => !m.ReturnsVoid))
                    )
                    .Distinct();
        }
    }
}
