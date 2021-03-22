using MockGen.Setup;
using MockGen.Specs.Sut;

namespace MockGen
{
    internal class IDependencyMethodsSetup
    {
        internal PropertySetup<Model1> GetOnlyPropertySetup { get; } = new PropertySetup<Model1>();
        internal PropertySetup<Model1> SetOnlyPropertySetup { get; } = new PropertySetup<Model1>();
        internal PropertySetup<Model1> GetSetPropertySetup { get; } = new PropertySetup<Model1>();
        internal MethodSetupVoid DoSomethingSetup { get; } = new MethodSetupVoid();
        internal MethodSetupVoid<int> DoSomethingWithParameterSetup { get; } = new MethodSetupVoid<int>();
        internal MethodSetupVoid<Model1> DoSomethingWithReferenceTypeParameterSetup { get; } = new MethodSetupVoid<Model1>();
        internal MethodSetupVoid<Model1, Model2> DoSomethingWithTwoParametersSetup { get; } = new MethodSetupVoid<Model1, Model2>();
        internal MethodSetupReturn<int> GetSomeNumberSetup { get; } = new MethodSetupReturn<int>();
        internal MethodSetupReturn<int, int> GetSomeNumberWithParameterSetup { get; } = new MethodSetupReturn<int, int>();
        internal MethodSetupReturn<Model1, int> GetSomeNumberWithReferenceTypeParameterSetup { get; } = new MethodSetupReturn<Model1, int>();
        internal MethodSetupReturn<Model1, Model2, int> GetSomeNumberWithTwoParametersSetup { get; } = new MethodSetupReturn<Model1, Model2, int>();

        internal void SetupDone()
        {
            GetOnlyPropertySetup.SetupDone();
            SetOnlyPropertySetup.SetupDone();
            GetSetPropertySetup.SetupDone();
            DoSomethingSetup.SetupDone();
            DoSomethingWithParameterSetup.SetupDone();
            DoSomethingWithReferenceTypeParameterSetup.SetupDone();
            DoSomethingWithTwoParametersSetup.SetupDone();
            GetSomeNumberSetup.SetupDone();
            GetSomeNumberWithParameterSetup.SetupDone();
            GetSomeNumberWithReferenceTypeParameterSetup.SetupDone();
            GetSomeNumberWithTwoParametersSetup.SetupDone();
        }
    }
}
