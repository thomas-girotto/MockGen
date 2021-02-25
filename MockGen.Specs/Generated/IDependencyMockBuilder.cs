using MockGen.Specs.Generated.Setup;
using MockGen.Specs.Sut;

namespace MockGen.Specs.Generated.IDependencyNs
{
    internal class IDependencyMockBuilder
    {
        private readonly MethodSetupVoid doSomethingSetup = new MethodSetupVoid();
        private readonly MethodSetupVoid<int> doSomethingWithParameterSetup = new MethodSetupVoid<int>();
        private readonly MethodSetupVoid<Model1> doSomethingWithReferenceTypeParameterSetup = new MethodSetupVoid<Model1>();
        private readonly MethodSetupVoid<Model1, Model2> doSomethingWithTwoParametersSetup = new MethodSetupVoid<Model1, Model2>();
        private readonly MethodSetupReturn<int> getSomeNumberSetup = new MethodSetupReturn<int>();
        private readonly MethodSetupReturn<int, int> getSomeNumberWithParameterSetup = new MethodSetupReturn<int, int>();
        private readonly MethodSetupReturn<Model1, int> getSomeNumberWithReferenceTypeParameterSetup = new MethodSetupReturn<Model1, int>();
        private readonly MethodSetupReturn<Model1, Model2, int> getSomeNumberWithTwoParametersSetup = new MethodSetupReturn<Model1, Model2, int>();

        public IMethodSetupVoid DoSomething()
        {
            return doSomethingSetup;
        }

        public IMethodSetupVoid DoSomethingWithParameter(Arg<int> input)
        {
            return doSomethingWithParameterSetup.ForParameter(input);
        }

        public IMethodSetupVoid DoSomethingWithReferenceTypeParameter(Arg<Model1>? model)
        {
            return doSomethingWithReferenceTypeParameterSetup.ForParameter(model ?? Arg<Model1>.Null);
        }

        public IMethodSetupVoid DoSomethingWithTwoParameters(Arg<Model1>? model1, Arg<Model2>? model2)
        {
            return doSomethingWithTwoParametersSetup.ForParameter(model1 ?? Arg<Model1>.Null, model2 ?? Arg<Model2>.Null);
        }

        public IMethodSetupReturn<int> GetSomeNumber()
        {
            return getSomeNumberSetup;
        }

        public IMethodSetupReturn<int> GetSomeNumberWithParameter(Arg<int> input)
        {
            return getSomeNumberWithParameterSetup.ForParameter(input);
        }

        public IMethodSetupReturn<int> GetSomeNumberWithReferenceTypeParameter(Arg<Model1> model1)
        {
            return getSomeNumberWithReferenceTypeParameterSetup.ForParameter(model1 ?? Arg<Model1>.Null);
        }

        public IMethodSetupReturn<int> GetSomeNumberWithTwoParameters(Arg<Model1> model1, Arg<Model2> model2)
        {
            return getSomeNumberWithTwoParametersSetup.ForParameter(model1 ?? Arg<Model1>.Null, model2 ?? Arg<Model2>.Null);
        }

        public IDependency Build()
        {
            return new IDependencyMock(doSomethingSetup, 
                doSomethingWithParameterSetup, 
                doSomethingWithReferenceTypeParameterSetup,
                doSomethingWithTwoParametersSetup, 
                getSomeNumberSetup, 
                getSomeNumberWithParameterSetup,
                getSomeNumberWithReferenceTypeParameterSetup,
                getSomeNumberWithTwoParametersSetup);
        }
    }
}
