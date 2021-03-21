namespace MockGen.Setup
{
    internal interface IPropertyGetSetSetup<T>
    {
        IMethodSetupReturn<T> Get { get; }
        IPropertySetSetup<T> Set { get; }
    }

    internal class PropertyGetSetSetup<T> : IPropertyGetSetSetup<T>
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

        public IPropertySetSetup<T> Set => setPropertySetup;
    }
}
