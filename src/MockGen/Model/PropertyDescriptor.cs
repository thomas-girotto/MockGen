namespace MockGen.Model
{
    public class PropertyDescriptor
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public bool HasGetter { get; set; }
        public bool HasSetter { get; set; }
    }
}
