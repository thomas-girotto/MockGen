using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockGen.Sample.Tests
{
    public class Generator
    {
        public void Generate()
        {
            MockGenerator.Generate<IDependency>();
            MockGenerator.Generate<IDependencyOutParams>();
            MockGenerator.Generate<ITaskDependency>();
        }
    }
}
