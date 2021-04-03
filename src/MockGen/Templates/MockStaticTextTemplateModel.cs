using MockGen.Model;
using MockGen.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Templates
{
    public partial class MockStaticTextTemplate
    {
        public MockStaticTextTemplate(IEnumerable<MockDescriptor> descriptor)
        {
            Mocks = descriptor;
        }

        public IEnumerable<MockDescriptor> Mocks { get; private set; }

        public IEnumerable<string> Namespaces => Mocks
            .SelectMany(m => new MockView(m).Namespaces)
            .Distinct()
            .Where(ns => !string.IsNullOrEmpty(ns));
    }
}
