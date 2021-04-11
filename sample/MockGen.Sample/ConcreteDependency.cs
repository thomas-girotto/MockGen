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

        /// <summary>
        /// As this method is not virtual, it will not be mocked
        /// </summary>
        /// <param name="model"></param>
        public void CallProtectedMethodFromNonMockedMethod(SomeModel model)
        {
            DoSomethingProtected(model);
        }

        /// <summary>
        /// As this method is virtual, it will be mocked so if you still want to call DoSomethingProcted
        /// in a test, you should conifgure the mock with <see cref="Mock.ConcreteDependency(callBase: true)"/>
        /// </summary>
        /// <param name="model"></param>
        public virtual void CallProtectedMethodFromMockedMethod(SomeModel model)
        {
            DoSomethingProtected(model);
        }

        protected virtual void DoSomethingProtected(SomeModel model)
        {

        }
    }
}
