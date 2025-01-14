using UnityEngine;

namespace Base.Helpers
{
    public static class VectorUtilities
    {
        public static float GetDistance(this Vector3 from, Vector3 to)
        {
            from.y = 0;
            to.y = 0;
            return (from - to).sqrMagnitude;
        }
    }
}