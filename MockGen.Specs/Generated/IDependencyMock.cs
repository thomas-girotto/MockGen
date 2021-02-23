using MockGen.Specs.Generated.Helpers;
using MockGen.Specs.Sut;

namespace MockGen.Specs.Generated.IDependencyNs
{
    internal class IDependencyMock : IDependency
    {
        private readonly MethodSetupVoid doSomethingSetup;
        private readonly MethodSetupVoid<int> doSomethingWithParameterSetup;
        private readonly MethodSetupVoid<Model1> doSomethingWithReferenceTypeParameterSetup;
        private readonly MethodSetupVoid<Model1, Model2> doSomethingWithTwoParametersSetup;
        private readonly MethodSetupReturn<int> getSomeNumberSetup;
        private readonly MethodSetupReturn<int, int> getSomeNumberWithParameterSetup;

        public IDependencyMock(
            MethodSetupVoid doSomethingSetup, 
            MethodSetupVoid<int> doSomethingWithParameterSetup, 
            MethodSetupVoid<Model1> doSomethingWithReferenceTypeParameterSetup,
            MethodSetupVoid<Model1, Model2> doSomethingWithTwoParametersSetup, 
            MethodSetupReturn<int> getSomeNumberSetup, 
            MethodSetupReturn<int, int> getSomeNumberWithParameterSetup)
        {
            this.doSomethingSetup = doSomethingSetup;
            this.doSomethingWithParameterSetup = doSomethingWithParameterSetup;
            this.doSomethingWithReferenceTypeParameterSetup = doSomethingWithReferenceTypeParameterSetup;
            this.doSomethingWithTwoParametersSetup = doSomethingWithTwoParametersSetup;
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

        public void DoSomethingWithReferenceTypeParameter(Model1 model)
        {
            doSomethingWithReferenceTypeParameterSetup.ExecuteSetup(model);
        }

        public void DoSomethingWithTwoParameters(Model1 model1, Model2 model2)
        {
            doSomethingWithTwoParametersSetup.ExecuteSetup(model1, model2);
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
