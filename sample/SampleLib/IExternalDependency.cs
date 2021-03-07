using System;
using System.Collections.Generic;
using System.Text;

namespace SampleLib
{
    public interface IExternalDependency
    {
        int GetSomeNumber();
        void DoSomething(Model model);
    }
}
