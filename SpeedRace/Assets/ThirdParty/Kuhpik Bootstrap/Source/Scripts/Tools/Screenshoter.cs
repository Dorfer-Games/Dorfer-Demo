using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Kuhpik.Tools
{
    // Thx to https://gist.github.com/yasirkula/fba5c7b5280aa90cdb66a68c4005b52d
    // Unity's github https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/GameView/GameView.cs
    // And lovely Google https://docs.unity3d.com/ScriptReference/ScreenCapture.html

    public enum EScreenTypes
    {
        png,
        jpg,
        jpeg
    }

    [DefaultExecutionOrder(10)]
    public class Screenshoter : MonoBehaviour
    {
        [SerializeField][BoxGroup("Settings")] private KeyCode screenshotKey;
        [SerializeField][BoxGroup("Settings")] private Vector2Int[] targetResolutions;
        [SerializeField][BoxGroup("Settings")] private List<EScreenTypes> screenTypes;
        [SerializeField][BoxGroup("Path")][ReadOnly] private string path;

#if UNITY_EDITOR

        [Button] private void SelectFolder() { path = EditorUtility.OpenFolderPanel("Choose output directory", "", ""); }
        [Button] private void OpenFolder() { if (Directory.Exists(path)) System.Diagnostics.Process.Start("explorer.exe", path.Replace("/", "\\")); }
        [Button] private void Capture() { if (!isBusy) StartCoroutine(ScreenshoterRoutine()); }

        private CameraInstaller cameraInstaller;
        private List<Vector2Int> indexesList;
        private EditorWindow gameView;
        private object sizeHolder;
        private bool isBusy;
        private int index;
        private Dictionary<Vector2Int, int> indexes;

        private void InitScreenTypes()
        {
            if (screenTypes.Count == 0)
            {
                screenTypes = new List<EScreenTypes>();
                screenTypes.Add(EScreenTypes.jpeg);
            }
            else
            {
                for (int i = 0; i < screenTypes.Count; i++)
                {
                    for (int j = 0; j < screenTypes.Count; j++)
                    {
                        if (i != j && screenTypes[i] == screenTypes[j])
                        {
                            screenTypes.RemoveAt(i);
                        }
                    }
                }
            }
        }

        private void Awake()
        {
            InitScreenTypes();
            sizeHolder = GetType("GameViewSizes").FetchProperty("instance").FetchProperty("currentGroup");
            gameView = GetWindow(GetType("GameView"));
            index = (int)gameView.FetchProperty("selectedSizeIndex");
            isBusy = false;

            indexes = new Dictionary<Vector2Int, int>();
            indexesList = new List<Vector2Int>();

            foreach (var resolution in targetResolutions)
            {
                var customSize = GetFixedResolution(resolution.x, resolution.y);
                sizeHolder.CallMethod("AddCustomSize", customSize);

                var customIndex = (int)sizeHolder.CallMethod("IndexOf", customSize) + (int)sizeHolder.CallMethod("GetBuiltinCount");
                indexes.Add(resolution, customIndex);

                indexesList.Add(resolution);
            }
        }

        private void OnDestroy()
        {
            for (int i = indexesList.Count - 1; i >= 0; i--)
            {
                sizeHolder.CallMethod("RemoveCustomSize", indexes[indexesList[i]]);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(screenshotKey) && !isBusy)
            {
                StartCoroutine(ScreenshoterRoutine());
            }
        }

        private IEnumerator ScreenshoterRoutine()
        {
            isBusy = true;
            Time.timeScale = 0;

            foreach (var resolution in targetResolutions)
            {
                gameView.CallMethod("SizeSelectionCallback", indexes[resolution], null);
                gameView.Repaint();
                yield return new WaitForSecondsRealtime(0.1f);

                cameraInstaller.FindOrUse().Resize();
                yield return new WaitForEndOfFrame();

                CaptureScreenshot(resolution);
                yield return null;
            }

            gameView.CallMethod("SizeSelectionCallback", index, null);
            gameView.Repaint();
            yield return null;
            cameraInstaller.FindOrUse().Resize();

            Time.timeScale = 1;
            isBusy = false;
        }

        private void CaptureScreenshot(Vector2Int resolution)
        {
            var screenshot = ScreenCapture.CaptureScreenshotAsTexture();
            foreach (var screenType in screenTypes)
            {
                var ShotPath = path + $"/{resolution.x}x{resolution.y}" + $"/{screenType}";
                if (!Directory.Exists(ShotPath))
                    Directory.CreateDirectory(ShotPath);

                int screenNumber = Directory.GetFiles(ShotPath).Length + 1;
                string fileName = $"Screenshot {screenNumber}." + screenType;

                while (File.Exists(ShotPath + "/" + fileName))
                {
                    screenNumber++;
                    fileName = $"Screenshot {screenNumber}." + screenType;
                }
                File.WriteAllBytes(Path.Combine(ShotPath, $"Screenshot {screenNumber}." + screenType), screenshot.EncodeToJPG());
            }
            Destroy(screenshot);
        }

        #region Helpers

        private Type GetType(string type)
        {
            return typeof(EditorWindow).Assembly.GetType("UnityEditor." + type);
        }

        private EditorWindow GetWindow(Type type)
        {
            return EditorWindow.GetWindow(type);
        }

        private object GetFixedResolution(int width, int height)
        {
            object sizeType = Enum.Parse(GetType("GameViewSizeType"), "FixedResolution");
            return GetType("GameViewSize").CreateInstance(sizeType, width, height, "temporary");
        }

        #endregion
#endif
    }
}
