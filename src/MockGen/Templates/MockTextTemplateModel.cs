using MockGen.Model;
using MockGen.ViewModel;
using System.Collections.Generic;
using System.Linq;

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
