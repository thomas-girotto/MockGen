using MockGen.Specs.Generated.Helpers;
using MockGen.Specs.Sut;

namespace MockGen.Specs.Generated.IDependencyNs
{
    internal class IDependencyMock : IDependency
    {
        private readonly MethodSetupVoid doSomethingSetup;
        private readonly MethodSetupVoid<int> doSomethingWithParameterSetup;
        private readonly MethodSetupVoid<Model> doSomethingWithReferenceTypeParameterSetup;
        private readonly MethodSetupReturn<int> getSomeNumberSetup;
        private readonly MethodSetupReturn<int, int> getSomeNumberWithParameterSetup;

        public IDependencyMock(MethodSetupVoid doSomethingSetup, MethodSetupVoid<int> doSomethingWithParameterSetup, MethodSetupVoid<Model> doSomethingWithReferenceTypeParameterSetup, MethodSetupReturn<int> getSomeNumberSetup, MethodSetupReturn<int, int> getSomeNumberWithParameterSetup)
        {
            this.doSomethingSetup = doSomethingSetup;
            this.doSomethingWithParameterSetup = doSomethingWithParameterSetup;
            this.doSomethingWithReferenceTypeParameterSetup = doSomethingWithReferenceTypeParameterSetup;
            this.getSomeNumberSetup = getSomeNumberSetup;
            this.getSomeNumberWithParameterSetup = getSomeNumberWithParameterSetup;
        }

        public void DoSomething()
        {
            doSomethingSetup.ExecuteSetup();
        }

        public void DoSomethingWithParameter(int input)
        {
            doSomethingWithParameterSetup.ExecuteSetup(input);
        }

        public void DoSomethingWithReferenceTypeParameter(Model model)
        {
            doSomethingWithReferenceTypeParameterSetup.ExecuteSetup(model);
        }

        public int GetSomeNumber()
        {
            return getSomeNumberSetup.ExecuteSetup();
        }

        public int GetSomeNumberWithParameter(int input)
        {
            return getSomeNumberWithParameterSetup.ExecuteSetup(input);
        }
    }
}
