using System.Text;

namespace HGS.FileControl.StringExtension
{
  public static class StorageStringExtension
  {
    public static void WriteString(this Storage storage, string key, string data)
    {
      var bytes = Encoding.UTF8.GetBytes(data);
      storage.WriteBytes(key, bytes);
    }

    public static string ReadString(this Storage storage, string key)
    {
      var bytes = storage.ReadBytes(key);
      var content = Encoding.UTF8.GetString(bytes, 0, bytes.Length);

      return content;
    }
  }
}