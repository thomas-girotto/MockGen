using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MockGen.Model
{
    public class MockDescriptorSanitizer
    {
        public static IEnumerable<MockDescriptor> Sanitize(IEnumerable<MockDescriptor> mocks)
        {
            var mocksPerType = new Dictionary<string, MockDescriptor>();
            foreach(var mock in mocks)
            {
                if (mocksPerType.ContainsKey(mock.TypeToMock))
                {
                    // If types are the exactly the same, don't do anything
                    if (mocksPerType[mock.TypeToMock].TypeToMockNamespace == mock.TypeToMockNamespace)
                    {
                        continue;
                    }
                    var (type1, type2) = AddPartsOfNamespaceToTypeUntilTheyAreDifferent(mock.TypeToMock, mocksPerType[mock.TypeToMock].TypeToMockNamespace, mock.TypeToMockNamespace);
                    var alreadyRegisteredMock = mocksPerType[mock.TypeToMock];
                    alreadyRegisteredMock.TypeToMock = type1;
                    mocksPerType.Remove(mock.TypeToMock);
                    mocksPerType.Add(type1, alreadyRegisteredMock);
                    mock.TypeToMock = type2;
                    mocksPerType.Add(type2, mock);
                }
                else
                {
                    mocksPerType.Add(mock.TypeToMock, mock);
                }
            }

            return mocksPerType.Values;
        }

        private static (string type1, string type2) AddPartsOfNamespaceToTypeUntilTheyAreDifferent(string type, string namespace1, string namespace2)
        {
            var type1 = type;
            var type2 = type;

            var ns1Decomposition = namespace1.Split('.').Reverse().ToArray();
            var ns2Decomposition = namespace2.Split('.').Reverse().ToArray();
            
            for (int i = 0; i < Math.Min(ns1Decomposition.Length, ns2Decomposition.Length); i++)
            {
                type1 += ns1Decomposition[i];
                type2 += ns2Decomposition[i];

                if (ns1Decomposition[i] != ns2Decomposition[i])
                {
                    break;
                }
            }
        
            if (type1 == type2)
            {
                if (ns1Decomposition.Length > ns2Decomposition.Length)
                {
                    type1 += ns1Decomposition[ns2Decomposition.Length];
                }
                else
                {
                    type2 += ns2Decomposition[ns1Decomposition.Length];
                }
            }
            return (type1, type2);
        }
    }
}
