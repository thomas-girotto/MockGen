﻿using MockGen.Specs.Generated.Helpers;
using MockGen.Specs.Sut;

namespace MockGen.Specs.Generated.IDependencyNs
{
    internal class IDependencyMockBuilder
    {
        private readonly MethodSetupVoid doSomethingSetup = new MethodSetupVoid();
        private readonly MethodSetupVoid<int> doSomethingWithParameterSetup = new MethodSetupVoid<int>();
        private readonly MethodSetupVoid<Model> doSomethingWithReferenceTypeParameterSetup = new MethodSetupVoid<Model>();
        private readonly MethodSetupReturn<int> getSomeNumberSetup = new MethodSetupReturn<int>();
        private readonly MethodSetupReturn<int, int> getSomeNumberWithParameterSetup = new MethodSetupReturn<int, int>();

        public IMethodSetupVoid DoSomething()
        {
            return doSomethingSetup;
        }

        public IMethodSetupVoid DoSomethingWithParameter(Arg<int> input)
        {
            return doSomethingWithParameterSetup.ForParameter(input);
        }

        public IMethodSetupVoid DoSomethingWithReferenceTypeParameter(Arg<Model>? model)
        {
            return doSomethingWithReferenceTypeParameterSetup.ForParameter(model ?? Arg<Model>.Null);
        }

        public IMethodSetupReturn<int> GetSomeNumber()
        {
            return getSomeNumberSetup;
        }

        public IMethodSetupReturn<int> GetSomeNumberWithParameter(Arg<int> input)
        {
            return getSomeNumberWithParameterSetup.ForParameter(input);
        }

        public IDependency Build()
        {
            return new IDependencyMock(doSomethingSetup, doSomethingWithParameterSetup, doSomethingWithReferenceTypeParameterSetup, getSomeNumberSetup, getSomeNumberWithParameterSetup);
        }
    }
}
