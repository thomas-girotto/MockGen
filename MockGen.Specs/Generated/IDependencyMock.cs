using MockGen.Specs.Generated.Helpers;
using MockGen.Specs.Sut;

namespace MockGen.Specs.Generated.IDependencyNs
{
    internal class IDependencyMock : IDependency
    {
        private readonly MethodSetup<int> getSomeNumberSetup;
        private readonly MethodSetup<int, int> getSomeNumberWithParameterSetup;

        public IDependencyMock(MethodSetup<int> getSomeNumberSetup, MethodSetup<int, int> getSomeNumberWithParameterSetup)
        {
            this.getSomeNumberSetup = getSomeNumberSetup;
            this.getSomeNumberWithParameterSetup = getSomeNumberWithParameterSetup;
        }

        public int GetSomeNumber()
        {
            getSomeNumberSetup.Spy.WasCalled();
            return getSomeNumberSetup.GetValue();
        }

        public int GetSomeNumberWithParameter(int input)
        {
            getSomeNumberWithParameterSetup.Spy.WasCalled(input);
            return getSomeNumberWithParameterSetup.GetValue(input);
        }
    }
}
