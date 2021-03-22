using System;

namespace MockGen.Sample
{
    public interface IDependency
    {
        void DoSomething();
        void DoSomethingWithParam(SomeModel value);
        int ReturnSomeInt();
        int ReturnSomeIntWithParam(SomeModel input);
    }
}
