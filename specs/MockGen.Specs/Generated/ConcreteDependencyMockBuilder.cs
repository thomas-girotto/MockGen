using MockGen.Setup;
using MockGen.Specs.Sut;
using System;

namespace MockGen
{
    internal class ConcreteDependencyMockBuilder
    {
        private readonly ConcreteDependencyMethodsSetup methods = new ConcreteDependencyMethodsSetup();
        private readonly Func<ConcreteDependencyMock> ctor;

        public ConcreteDependencyMockBuilder(bool callBase)
        {
            ctor = () => new ConcreteDependencyMock(callBase, methods);
        }

        public ConcreteDependencyMockBuilder(bool callBase, Model1 model1)
        {
            ctor = () => new ConcreteDependencyMock(callBase, methods, model1); 
        }

        public ConcreteDependencyMockBuilder(bool callBase, Model1 model1, Model2 model2)
        {
            ctor = () => new ConcreteDependencyMock(callBase, methods, model1, model2);
        }

        internal IMethodSetupReturn<int> ICanBeMocked()
        {
            return methods.ICanBeMockedSetup;
        }

        internal IMethodSetup<string> ICallProtectedMethod()
        {
            return methods.ICallProtectedMethodSetup;
        }

        internal IMethodSetup<string> SaveFullName(Arg<string>? firstname)
        {
            return methods.SaveFullName.ForParameter(firstname?? Arg<string>.Null);
        }

        internal ConcreteDependencyMock Build()
        {
            methods.SetupDone();
            return ctor();
        }
    }
}
