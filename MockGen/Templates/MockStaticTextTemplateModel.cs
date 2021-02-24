using MockGen.Model;

namespace MockGen.Templates
{
    public partial class MockStaticTextTemplate
    {
        public MockStaticTextTemplate(MockDescriptor descriptor)
        {
            Descriptor = descriptor;
        }

        public MockDescriptor Descriptor { get; private set; }
    }
}
