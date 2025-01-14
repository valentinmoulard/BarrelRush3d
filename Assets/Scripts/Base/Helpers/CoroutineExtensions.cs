using System.Collections.Generic;
using UnityEngine;

namespace Base.Helpers
{
    public static class CoroutineExtensions
    {
        private static readonly Dictionary<float, WaitForSeconds> s_waitDictionary = new ();

        public static WaitForSeconds WaitForSeconds(float time)
        {
            if (s_waitDictionary.TryGetValue(time, out var wait)) return wait;
            s_waitDictionary[time] = new WaitForSeconds(time);
            return s_waitDictionary[time];
        }
    }
}