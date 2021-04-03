namespace MockGen.Model
{
    public class Parameter
    {
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
