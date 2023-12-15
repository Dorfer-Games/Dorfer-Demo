using System;
using System.Collections.Generic;

namespace UnityTools.Extentions
{
    public static class ReflectionExtensions
    {
        public static Type[] GetAllDerivedTypes(this AppDomain aAppDomain, Type baseType)
        {
            var result = new List<Type>();
            var assemblies = aAppDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsSubclassOf(baseType))
                    {
                        result.Add(type);
                    }
                }
            }
            return result.ToArray();
        }
    }
}