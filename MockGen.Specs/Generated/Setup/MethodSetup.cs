﻿using System;

namespace MockGen.Setup
{
    internal abstract class MethodSetup : MethodSetupBase
    {
        protected Action additionalCallback = () => { };
        protected int numberOfCalls;

        public int NumberOfCalls
        {
            get
            {
                EnsureSpyingMethodsAreAllowed(nameof(NumberOfCalls));
                return numberOfCalls;
            }
        }

        public abstract void Throws<TException>() where TException : Exception, new();

        public abstract void Throws<TException>(TException exception) where TException : Exception;

        public void Execute(Action callback)
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Execute));
            additionalCallback = callback;
        }
    }
}