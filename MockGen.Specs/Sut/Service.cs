namespace MockGen.Specs.Sut
{
    public class Service
    {
        private readonly IDependency dependency;

        public Service(IDependency dependency)
        {
            this.dependency = dependency;
        }

        public void ExecuteSomeAction()
        {
            dependency.DoSomething();
        }

        public void ExecuteSomeActionWithParam(int input)
        {
            dependency.DoSomethingWithParameter(input);
        }

        public void ExecuteSomeActionWithReferenceTypeParameter(Model1 model)
        {
            dependency.DoSomethingWithReferenceTypeParameter(model);
        }

        public void ExecuteSomeActionWithTwoParameters(Model1 model1, Model2 model2)
        {
            dependency.DoSomethingWithTwoParameters(model1, model2);
        }

        public int ReturnDependencyNumber()
        {
            return dependency.GetSomeNumber();
        }

        public int ReturnDependencyNumberWithParam(int input)
        {
            return dependency.GetSomeNumberWithParameter(input);
        }
    }
}
