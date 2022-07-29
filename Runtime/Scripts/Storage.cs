using HGS.LocalStorage.Utils;
using UnityEngine;
using UnityEngine.Profiling;

namespace HGS.LocalStorage
{
  public abstract class Storage : ScriptableObject
  {
    [SerializeField] bool useCrypto = true;
    [SerializeField] string passPhrase = "your-key-here";
    [SerializeField] string folder = "data";

    protected string Root => @$"{Application.persistentDataPath}/{folder}/";
    protected string PassPhrase => passPhrase;

    public virtual bool Exists(string key)
    {
      var hash = CriptographyUtility.Hash(key);
      var path = Root + hash;
      return FileUtility.Exists(path);
    }

    public void Clear()
    {
      FileUtility
        .GetFiles(Root)
        .ForEach(FileUtility.Delete);
    }

    public virtual byte[] ReadBytes(string key)
    {
      Profiler.BeginSample("Storage Read");
      var hash = CriptographyUtility.Hash(key);

      if (!Exists(key)) return null;

      var path = Root + hash;
      var output = FileUtility.Read(path);
      if (useCrypto) output = CriptographyUtility.Decrypt(output, passPhrase);

      Profiler.EndSample();

      return output;
    }

    public virtual void WriteBytes(string key, byte[] bytes)
    {
      var hash = CriptographyUtility.Hash(key);

      var input = useCrypto
          ? CriptographyUtility.Encrypt(bytes, passPhrase)
          : bytes;

      FileUtility.Write(Root, hash, input);
    }
  }
}