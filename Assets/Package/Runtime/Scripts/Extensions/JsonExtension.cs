using HGS.LocalStorage.StringExtension;
using Newtonsoft.Json;

namespace HGS.LocalStorage.JsonExtension
{
  public static class StorageStringExtension
  {
    public static void WriteObject<T>(this Storage storage, string key, T data)
    {
      var content = JsonConvert.SerializeObject(data);
      storage.WriteString(key, content);
    }

    public static T ReadObject<T>(this Storage storage, string key)
    {
      var raw = storage.ReadString(key);
      var content = JsonConvert.DeserializeObject<T>(raw);
      return content;
    }
  }
}