using System.Threading.Tasks;

namespace MockGen.Sample
{
    public interface ITaskDependency
    {
        Task<SomeModel> GetSomeModelTaskAsync(int id);
        ValueTask<SomeModel> GetSomeModelValueTaskAsync(int id);
    }
}
