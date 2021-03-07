using System;
using System.Collections.Generic;
using System.Text;
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
