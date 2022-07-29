using System;
using HGS.LocalStorage.Utils;
using UnityEngine;

namespace HGS.LocalStorage
{
  [CreateAssetMenu(fileName = "CacheStorage", menuName = "HGS/LocalStorage/CacheStorage")]
  public class CacheStorage : Storage
  {
    [SerializeField] int expiration = 1440;

    public void Invalidate(string key)
    {
      var hash = CriptographyUtility.Hash(key);
      var path = Root + hash;
      if (FileUtility.Exists(path)) InvalidateFile(path);
    }

    private void InvalidateFile(string path)
    {
      var lastWrite = FileUtility.GetLastWriteTime(path);

      var timeDiff = DateTime.UtcNow - lastWrite;

      var result = timeDiff.CompareTo(TimeSpan.FromMinutes(expiration));

      if (result > 0)
      {
        FileUtility.Delete(path);
      }
    }

    void OnEnable()
    {
      var files = FileUtility.GetFiles(Root);
      files.ForEach(InvalidateFile);
    }

    public override bool Exists(string key)
    {
      Invalidate(key);
      return base.Exists(key);
    }
  }
}