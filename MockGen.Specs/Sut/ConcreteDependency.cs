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
        private readonly Model1 m1;
        private readonly Model2 m2;

        public ConcreteDependency()
        {

        }

        public ConcreteDependency(Model1 m1)
        {
            this.m1 = m1;
        }

        public ConcreteDependency(Model1 m1, Model2 m2)
        {
            this.m1 = m1;
            this.m2 = m2;
        }

        public Model1 M1 => m1;
        public Model2 M2 => m2;

        public int ICannotBeMocked()
        {
            return 1;
        }

        public virtual int ICanBeMocked()
        {
            return m1?.Id ?? 0;
        }
    }
}
