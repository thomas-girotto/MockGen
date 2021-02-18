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
