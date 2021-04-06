using MockGen.ViewModel;

namespace MockGen.Templates.Setup
{
    public partial class MethodSetupReturnTaskPnTextTemplate
    {
        private readonly MethodsInfoView view;
        private readonly TaskContext taskContext;

        public MethodSetupReturnTaskPnTextTemplate(MethodsInfoView view, TaskContext taskContext)
        {
            this.view = view;
            this.taskContext = taskContext;
        }
    }
}
