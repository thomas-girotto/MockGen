﻿namespace MockGen.Setup
{
    internal interface IPropertyGet<T>
    {
        IMethodSetupReturn<T> Get { get; }
    }

    internal interface IPropertySet<T>
    {
        IMethodSetup<T> Set(Arg<T> value);
    }

    internal interface IPropertyGetSet<T> : IPropertyGet<T>, IPropertySet<T> { }

    internal class PropertySetup<T> : IPropertyGetSet<T>
    {
        private readonly MethodSetupReturn<T> getPropertySetup = new MethodSetupReturn<T>();
        private readonly MethodSetupVoid<T> setPropertySetup = new MethodSetupVoid<T>();

        internal void SetupDone()
        {
            getPropertySetup.SetupDone();
            setPropertySetup.SetupDone();
        }

        internal T ExecuteGetSetup() => getPropertySetup.ExecuteSetup();
        
        internal void ExecuteSetSetup(T param) => setPropertySetup.ExecuteSetup(param);

        public IMethodSetupReturn<T> Get => getPropertySetup;

        public IMethodSetup<T> Set(Arg<T> value) => setPropertySetup.ForParameter(value);
    }
}
