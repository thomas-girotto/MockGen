namespace MockGen.Specs.Sut
{
    public interface IDependency
    {
        void DoSomething();
        void DoSomethingWithParameter(int input);
        void DoSomethingWithReferenceTypeParameter(Model model);
        int GetSomeNumber();
        int GetSomeNumberWithParameter(int input);
    }
}
