namespace MockGen.Model
{
    public class PropertyDescriptor
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Namespace { get; set; }
        public bool HasGetter { get; set; }
        public bool HasSetter { get; set; }
    }
}
