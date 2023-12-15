using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

#if UNITY_EDITOR
namespace UnityTools.Extentions
{
    public static class AssetDatabaseExtentions
    {
        public static T[] LoadAllAssetsOfType<T>() where T : Object
        {
            return AssetDatabase
                .FindAssets($"t:{typeof(T).Name}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>)
                .ToArray();
        }

        public static T[] LoadAllPrefabsWithComponent<T>() where T : Object
        {
            var result = new List<T>();
            var allAssets = AssetDatabase.FindAssets($"t:Prefab");

            foreach (var asset in allAssets)
            {
                var GUID = AssetDatabase.GUIDToAssetPath(asset);
                var asssetGO = AssetDatabase.LoadAssetAtPath<GameObject>(GUID);
                if (asssetGO.TryGetComponent(out T component))
                {
                    result.Add(component);
                }
            }
            return result.ToArray();
        }

        public static void MarkPrefabDirty()
        {
            var prefabStage = PrefabStageUtility.GetCurrentPrefabStage();
            if (prefabStage != null)
            {
                EditorSceneManager.MarkSceneDirty(prefabStage.scene);
            }
        }
    }
}
#endif