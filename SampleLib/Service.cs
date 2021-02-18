using System;

namespace SampleLib
{
    public class Service
    {
        private readonly IExternalDependency dependency;

        public Service(IExternalDependency dependency)
        {
            this.dependency = dependency;
        }

        public int ReturnDependencyNumber()
        {
            return dependency.GetSomeNumber();
        }
    }
}
