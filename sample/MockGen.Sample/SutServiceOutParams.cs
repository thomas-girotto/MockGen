namespace MockGen.Sample
{
    public class SutServiceOutParams
    {
        private readonly IDependencyOutParams dependency;

        public SutServiceOutParams(IDependencyOutParams dependency)
        {
            this.dependency = dependency;
        }

        public bool TryGetById(int id, out SomeModel model)
        {
            return dependency.TryGetById(id, out model);
        }

        public bool TryGetByIdWithSeveralOutParameters(int id, out SomeModel outParam1, out SomeModel outParam2)
        {
            return dependency.TryGetByIdWithSeveralOutParameters(id, out outParam1, out outParam2);
        }
    }
}
