namespace MockGen.Model
{
    public class Parameter
    {
        /// <summary>
        /// Specific boolean parameter that we add in constructors of concrete class mock to configure if we should
        /// call the base class or not.
        /// </summary>
        public static Parameter CallBaseCtorParameter = new Parameter(new Type("bool"), "callBase", false);

        public Parameter(Type type, string name, bool isOutParameter)
        {
            Name = name;
            Type = type;
            IsOutParameter = isOutParameter;
        }

        public string Name { get; private set; }
        public Type Type { get; private set; }
        public bool IsOutParameter { get; private set; }
    }
}
