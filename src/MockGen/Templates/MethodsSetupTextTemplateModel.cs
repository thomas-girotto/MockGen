using MockGen.Model;
using MockGen.ViewModel;

namespace MockGen.Templates
{
    public partial class MethodsSetupTextTemplate
    {
        public MethodsSetupTextTemplate(MockDescriptor mock)
        {
            view = new MockView(mock);
        }

        private MockView view;
    }
}
