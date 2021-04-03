using MockGen.Model;
using MockGen.ViewModel;

namespace MockGen.Templates
{
    public partial class MockTextTemplate
    {
        private readonly MockView view;

        public MockTextTemplate(Mock mock)
        {
            view = new MockView(mock);
        }

    }
}
