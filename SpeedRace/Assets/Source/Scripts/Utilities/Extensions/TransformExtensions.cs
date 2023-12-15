using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityTools.Extentions
{
    public static class TransformExtensions
    {
        public static void ApplyPoint(this Transform transform, Point point)
        {
            transform.position = point.position;
            transform.rotation = point.rotation;
        }

        public static Vector3 GetLookRotationY(this Transform transform, Transform target)
        {
            var rotation = Quaternion.LookRotation(target.position - transform.position).eulerAngles;
            rotation = new Vector3(0, rotation.y, 0);
            return rotation;
        }

        public static void MoveTowards(this Transform transform, Vector3 target, float maxDelta)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, maxDelta);
        }

        public static int GetGlobalSiblingIndexFast(this Transform transform)
        {
            return int.Parse(GetGlobalSiblingIndex(transform).Inverse());

            string GetGlobalSiblingIndex(Transform transform)
            {
                var index = transform.GetSiblingIndex().ToString();
                var parent = transform.parent;
                if (parent != null) index += GetGlobalSiblingIndex(parent);
                return index;
            }
        }

        public static int GetGlobalSiblingIndexCorrect(this Transform transform)
        {
            var map = BuildMap();
            return map.IndexOf(transform);
        }

        public static List<int> GetGlobalSiblingIndexCorrect(this List<Transform> transforms)
        {
            var map = BuildMap();
            return transforms.Select(x => map.IndexOf(x)).ToList();
        }

        private static List<Transform> BuildMap()
        {
            var map = new List<Transform>(1024);
            var roots = GetRoots();
            roots.ForEach(Map);
            return map;

            void Map(Transform obj)
            {
                map.Add(obj);
                obj.GetChildrens().ForEach(Map);
            }
        }

        private static IEnumerable<Transform> GetRoots()
        {
            return Object
                .FindObjectsOfType<GameObject>()
                .Select(x => x.transform.root)
                .Distinct()
                .OrderBy(x => x.GetSiblingIndex());
        }
    }
}