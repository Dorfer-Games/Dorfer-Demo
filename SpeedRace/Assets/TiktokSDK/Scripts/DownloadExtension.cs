using System;
using System.Collections;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using WebP;

public class DownloadExtension : MonoBehaviour
{
    public static DownloadExtension Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private IEnumerator DownloadRoutine(string imageUrl, Action<Texture2D> onSuccess)
    {
        if (imageUrl.Contains(".webp"))
        {
            var task = DownloadWebP(imageUrl);
            yield return new WaitUntil(() => task.IsCompleted);
            onSuccess?.Invoke(task.Result);
            yield break;
        }
        imageUrl = UnityWebRequest.UnEscapeURL(imageUrl);
        var request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            var textureDownloadHandler = (DownloadHandlerTexture)request.downloadHandler;
            var texture = textureDownloadHandler.texture;
            if (texture == null)
            {
                Debug.Log("Image not available");
                yield break;
            }

            onSuccess?.Invoke(texture);
            yield break;
        }

    }

    private async Task<Texture2D> DownloadWebP(string url)
    {
        using var client = new WebClient();
        var bytes = await client.DownloadDataTaskAsync(url);
        var texture = Texture2DExt.CreateTexture2DFromWebP(bytes, true, true, out var error, makeNoLongerReadable: false);
        if (error != Error.Success)
        {
            Debug.LogError("Cannot parce webp");
        }
        return texture;
    }

    public void DownloadAndSetSprite(string url, Image image)
    {
        StartCoroutine(DownloadRoutine(url, texture =>
        {
            image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }));
    }
}
