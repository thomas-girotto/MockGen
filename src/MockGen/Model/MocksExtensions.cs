using System.Collections.Generic;
using System.Linq;

namespace MockGen.Model
{
    public static class MocksExtensions
    {
        public static IEnumerable<TypedParameterMethod> GetAllMethodsGroupedByTypeParameter(this IEnumerable<Mock> mocks)
        {
            var initTypedMethodsFromProperties = new List<TypedParameterMethod>();

            var properties = mocks.SelectMany(m => m.Properties);
            if (properties.Any())
            {
                // set property is configured via MethodSetupVoid<T>
                initTypedMethodsFromProperties.Add(new TypedParameterMethod(1, true, false, false));
                // get property is configured via MethodSetupReturn<T> (here T is the return type and not a parameter type)
                if (properties.Any(p => p.Type.IsTask))
                {
                    initTypedMethodsFromProperties.Add(new TypedParameterMethod(0, false, true, true));
                }
                if (properties.Any(p => !p.Type.IsTask))
                {
                    initTypedMethodsFromProperties.Add(new TypedParameterMethod(0, false, true, false));
                }
            }

            return mocks
                .SelectMany(mock => mock.Methods)
                .Select(m => new TypedParameterMethod(
                    m.Parameters.Count, 
                    m.ReturnsVoid, 
                    !m.ReturnsVoid && !m.ReturnType.IsTask, 
                    !m.ReturnsVoid && m.ReturnType.IsTask))
                .Union(initTypedMethodsFromProperties)
                .GroupBy(
                    m => m.NumberOfTypedParameters,
                    (n, methodsInGroup) => new TypedParameterMethod(
                        n, 
                        methodsInGroup.Any(m => m.HasMethodThatReturnsVoid), 
                        methodsInGroup.Any(m => m.HasMethodThatReturns),
                        methodsInGroup.Any(m => m.HasMethodThatReturnsTask))
                )
                .Distinct();
        }
    }
}
