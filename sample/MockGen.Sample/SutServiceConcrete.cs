using System;
using System.Collections.Generic;
using System.Text;

namespace MockGen.Sample
{
    public class SutServiceConcrete
    {
        private readonly ConcreteDependency dependency;

        public SutServiceConcrete(ConcreteDependency dependency)
        {
            this.dependency = dependency;
        }

        public void DoSomethingAndSave(SomeModel model)
        {
            dependency.SaveModel(model);
        }

        public void DoSomethingAndCallProtectedMethod(SomeModel model)
        {
            dependency.CallProtectedMethod(model);
        }
    }
}
