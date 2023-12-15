using UnityEngine;

namespace UnityTools.Extentions
{
    public static class RenderTextureExtensions
    {
        public static Texture2D ToTexture2D(this RenderTexture renderTexture)
        {
            Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
            var tempRenderTexture = RenderTexture.active;
            RenderTexture.active = renderTexture;
            tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            tex.Apply();
            RenderTexture.active = tempRenderTexture;
            return tex;
        }
    }
}