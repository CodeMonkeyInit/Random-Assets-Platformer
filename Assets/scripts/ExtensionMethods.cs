using System;
using UnityEngine;

namespace UnityEngine
{
    public static class ExtensionMethods
    {
        public static Vector3 toVector3(this Vector2 vector)
        {
            return new Vector3(vector.x, vector.y);
        }

        public static Vector2 toVector2(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.y);
        }

        public static Vector2 Abs(this Vector2 vector)
        {
            return new Vector2(Math.Abs(vector.x), Math.Abs(vector.y));
        }

        public static AudioClip GetRandomSound (this AudioClip[] soundsArray)
        {
            int soundsLastIndex = soundsArray.Length - 1;
            if (soundsLastIndex == 0)
            {
                return soundsArray[0];
            }
            else
            {
                return soundsArray[Random.Range(0, soundsLastIndex)];
            }
        }
    }
}

