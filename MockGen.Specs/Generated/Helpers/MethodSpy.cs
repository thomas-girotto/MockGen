using System.Collections.Generic;

namespace MockGen.Specs.Generated.Helpers
{
    internal class MethodSpy
    {
        private int totalCalls;

        internal void WasCalled()
        {
            totalCalls++;
        }

        internal int TotalCalls => totalCalls;
    }

    internal class MethodSpy<TParam>
    {
        private Dictionary<TParam, int> callsByParamValue = new Dictionary<TParam, int>();

        internal void WasCalled(TParam paramValue)
        {
            if (callsByParamValue.ContainsKey(paramValue))
            {
                callsByParamValue[paramValue] = ++callsByParamValue[paramValue];
            }
            else
            {
                callsByParamValue.Add(paramValue, 1);
            }
        }

        internal int GetCallsFor(Arg<TParam> param) 
        {
            if (param.Equals(Arg<TParam>.Any))
            {
                return callsByParamValue.Count;
            }
            if (callsByParamValue.ContainsKey(param.Value))
            {
                return callsByParamValue[param.Value];
            }
            return 0;
        }
    }
}
