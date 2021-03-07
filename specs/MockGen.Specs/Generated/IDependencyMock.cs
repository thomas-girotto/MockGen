using MockGen.Specs.Sut;

namespace MockGen
{
    internal class IDependencyMock : IDependency
    {
        private readonly IDependencyMethodsSetup methods;
        
        public IDependencyMock(IDependencyMethodsSetup methods)
        {
            this.methods = methods;
        }

        public void DoSomething()
        {
            methods.DoSomethingSetup.ExecuteSetup();
        }

        public void DoSomethingWithParameter(int input)
        {
            methods.DoSomethingWithParameterSetup.ExecuteSetup(input);
        }

        public void DoSomethingWithReferenceTypeParameter(Model1 model)
        {
            methods.DoSomethingWithReferenceTypeParameterSetup.ExecuteSetup(model);
        }

        public void DoSomethingWithTwoParameters(Model1 model1, Model2 model2)
        {
            methods.DoSomethingWithTwoParametersSetup.ExecuteSetup(model1, model2);
        }

        public int GetSomeNumber()
        {
            return methods.GetSomeNumberSetup.ExecuteSetup();
        }

        public int GetSomeNumberWithParameter(int input)
        {
            return methods.GetSomeNumberWithParameterSetup.ExecuteSetup(input);
        }

        public int GetSomeNumberWithReferenceTypeParameter(Model1 model1)
        {
            return methods.GetSomeNumberWithReferenceTypeParameterSetup.ExecuteSetup(model1);
        }

        public int GetSomeNumberWithTwoParameters(Model1 model1, Model2 model2)
        {
            return methods.GetSomeNumberWithTwoParametersSetup.ExecuteSetup(model1, model2);
        }
    }
}
