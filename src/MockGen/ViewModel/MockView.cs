using MockGen.Model;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.ViewModel
{
    public class MockView
    {
        public Mock Mock { get; private set; }
        public List<MethodView> Methods { get; private set; }
        
        public List<ParametersView> CtorsParameters { get; private set; }

        public MockView(Mock mock)
        {
            Mock = mock;
            Methods = mock.Methods.Select(m => new MethodView(m)).ToList();
            CtorsParameters = mock.Ctors.Select(c => new ParametersView(c.Parameters)).ToList();
        }

        public IEnumerable<string> Namespaces => Mock.TypeToMock.Namespaces
            .Union(Mock.Methods.SelectMany(m => m.Parameters.SelectMany(p => p.Type.Namespaces)))
            .Union(Mock.Methods.SelectMany(m => m.ReturnType.Namespaces))
            .Union(Mock.Ctors.SelectMany(c => c.Parameters.SelectMany(p => p.Type.Namespaces)))
            .Union(Mock.Properties.SelectMany(p => p.Type.Namespaces))
            .Where(ns => !string.IsNullOrEmpty(ns));

        public IEnumerable<MethodView> MethodsWithOutParameters => Methods.Where(m => m.Parameters.HasOutParameter);

        public string CallBaseCtorIfNeeded => Mock.IsInterface ? "" : " : base({0})";
    }
}
