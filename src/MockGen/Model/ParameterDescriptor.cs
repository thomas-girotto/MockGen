namespace MockGen.Model
{
    public class ParameterDescriptor
    {
        public ParameterDescriptor(Type type, string name)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; private set; }
        public Type Type { get; private set; }
    }
}
