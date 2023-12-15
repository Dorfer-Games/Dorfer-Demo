using UnityEngine;
using UnityEngine.UI;

namespace UnityTools.Extentions
{
    public static class ImageExtensions
    {
        public static void SetSpriteWithoutBackground(this Image image, Sprite sprite)
        {
            var color = sprite != null ? new Color(1, 1, 1, 1) : new Color(0, 0, 0, 0);
            image.color = color;
            image.sprite = sprite;
        }
    }
}