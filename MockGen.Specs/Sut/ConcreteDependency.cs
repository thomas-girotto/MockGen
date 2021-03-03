using System;
using System.Collections.Generic;
using System.Text;

namespace MockGen.Specs.Sut
{
    /// <summary>
    /// Demonstrate mocking a real class
    /// </summary>
    public class ConcreteDependency
    {
        public int ICannotBeMocked()
        {
            return 1;
        }

        public virtual int ICanBeMocked()
        {
            return 1;
        }
    }
}
