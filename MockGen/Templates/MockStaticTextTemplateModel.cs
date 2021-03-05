using MockGen.Model;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Templates
{
    public partial class MockStaticTextTemplate
    {
        public MockStaticTextTemplate(List<MockDescriptor> descriptor)
        {
            Mocks = descriptor;
        }

        public List<MockDescriptor> Mocks { get; private set; }

        public IEnumerable<string> Namespaces => Mocks.SelectMany(m => m.Namespaces).Distinct();
    }
}
