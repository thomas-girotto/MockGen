﻿using System;

namespace MockGen.Setup
{
    internal class MethodSetupVoid : IMethodSetup
    {
        private int numberOfCalls;
        private Action executeSetupAction = () => { };


        public int NumberOfCalls => numberOfCalls;
        
        public void ExecuteSetup()
        {
            numberOfCalls++;
            executeSetupAction();
        }

        public void Throws<TException>() where TException : Exception, new()
        {
            executeSetupAction = () => throw new TException();
        }

        public void Throws<TException>(TException exception) where TException : Exception
        {
            executeSetupAction = () => throw exception;
        }
    }
}
