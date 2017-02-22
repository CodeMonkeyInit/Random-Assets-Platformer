using System;
using UnityEngine;

namespace UnityEngine
{
    public static class ExtensionMethods
    {
        public static Vector2 toVector2(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.y);
        }
    }
}

