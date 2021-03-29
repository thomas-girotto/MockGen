using MockGen.Specs.Sut;
using System.Threading.Tasks;

namespace MockGen
{
    internal class IDependencyMock : IDependency
    {
        private readonly IDependencyMethodsSetup methods;
        
        public IDependencyMock(IDependencyMethodsSetup methods)
        {
            this.methods = methods;
        }

        public Model1 GetOnlyProperty => methods.GetOnlyPropertySetup.ExecuteGetSetup();

        public Model1 SetOnlyProperty
        {
            set => methods.SetOnlyPropertySetup.ExecuteSetSetup(value);
        }

        public Model1 GetSetProperty
        {
            get => methods.GetSetPropertySetup.ExecuteGetSetup();
            set => methods.GetSetPropertySetup.ExecuteSetSetup(value);
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

        public Task<int> GetSomeNumberAsync()
        {
            return methods.GetSomeNumberAsyncSetup.ExecuteSetup();
        }

        public bool TryGetById(int id, out Model1 model)
        {
            model = methods.TryGetByIdSetupOutParameters(id);
            return methods.TryGetByIdSetup.ExecuteSetup(id);
        }

        public bool TryGetById(int id, out Model1 model, out Model2 model2)
        {
            (model, model2) = methods.TryGetByIdSetupOutParameters2(id);
            return methods.TryGetByIdSetup2.ExecuteSetup(id);
        }
    }
}
