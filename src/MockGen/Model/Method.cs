using System;
using System.Linq;

namespace MockGen.Model
{
    public class Method : Ctor
    {
        public ReturnType ReturnType { get; set; }
        
        public bool ReturnsVoid => ReturnType == ReturnType.Void;

        public string Name { get; set; }

        /// <summary>
        /// Make sure that we have a unique name (in case of method overload).
        /// For instance when method name is used to build some class attribute, it must be unique
        /// </summary>
        public string UniqueName { get; set; }

        public bool ShouldBeOverriden { get; set; }
    }
}
