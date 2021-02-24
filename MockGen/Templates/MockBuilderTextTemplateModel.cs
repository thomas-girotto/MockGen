using MockGen.Model;

namespace MockGen.Templates
{
    public partial class MockBuilderTextTemplate
    {
        public MockBuilderTextTemplate(MockDescriptor descriptor)
        {
            Descriptor = descriptor;
        }

        public MockDescriptor Descriptor { get; private set; }
    }
}