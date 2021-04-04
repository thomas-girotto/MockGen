namespace MockGen.Model
{
    /// <summary>
    /// Store information needed to generate helper classes about MethodsSetup.
    /// We want to know for a given number of parameters, if there are some methods to mock that:
    /// - Returns void 
    ///     => if yes we will generate sources for MethodSetupVoid<T1, ...>
    /// - or Returns something 
    ///     => if yes we will generate sources for MethodSetupReturns<T1, ..., TReturn>
    /// - or Returns something but with Task 
    ///     => if yes we will generate sources for MethodSetupReturnTask<T1, ..., TReturn>
    /// We can have all of the possibilities or not.
    /// </summary>
    public record MethodsInfo
    {
        public MethodsInfo(int numberOfParameters, bool hasMethodThatReturnsVoid, bool hasMethodThatReturns, bool hasMethodThatReturnsTask)
        {
            NumberOfParameters = numberOfParameters;
            HasMethodThatReturnsVoid = hasMethodThatReturnsVoid;
            HasMethodThatReturns = hasMethodThatReturns;
            HasMethodThatReturnsTask = hasMethodThatReturnsTask;
        }

        /// <summary>
        /// Number of parameters for the methods
        /// </summary>
        public int NumberOfParameters { get; }
        
        /// <summary>
        /// Is there one method with the given number of parameters that returns void
        /// </summary>
        public bool HasMethodThatReturnsVoid { get; }
        
        /// <summary>
        /// Is there one method with the given number of parameters that returns something other than a Task<T>
        /// </summary>
        public bool HasMethodThatReturns { get; }

        /// <summary>
        /// Is there one method with the given number of parameters that returns a Task<T>
        /// </summary>
        public bool HasMethodThatReturnsTask { get; }
    }
}
