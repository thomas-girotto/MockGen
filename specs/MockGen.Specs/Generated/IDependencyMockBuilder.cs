using System;
using MockGen.Setup;
using MockGen.Specs.Sut;

namespace MockGen
{
    internal class IDependencyMockBuilder
    {
        private readonly IDependencyMethodsSetup methods = new IDependencyMethodsSetup();
        private readonly Func<IDependencyMock> ctor;

        public IDependencyMockBuilder()
        {
            ctor = () => new IDependencyMock(methods);
        }

        public IPropertyGet<Model1> GetOnlyProperty => methods.GetOnlyPropertySetup;
        
        public IPropertySet<Model1> SetOnlyProperty => methods.SetOnlyPropertySetup;

        public IPropertyGetSet<Model1> GetSetProperty => methods.GetSetPropertySetup;
        
        public IMethodSetup DoSomething()
        {
            return methods.DoSomethingSetup;
        }

        public IMethodSetup<int> DoSomethingWithParameter(Arg<int> input)
        {
            return methods.DoSomethingWithParameterSetup.ForParameter(input);
        }

        public IMethodSetup<Model1> DoSomethingWithReferenceTypeParameter(Arg<Model1>? model)
        {
            return methods.DoSomethingWithReferenceTypeParameterSetup.ForParameter(model ?? Arg<Model1>.Null);
        }

        public IMethodSetup<Model1, Model2> DoSomethingWithTwoParameters(Arg<Model1>? model1, Arg<Model2>? model2)
        {
            return methods.DoSomethingWithTwoParametersSetup.ForParameter(model1 ?? Arg<Model1>.Null, model2 ?? Arg<Model2>.Null);
        }

        public IMethodSetupReturn<int> GetSomeNumber()
        {
            return methods.GetSomeNumberSetup;
        }

        public IMethodSetupReturn<int, int> GetSomeNumberWithParameter(Arg<int> input)
        {
            return methods.GetSomeNumberWithParameterSetup.ForParameter(input);
        }

        public IMethodSetupReturn<Model1, int> GetSomeNumberWithReferenceTypeParameter(Arg<Model1> model1)
        {
            return methods.GetSomeNumberWithReferenceTypeParameterSetup.ForParameter(model1 ?? Arg<Model1>.Null);
        }

        public IMethodSetupReturn<Model1, Model2, int> GetSomeNumberWithTwoParameters(Arg<Model1> model1, Arg<Model2> model2)
        {
            return methods.GetSomeNumberWithTwoParametersSetup.ForParameter(model1 ?? Arg<Model1>.Null, model2 ?? Arg<Model2>.Null);
        }

        public IMethodSetupReturn<int> GetSomeNumberAsync()
        {
            return methods.GetSomeNumberAsyncSetup;
        }


        public IMethodSetupReturn<int, bool> TryGetById(Arg<int> id)
        {
            return TryGetById(id, (_) => default(Model1));
        }

        public IMethodSetupReturn<int, bool> TryGetById(Arg<int> id, Func<Model1> setupOutParameter)
        {
            return TryGetById(id, (_) => setupOutParameter());
        }

        public IMethodSetupReturn<int, bool> TryGetById(Arg<int> id, Func<int, Model1> setupOutParameter)
        {
            methods.TryGetByIdSetupOutParameters = setupOutParameter;
            return methods.TryGetByIdSetup.ForParameter(id);
        }

        public IMethodSetupReturn<int, bool> TryGetById(Arg<int> id, Func<(Model1 model1, Model2 model2)> setupOutParameters)
        {
            return TryGetById(id, (_) => setupOutParameters());
        }

        public IMethodSetupReturn<int, bool> TryGetById(Arg<int> id, Func<int, (Model1 model1, Model2 model2)> setupOutParameters)
        {
            methods.TryGetByIdSetupOutParameters2 = setupOutParameters;
            return methods.TryGetByIdSetup2.ForParameter(id);
        }

        public IDependency Build()
        {
            methods.SetupDone();
            return ctor();
        }
    }
}
