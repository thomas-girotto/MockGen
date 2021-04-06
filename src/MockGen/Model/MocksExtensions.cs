using System.Collections.Generic;
using System.Linq;

namespace MockGen.Model
{
    public static class MocksExtensions
    {
        public static IEnumerable<MethodsInfo> GetAllMethodsGroupedByTypeParameter(this IEnumerable<Mock> mocks)
        {
            var initTypedMethodsFromProperties = new List<MethodsInfo>();

            var properties = mocks.SelectMany(m => m.Properties);
            if (properties.Any())
            {
                // set property is configured via MethodSetupVoid<T>
                initTypedMethodsFromProperties.Add(new MethodsInfo(1, true, false, false, false));
                // get property is configured via MethodSetupReturn<T> (here T is the return type and not a parameter type)
                if (properties.Any(p => p.Type.TaskInfo == TaskInfo.Task))
                {
                    initTypedMethodsFromProperties.Add(new MethodsInfo(0, false, true, true, false));
                }
                if (properties.Any(p => p.Type.TaskInfo == TaskInfo.ValueTask))
                {
                    initTypedMethodsFromProperties.Add(new MethodsInfo(0, false, true, false, true));
                }
                if (properties.Any(p => p.Type.TaskInfo == TaskInfo.NotATask))
                {
                    initTypedMethodsFromProperties.Add(new MethodsInfo(0, false, true, false, false));
                }
            }

            return mocks
                .SelectMany(mock => mock.Methods)
                .Select(m => new MethodsInfo(
                    m.Parameters.Count, 
                    m.ReturnsVoid, 
                    !m.ReturnsVoid && m.ReturnType.TaskInfo == TaskInfo.NotATask,
                    !m.ReturnsVoid && m.ReturnType.TaskInfo == TaskInfo.Task,
                    !m.ReturnsVoid && m.ReturnType.TaskInfo == TaskInfo.ValueTask))
                .Union(initTypedMethodsFromProperties)
                .GroupBy(
                    m => m.NumberOfParameters,
                    (n, methodsInGroup) => new MethodsInfo(
                        n, 
                        methodsInGroup.Any(m => m.HasMethodThatReturnsVoid), 
                        methodsInGroup.Any(m => m.HasMethodThatReturns),
                        methodsInGroup.Any(m => m.HasMethodThatReturnsTask),
                        methodsInGroup.Any(m => m.HasMethodThatReturnsValueTask))
                )
                .Distinct();
        }
    }
}
