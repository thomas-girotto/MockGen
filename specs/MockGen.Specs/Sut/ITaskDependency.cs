using System.Threading.Tasks;

namespace MockGen.Specs.Sut
{
    public interface ITaskDependency
    {
        Task<int> GetSomeNumberTaskAsync();
        Task<int> GetSomeNumberWithParamTaskAsync(Model1 model);
        ValueTask<int> GetSomeNumberValueTaskAsync();
        ValueTask<int> GetSomeNumberWithParamValueTaskAsync(Model1 model);
    }
}
