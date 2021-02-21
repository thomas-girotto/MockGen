﻿using System.Collections.Generic;
using System.Linq;

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
        private Dictionary<Arg<TParam>, int> callsByParamValue = new Dictionary<Arg<TParam>, int>();

        internal void WasCalled(Arg<TParam> paramValue)
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
            if (ReferenceEquals(param, Arg<TParam>.Any))
            {
                return callsByParamValue.Values.Sum();
            }
            if (callsByParamValue.ContainsKey(param))
            {
                return callsByParamValue[param];
            }
            return 0;
        }
    }
}
