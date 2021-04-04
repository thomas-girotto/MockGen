using System.Collections.Generic;

namespace MockGen.Model
{
    public class Method
    {
        public ReturnType ReturnType { get; set; }
        
        public List<Parameter> Parameters { get; set; } = new List<Parameter>();

        public bool ReturnsVoid => ReturnType == ReturnType.Void;

        public string Name { get; set; }

        /// <summary>
        /// Make sure that we have a unique name (in case of method overload).
        /// For instance when method name is used to build some class attribute, it must be unique
        /// </summary>
        public string UniqueName { get; set; }

        public bool IsVirtual { get; set; }
    }
}
