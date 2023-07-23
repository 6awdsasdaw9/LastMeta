using System;
using Code.Data.GameData;
using UnityEngine;

namespace Code.Data.AdditionalData
{
    public static class Extensions
    {
        public static Vector3Data AsVectorData(this Vector3 vector) => new(vector.x, vector.y, vector.z);

        public static Vector3 AsUnityVector(this Vector3Data vector3Data) => 
            vector3Data == null ? Vector3.zero : new Vector3(vector3Data.X, vector3Data.Y, vector3Data.Z);

 

        public static T With<T>(this T self, Action<T> set)
        {
            set.Invoke(self);
            return self;
        }

        public static T With<T>(this T self, Action<T> apply, Func<bool> when)
        {
            if (when())
                apply?.Invoke(self);

            return self;
        }

        public static T With<T>(this T self, Action<T> apply, bool when)
        {
            if (when)
                apply?.Invoke(self);

            return self;
        }
        
        
    }
}