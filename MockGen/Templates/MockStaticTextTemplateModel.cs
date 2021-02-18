using System;
using System.Collections.Generic;

namespace MockGen.Templates
{
    public partial class MockStaticTextTemplate
    {
        public MockStaticTextTemplate()
        {
        }

        public MockStaticTextTemplate(List<string> interfaceNames)
        {
            InterfaceNames = interfaceNames;
        }

        public List<string> InterfaceNames { get; set; }
    }
}
