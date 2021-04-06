using MockGen.Setup;

namespace MockGen
{
    internal class ITaskDependencyMethodsSetup
    {
        internal MethodSetupReturnTask<int> GetSomeNumberTaskAsyncSetup { get; } = new MethodSetupReturnTask<int>();
        internal MethodSetupReturnValueTask<int> GetSomeNumberValueTaskAsyncSetup { get; } = new MethodSetupReturnValueTask<int>();

        internal void SetupDone()
        {
            GetSomeNumberTaskAsyncSetup.SetupDone();
            GetSomeNumberValueTaskAsyncSetup.SetupDone();
        }
    }
}
