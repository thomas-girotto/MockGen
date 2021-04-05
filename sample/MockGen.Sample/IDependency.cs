using System;
using System.Threading.Tasks;

namespace MockGen.Sample
{
    public interface IDependency
    {
        void DoSomething();
        void DoSomethingWithParam(SomeModel value);
        int ReturnSomeInt();
        int ReturnSomeIntWithParam(SomeModel input);
        Task<SomeModel> GetSomeModelAsync(int id);
    }
}
