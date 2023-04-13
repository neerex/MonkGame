using UnityEngine;

namespace MainGame.Utilities
{
    public static class VectorUtils
    {
        public static Vector3 FlatY(this Vector3 v)
        {
            v.y = 0;
            return v;
        }
    }
}