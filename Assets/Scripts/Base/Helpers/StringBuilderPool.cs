using System.Collections.Generic;
using System.Text;

namespace Base.Helpers
{
    public static class StringBuilderPool
    {
        private static readonly Stack<StringBuilder> StrBuilderPool = new ();

        public static StringBuilder Get()
        {
            lock (StrBuilderPool) // Ensure thread safety
            {
                return StrBuilderPool.Count == 0 ? new StringBuilder() : StrBuilderPool.Pop();
            }
        }

        public static void Release(this StringBuilder sb)
        {
            if (sb == null) return;

            lock (StrBuilderPool) // Ensure thread safety
            {
                sb.Clear();
                StrBuilderPool.Push(sb);
            }
        }
    }
}