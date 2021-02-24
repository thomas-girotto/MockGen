namespace MockGen.Specs.Sut
{
    public interface IDependency
    {
        void DoSomething();
        void DoSomethingWithParameter(int input);
        void DoSomethingWithReferenceTypeParameter(Model1 model);
        void DoSomethingWithTwoParameters(Model1 model1, Model2 model2);
        int GetSomeNumber();
        int GetSomeNumberWithParameter(int input);
        int GetSomeNumberWithReferenceTypeParameter(Model1 model1);
        int GetSomeNumberWithTwoParameters(Model1 model1, Model2 model2);
    }
}
