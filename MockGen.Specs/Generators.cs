using MockGen.Specs.Sut;

namespace MockGen.Specs
{
    public class Generators
    {
        public void GenerateMocks()
        {
            MockGenerator.Generate<IDependency>();
        }
    }
}
