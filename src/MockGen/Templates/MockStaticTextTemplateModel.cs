using MockGen.Model;
using MockGen.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Templates
{
    public partial class MockStaticTextTemplate
    {
        public MockStaticTextTemplate(IEnumerable<Mock> descriptor)
        {
            Mocks = descriptor;
        }

        public IEnumerable<Mock> Mocks { get; private set; }

        public IEnumerable<string> Namespaces => Mocks
            .SelectMany(m => new MockView(m).Namespaces)
            .Distinct()
            .Where(ns => !string.IsNullOrEmpty(ns));
    }
}
