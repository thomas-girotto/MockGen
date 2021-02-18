namespace MockGen.Specs.Sut
{
    public interface IDependency
    {
        void DoSomething();
        int GetSomeNumber();
        int GetSomeNumberWithParameter(int input);
    }
}
