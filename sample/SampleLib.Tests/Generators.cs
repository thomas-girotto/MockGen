using System;
using MockGen;

namespace SampleLib.Tests
{
    public class Generators
    {
        public void GenerateMocks()
        {
            MockGenerator.Generate<IExternalDependency>();
        }
    }
}
