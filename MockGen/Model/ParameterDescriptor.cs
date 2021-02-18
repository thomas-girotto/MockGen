namespace MockGen
{
    public class ParameterDescriptor
    {
        public ParameterDescriptor(string type, string name)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; private set; }
        public string Type { get; private set; }
    }
}
