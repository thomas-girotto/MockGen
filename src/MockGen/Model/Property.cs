namespace MockGen.Model
{
    public class Property
    {
        public string Name { get; set; }
        public ReturnType Type { get; set; }
        public bool HasGetter { get; set; }
        public bool HasSetter { get; set; }
    }
}
