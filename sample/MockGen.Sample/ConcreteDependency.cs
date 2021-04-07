using System;
using System.Collections.Generic;
using System.Text;

namespace MockGen.Sample
{
    public class ConcreteDependency
    {
        public string ConstructorParam { get; private set; }

        public ConcreteDependency(string someValue)
        {
            ConstructorParam = someValue;
        }

        public virtual void SaveModel(SomeModel model)
        {
            // Let's say we save the model into a database
        }

        public void CallProtectedMethod(SomeModel model)
        {
            DoSomethingProtected(model);
        }

        protected virtual void DoSomethingProtected(SomeModel model)
        {

        }
    }
}
