using System;
using System.Collections.Generic;
using System.Text;

namespace MockGen.Specs.Generated.Helpers
{
    interface IMethodSetupReturn<TReturn> : IMethodSetup
    {
        void Returns(TReturn value);
    }
}
