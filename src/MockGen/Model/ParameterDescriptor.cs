namespace MockGen.Model
{
    public class ParameterDescriptor
    {
        public ParameterDescriptor(string type, string name, string @namespace)
        {
            Name = name;
            Type = type;
            Namespace = @namespace;
        }

        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Namespace { get; private set; }
    }
}
