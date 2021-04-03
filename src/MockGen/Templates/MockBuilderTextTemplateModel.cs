using MockGen.Model;
using MockGen.ViewModel;

namespace MockGen.Templates
{
    public partial class MockBuilderTextTemplate
    {
        public MockBuilderTextTemplate(MockDescriptor mock)
        {
            view = new MockView(mock);
        }

        public MockView view { get; private set; }
    }
}