using System.Collections;
using HGS.FileControl;
using HGS.FileControl.Texture2DExtension;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Texture2DSample : MonoBehaviour
{
  [SerializeField] Storage storage;
  [SerializeField] string url;
  [SerializeField] RawImage image;

  public void Load()
  {
    Load(url);
  }

  private void Load(string imgUrl)
  {
    if (storage.Exists(imgUrl))
    {
      LoadFromCache(imgUrl);
    }
    else
    {
      LoadFromWeb(imgUrl);
    }
  }

  private void LoadFromCache(string imgUrl)
  {
    var texture = storage.ReadTexture2D(imgUrl);
    image.texture = texture;
  }

  private void LoadFromWeb(string imgUrl)
  {
    StartCoroutine(Download(imgUrl));
  }

  IEnumerator Download(string url)
  {
    var request = UnityWebRequestTexture.GetTexture(url);

    Debug.Log($"downloading: {url}");

    yield return request.SendWebRequest();

    if (request.result != UnityWebRequest.Result.Success)
    {
      Debug.LogError($"Download of {url} failed!");
    }
    else
    {
      var texture = DownloadHandlerTexture.GetContent(request);

      image.texture = texture;

      storage.WriteTexture2D(url, texture);
    }
  }
}
