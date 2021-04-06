using System.Threading.Tasks;

namespace MockGen.Specs.Sut
{
    public interface IDependency
    {
        Model1 GetOnlyProperty { get; }
        Model1 SetOnlyProperty { set; }
        Model1 GetSetProperty { get; set; }
        void DoSomething();
        void DoSomethingWithParameter(int input);
        void DoSomethingWithReferenceTypeParameter(Model1 model);
        void DoSomethingWithTwoParameters(Model1 model1, Model2 model2);
        int GetSomeNumber();
        int GetSomeNumberWithParameter(int input);
        int GetSomeNumberWithReferenceTypeParameter(Model1 model1);
        int GetSomeNumberWithTwoParameters(Model1 model1, Model2 model2);
        Task<int> GetSomeNumberAsync();
        ValueTask<int> GetSomeNumberWithValueTaskAsync();
        bool TryGetById(int id, out Model1 model);
        bool TryGetById(int id, out Model1 model, out Model2 model2);
    }
}
