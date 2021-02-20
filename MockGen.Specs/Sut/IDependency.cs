namespace MockGen.Specs.Sut
{
    public interface IDependency
    {
        void DoSomething();
        void DoSomethingWithParameter(int input);
        int GetSomeNumber();
        int GetSomeNumberWithParameter(int input);
    }
}
