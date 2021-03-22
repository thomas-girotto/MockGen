﻿using System;

namespace MockGen.Sample
{
    public class SutService
    {
        private readonly IDependency dependency;

        public SutService(IDependency dependency)
        {
            this.dependency = dependency;
        }

        public void DoSomething()
        {
            dependency.DoSomething();
        }

        public void DoSomething(SomeModel model)
        {
            dependency.DoSomethingWithParam(model);
        }

        public int ReturnSomeInt()
        {
            return dependency.ReturnSomeInt();
        }

        public int ReturnSomeInt(SomeModel model)
        {
            return dependency.ReturnSomeIntWithParam(model);
        }
    }
}
