using MockGen.Specs.Generated.Helpers;
using MockGen.Specs.Sut;

namespace MockGen.Specs.Generated.IDependencyNs
{
    internal class IDependencyMockBuilder
    {
        private readonly MethodSetup<int> getSomeNumberSetup = new MethodSetup<int>();
        private readonly MethodSetup<int, string> getSomeNumberSetupOverload1 = new MethodSetup<int, string>();

        public IMethodSetup<int> GetSomeNumber()
        {
            return getSomeNumberSetup;
        }

        public IMethodSetup<int> GetSomeNumber(Arg<string> paramValue)
        {
            getSomeNumberSetupOverload1.ForParameter(paramValue);
            return getSomeNumberSetupOverload1;
        }

        public IDependency Build()
        {
            return new IDependencyMock(getSomeNumberSetup, getSomeNumberSetupOverload1);
        }
    }
}
