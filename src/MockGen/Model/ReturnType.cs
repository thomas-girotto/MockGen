using System.Collections.Generic;
using System.Linq;

namespace MockGen.Model
{
    public class ReturnType : Type
    {
        public static ReturnType Void = new ReturnType("void", TaskInfo.NotATask, Enumerable.Empty<string>());

        public ReturnType(string name, TaskInfo taskInfo) : base(name)
        {
            TaskInfo = taskInfo;
        }

        public ReturnType(string name, TaskInfo taskInfo, string @namespace) : base(name, @namespace)
        {
            TaskInfo = taskInfo;
        }

        public ReturnType(string name, TaskInfo taskInfo, IEnumerable<string> namespaces) : base(name, namespaces)
        {
            TaskInfo = taskInfo;
        }

        /// <summary>
        /// Is the type a Task<T>
        /// </summary>
        public TaskInfo TaskInfo { get; }
    }

    public enum TaskInfo
    {
        /// <summary>
        /// The type is neither a Task<T> nor a ValueTask<T>
        /// </summary>
        NotATask,
        /// <summary>
        /// The type is a Task<T>
        /// </summary>
        Task,
        /// <summary>
        /// The type is a ValueTask<T>
        /// </summary>
        ValueTask,
    }
}
