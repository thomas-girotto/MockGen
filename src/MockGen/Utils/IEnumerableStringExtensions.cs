using System.Linq;

namespace System.Collections.Generic
{
    public static class IEnumerableStringExtensions
    {
        public static string JoinAsParameters(this IEnumerable<string> args)
        {
            return string.Join(", ", args.Where(arg => !string.IsNullOrEmpty(arg)));
        }
    }
}
