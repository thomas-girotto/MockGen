using MockGen.Model;

namespace MockGen.Templates.Setup
{
    public partial class ActionConfigurationPnTextTemplate
    {
        public ActionConfigurationPnTextTemplate(GenericTypesDescriptor descriptor)
        {
            Descriptor = descriptor;
        }
        
        public GenericTypesDescriptor Descriptor { get; set; }
    }
}
