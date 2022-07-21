using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.Profiling;

namespace HGS.FileControl.Utils
{
  public static class FileUtility
  {
    public static bool Exists(string path)
    {
      return File.Exists(path);
    }

    public static void Clear(string path)
    {
      if (!Exists(path)) return;
      File.Delete(path);
    }

    public static DateTime GetLastWriteTime(string path)
    {
      return File.GetLastWriteTimeUtc(path);
    }

    public static List<string> GetFiles(string folder)
    {
      return Directory.GetFiles(folder).ToList();
    }

    public static void Delete(string path)
    {
      File.Delete(path);
    }

    public static void Write(string folder, string fileName, byte[] content)
    {
      if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

      File.WriteAllBytes(folder + fileName, content);
    }

    public static byte[] Read(string path)
    {
      Profiler.BeginSample("FileUtility Read");
      if (!Exists(path)) return null;
      var content = File.ReadAllBytes(path);
      Profiler.EndSample();

      return content;
    }
  }
}