using MockGen.Specs.Generated.Helpers;
using MockGen.Specs.Sut;

namespace MockGen.Specs.Generated.IDependencyNs
{
    internal class IDependencyMockBuilder
    {
        private readonly MethodSetup<int> getSomeNumberSetup = new MethodSetup<int>();
        private readonly MethodSetup<int, int> getSomeNumberWithParameterSetup = new MethodSetup<int, int>();

        public IMethodSetup<int> GetSomeNumber()
        {
            return getSomeNumberSetup;
        }

        public IMethodSetup<int, int> GetSomeNumberWithParameter(Arg<int> input)
        {
            return getSomeNumberWithParameterSetup.ForParameter(input);
        }

        public IDependency Build()
        {
            return new IDependencyMock(getSomeNumberSetup, getSomeNumberWithParameterSetup);
        }
    }
}
