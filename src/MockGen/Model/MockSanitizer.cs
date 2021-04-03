using System;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Model
{
    public class MockSanitizer
    {
        public static IEnumerable<Mock> Sanitize(IEnumerable<Mock> mocks)
        {
            var mocksPerType = new Dictionary<string, Mock>();
            foreach(var mock in mocks)
            {
                if (mocksPerType.ContainsKey(mock.TypeToMock.Name))
                {
                    // If types are the exactly the same, don't do anything
                    if (mocksPerType[mock.TypeToMock.Name].TypeToMock.FullName == mock.TypeToMock.FullName)
                    {
                        continue;
                    }
                    var (type1, type2) = AddPartsOfNamespaceToTypeUntilTheyAreDifferent(mock.TypeToMock.Name, mocksPerType[mock.TypeToMock.Name].TypeToMock.FullName, mock.TypeToMock.FullName);
                    var alreadyRegisteredMock = mocksPerType[mock.TypeToMock.Name];
                    alreadyRegisteredMock.TypeToMock.Name = type1;
                    mocksPerType.Remove(mock.TypeToMock.Name);
                    mocksPerType.Add(type1, alreadyRegisteredMock);
                    mock.TypeToMock.Name = type2;
                    mocksPerType.Add(type2, mock);
                }
                else
                {
                    mocksPerType.Add(mock.TypeToMock.Name, mock);
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
            
            // starts at index 1 because we don't want to include the type itself (which is the last part of the FullName,
            // so the first after the Reverse)
            for (int i = 1; i < Math.Min(ns1Decomposition.Length, ns2Decomposition.Length); i++)
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
