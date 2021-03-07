using MockGen.Model;

namespace MockGen.Templates
{
    public partial class MethodsSetupTextTemplate
    {
        public MethodsSetupTextTemplate(MockDescriptor descriptor)
        {
            Descriptor = descriptor;
        }

        public MockDescriptor Descriptor { get; private set; }
    }
}
