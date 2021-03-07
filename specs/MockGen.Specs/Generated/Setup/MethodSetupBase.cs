using System;

namespace MockGen.Setup
{
    internal abstract class MethodSetupBase
    {
        protected bool setupDone = false;

        internal virtual void SetupDone() => setupDone = true;

        protected void EnsureConfigurationMethodsAreAllowed(string methodName)
        {
            if (setupDone)
            {
                throw new InvalidOperationException($"{methodName} method is not allowed once the mock has been built");
            }
        }

        protected void EnsureSpyingMethodsAreAllowed(string methodName)
        {
            if (!setupDone)
            {
                throw new InvalidOperationException($"{methodName} method is not allowed until the mock has been built");
            }
        }
    }
}
