using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MockGen.Sample
{
    public class SutServiceAsync
    {
        private readonly ITaskDependency dependency;

        public SutServiceAsync(ITaskDependency dependency)
        {
            this.dependency = dependency;
        }

        public async Task<SomeModel> GetSomeModelTaskAsync(int id)
        {
            return await dependency.GetSomeModelTaskAsync(id);
        }

        public async ValueTask<SomeModel> GetSomeNumberValueTaskAsync(int id)
        {
            return await dependency.GetSomeModelValueTaskAsync(id);
        }
    }
}
