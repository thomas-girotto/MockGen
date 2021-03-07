using MockGen.Setup;
using MockGen.Specs.Sut;

namespace MockGen
{
    internal class ConcreteDependencyMock : ConcreteDependency
    {
        private readonly ConcreteDependencyMethodsSetup methods;

        public ConcreteDependencyMock(ConcreteDependencyMethodsSetup methods)
        {
            this.methods = methods;
        }

        public ConcreteDependencyMock(ConcreteDependencyMethodsSetup methods, Model1 model1) : base(model1) 
        {
            this.methods = methods;
        }

        public ConcreteDependencyMock(ConcreteDependencyMethodsSetup methods, Model1 model1, Model2 model2) : base(model1, model2)
        {
            this.methods = methods;
        }

        public override int ICanBeMocked()
        {
            return methods.ICanBeMockedSetup.ExecuteSetup();
        }
    }
}
