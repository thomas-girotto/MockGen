using System.Collections.Generic;
using System.Linq;

namespace MockGen.Model
{
    public class ReturnType : Type
    {
        public static ReturnType Void = new ReturnType("void", false, Enumerable.Empty<string>());

        public ReturnType(string name, bool isTask) : base(name)
        {
            IsTask = isTask;
        }

        public ReturnType(string name, bool isTask, string @namespace) : base(name, @namespace)
        {
            IsTask = isTask;
        }

        public ReturnType(string name, bool isTask, IEnumerable<string> namespaces) : base(name, namespaces)
        {
            IsTask = isTask;
        }

        /// <summary>
        /// Is the type a Task<T>
        /// </summary>
        public bool IsTask { get; }

    }
}
