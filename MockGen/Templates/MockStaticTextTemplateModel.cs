using MockGen.Model;
using System.Collections.Generic;

namespace MockGen.Templates
{
    public partial class MockStaticTextTemplate
    {
        public MockStaticTextTemplate(List<MockDescriptor> descriptor)
        {
            Descriptor = descriptor;
        }

        public List<MockDescriptor> Descriptor { get; private set; }
    }
}
