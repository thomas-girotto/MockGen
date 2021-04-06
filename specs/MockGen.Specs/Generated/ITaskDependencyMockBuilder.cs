using MockGen.Setup;
using MockGen.Specs.Sut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockGen
{
    internal class ITaskDependencyMockBuilder
    {
        private readonly ITaskDependencyMethodsSetup methods = new ITaskDependencyMethodsSetup();
        private readonly Func<ITaskDependencyMock> ctor;

        public ITaskDependencyMockBuilder()
        {
            ctor = () => new ITaskDependencyMock(methods);
        }

        public IMethodSetupReturn<int> GetSomeNumberTaskAsync()
        {
            return methods.GetSomeNumberTaskAsyncSetup;
        }

        public IMethodSetupReturn<int> GetSomeNumberValueTaskAsync()
        {
            return methods.GetSomeNumberValueTaskAsyncSetup;
        }

        public ITaskDependency Build()
        {
            methods.SetupDone();
            return ctor();
        }
    }
}
