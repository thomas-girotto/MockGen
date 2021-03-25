using System.Collections.Generic;
using System.Linq;

namespace MockGen.Model
{
    public class Type
    {
        public static Type Void = new Type("void", Enumerable.Empty<string>());

        public Type(string name) : this(name, Enumerable.Empty<string>()) { }
        public Type(string name, string @namespace) : this(name, new string[] { @namespace }) { }
        public Type(string name, IEnumerable<string> namespaces)
        {
            Name = name;
            FullName = namespaces.Any() ? $"{namespaces.First()}.{name}" : string.Empty;
            Namespaces = namespaces;
        }

        /// <summary>
        /// Short name of the type
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Long name of the type (including namespace)
        /// </summary>
        public string FullName { get; }
        /// <summary>
        /// List of namespaces needed to use this type. Can have several when the type is generic
        /// </summary>
        public IEnumerable<string> Namespaces { get; }
    }
}
