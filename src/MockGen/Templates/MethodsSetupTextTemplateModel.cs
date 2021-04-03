using MockGen.Model;
using MockGen.ViewModel;

namespace MockGen.Templates
{
    public partial class MethodsSetupTextTemplate
    {
        public MethodsSetupTextTemplate(Mock mock)
        {
            view = new MockView(mock);
        }

        private MockView view;
    }
}
