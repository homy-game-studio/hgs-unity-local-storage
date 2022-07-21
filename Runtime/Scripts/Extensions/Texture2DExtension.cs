using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;

namespace HGS.FileControl.Texture2DExtension
{
  public static class StorageTexture2DExtension
  {
    private static Texture2D BytesToTexture2D(byte[] bytes)
    {
      Profiler.BeginSample("BytesTOTexture2D");
      var formatBytes = bytes
        .Take(sizeof(int))
        .ToArray();

      var widthBytes = bytes
        .Skip(sizeof(int))
        .Take(sizeof(int))
        .ToArray();

      var heightBytes = bytes
        .Skip(sizeof(int) * 2)
        .Take(sizeof(int))
        .ToArray();

      var textureBytes = bytes
        .Skip(sizeof(int) * 3)
        .Take(bytes.Length - (sizeof(int) * 3))
        .ToArray();

      var format = (TextureFormat)BitConverter.ToInt32(formatBytes, 0);
      var width = BitConverter.ToInt32(widthBytes, 0);
      var height = BitConverter.ToInt32(heightBytes, 0);

      var texture = new Texture2D(width, height, format, false);
      texture.LoadRawTextureData(bytes);
      texture.Apply();

      Profiler.EndSample();

      return texture;
    }

    private static byte[] Texture2DToBytes(Texture2D texture)
    {
      Profiler.BeginSample("Texture2DToBytes");
      var formatBytes = BitConverter.GetBytes((int)texture.format);
      var widthBytes = BitConverter.GetBytes(texture.width);
      var heightBytes = BitConverter.GetBytes(texture.height);
      var bytes = texture.GetRawTextureData();

      var result = formatBytes
        .Concat(widthBytes)
        .Concat(heightBytes)
        .Concat(bytes)
        .ToArray();

      Profiler.EndSample();
      return result;
    }

    public static void WriteTexture2D(this Storage storage, string key, Texture2D texture)
    {
      var bytes = Texture2DToBytes(texture);
      storage.WriteBytes(key, bytes);
    }

    public static Texture2D ReadTexture2D(this Storage storage, string key)
    {
      var bytes = storage.ReadBytes(key);
      return BytesToTexture2D(bytes);
    }
  }
}