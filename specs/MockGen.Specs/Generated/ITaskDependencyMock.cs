using MockGen.Specs.Sut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockGen
{
    internal class ITaskDependencyMock : ITaskDependency
    {
        private readonly ITaskDependencyMethodsSetup methods;

        public ITaskDependencyMock(ITaskDependencyMethodsSetup methods)
        {
            this.methods = methods;
        }

        public Task<int> GetSomeNumberTaskAsync()
        {
            return methods.GetSomeNumberTaskAsyncSetup.ExecuteSetup();
        }

        public ValueTask<int> GetSomeNumberValueTaskAsync()
        {
            return methods.GetSomeNumberValueTaskAsyncSetup.ExecuteSetup();
        }

        public Task<int> GetSomeNumberWithParamTaskAsync(Model1 model)
        {
            throw new NotImplementedException();
        }

        public ValueTask<int> GetSomeNumberWithParamValueTaskAsync(Model1 model)
        {
            throw new NotImplementedException();
        }
    }
}
