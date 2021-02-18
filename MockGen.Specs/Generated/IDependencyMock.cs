using MockGen.Specs.Generated.Helpers;
using MockGen.Specs.Sut;

namespace MockGen.Specs.Generated.IDependencyNs
{
    internal class IDependencyMock : IDependency
    {
        private readonly MethodSetup<int> getSomeNumberSetup;
        private readonly MethodSetup<int, string> getSomeNumberSetupOverload1;

        public IDependencyMock(MethodSetup<int> getSomeNumberSetup, MethodSetup<int, string> getSomeNumberSetupOverload1)
        {
            this.getSomeNumberSetup = getSomeNumberSetup;
            this.getSomeNumberSetupOverload1 = getSomeNumberSetupOverload1;
        }

        public int GetSomeNumber()
        {
            getSomeNumberSetup.Spy.WasCalled();
            return getSomeNumberSetup.GetValue();
        }

        public int GetSomeNumber(string paramValue)
        {
            getSomeNumberSetupOverload1.Spy.WasCalledWithParam(paramValue);
            return getSomeNumberSetupOverload1.GetValue(paramValue);
        }
    }
}
