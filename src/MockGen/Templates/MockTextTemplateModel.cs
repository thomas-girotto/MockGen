using MockGen.Model;

namespace MockGen.Templates
{
    public partial class MockTextTemplate
    {
        public MockTextTemplate(MockDescriptor descriptor)
        {
            Descriptor = descriptor;
        }

        public MockDescriptor Descriptor { get; private set; }
    }
}
