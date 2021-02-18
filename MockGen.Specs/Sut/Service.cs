﻿namespace MockGen.Specs.Sut
{
    public class Service
    {
        private readonly IDependency dependency;

        public Service(IDependency dependency)
        {
            this.dependency = dependency;
        }

        public int ReturnDependencyNumber()
        {
            return dependency.GetSomeNumber();
        }
    }
}
