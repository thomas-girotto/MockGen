using MockGen.Specs.Sut;

namespace MockGen
{
    internal class ConcreteDependencyMock : ConcreteDependency
    {
        private readonly bool callBase;
        private readonly ConcreteDependencyMethodsSetup methods;

        public ConcreteDependencyMock(bool callBase, ConcreteDependencyMethodsSetup methods)
        {
            this.callBase = callBase;
            this.methods = methods;
        }

        public ConcreteDependencyMock(bool callBase, ConcreteDependencyMethodsSetup methods, Model1 model1) : base(model1) 
        {
            this.callBase = callBase;
            this.methods = methods;
        }

        public ConcreteDependencyMock(bool callBase, ConcreteDependencyMethodsSetup methods, Model1 model1, Model2 model2) : base(model1, model2)
        {
            this.callBase = callBase;
            this.methods = methods;
        }

        public override int ICanBeMocked()
        {
            if (callBase)
            {
                methods.ICanBeMockedSetup.ExecuteSetup();
                return base.ICanBeMocked();
            }
            return methods.ICanBeMockedSetup.ExecuteSetup();
        }

        public override void ICallProtectedMethod(string firstName)
        {
            methods.ICallProtectedMethodSetup.ExecuteSetup(firstName);
            if (callBase)
            {
                base.ICallProtectedMethod(firstName);
            }
        }

        protected override void SaveFullName(string fullName)
        {
            methods.SaveFullName.ExecuteSetup(fullName);
        }
    }
}
