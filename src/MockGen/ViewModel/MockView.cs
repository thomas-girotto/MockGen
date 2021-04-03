using MockGen.Model;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.ViewModel
{
    public class MockView
    {
        public MockView(Mock mock)
        {
            Mock = mock;
        }

        public IEnumerable<string> Namespaces => Mock.TypeToMock.Namespaces
            .Union(Mock.Methods.SelectMany(m => m.Parameters.SelectMany(p => p.Type.Namespaces)))
            .Union(Mock.Methods.SelectMany(m => m.ReturnType.Namespaces))
            .Union(Mock.Ctors.SelectMany(c => c.Parameters.SelectMany(p => p.Type.Namespaces)))
            .Union(Mock.Properties.SelectMany(p => p.Type.Namespaces))
            .Where(ns => !string.IsNullOrEmpty(ns));

        public IEnumerable<Method> MethodsWithOutParameters => Mock.Methods.Where(m => m.OutParameters.Any());

        public string CallBaseCtorIfNeeded => Mock.IsInterface ? "" : " : base({0})";

        public Mock Mock { get; private set; }
    }
}
