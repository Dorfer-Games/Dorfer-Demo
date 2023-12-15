using System;
using System.Collections.Generic;

namespace UnityTools.Extentions
{
    public static class EnumExtentions
    {
        public static int Count(this Enum value)
        {
            return Enum.GetValues(value.GetType()).Length;
        }

        public static void Foreach<T>(this Enum values, Action<T> action)
        {
            foreach (var element in Enum.GetValues(values.GetType()))
            {
                action((T)element);
            }
        }

        public static void Foreach(this Enum values, Action<object> action)
        {
            foreach (var element in Enum.GetValues(values.GetType()))
            {
                action(element);
            }
        }

        public static IEnumerable<T> Enumerate<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)) as T[];
        }
    }
}