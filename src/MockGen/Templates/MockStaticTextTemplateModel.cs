using MockGen.Model;
using MockGen.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Templates
{
    public partial class MockStaticTextTemplate
    {
        public List<MockView> Mocks { get; private set; }

        public MockStaticTextTemplate(IEnumerable<Mock> mocks)
        {
            Mocks = mocks.Select(m => new MockView(m)).ToList();
        }

        public IEnumerable<string> Namespaces => Mocks
            .SelectMany(m => m.Namespaces)
            .Distinct()
            .Where(ns => !string.IsNullOrEmpty(ns));
    }
}
