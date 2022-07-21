using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.Profiling;

namespace HGS.FileControl.Utils
{
  public static class CriptographyUtility
  {
    // This constant is used to determine the keysize of the encryption algorithm in bits.
    // We divide this by 8 within the code below to get the equivalent number of bytes.
    private static int Keysize = 256;

    // This constant determines the number of iterations for the password bytes generation function.
    private static int DerivationIterations = 1000;

    public static byte[] Hash(byte[] input)
    {
      using (SHA256 sha256Hash = SHA256.Create())
      {
        return sha256Hash.ComputeHash(input);
      }
    }

    public static string Hash(string input)
    {
      var inputBytes = Encoding.UTF8.GetBytes(input);
      var hashBytes = Hash(inputBytes);

      var builder = new StringBuilder();

      for (int i = 0; i < hashBytes.Length; i++)
      {
        builder.Append(hashBytes[i].ToString("x2"));
      }

      return builder.ToString();
    }

    public static byte[] Encrypt(byte[] bytes, string passPhrase)
    {
      var saltBytes = Generate256BitsOfRandomEntropy();
      var ivBytes = Generate256BitsOfRandomEntropy();

      using (var password = new Rfc2898DeriveBytes(passPhrase, saltBytes, DerivationIterations))
      {
        var keyBytes = password.GetBytes(Keysize / 8);
        using (var symmetricKey = new RijndaelManaged())
        {
          symmetricKey.BlockSize = 256;
          symmetricKey.Mode = CipherMode.CBC;
          symmetricKey.Padding = PaddingMode.PKCS7;
          using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivBytes))
          {
            using (var memoryStream = new MemoryStream())
            {
              using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
              {
                cryptoStream.Write(bytes, 0, bytes.Length);
                cryptoStream.FlushFinalBlock();
                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                var cipherBytes = saltBytes;
                cipherBytes = cipherBytes.Concat(ivBytes).ToArray();
                cipherBytes = cipherBytes.Concat(memoryStream.ToArray()).ToArray();
                memoryStream.Close();
                cryptoStream.Close();
                return cipherBytes;
              }
            }
          }
        }
      }
    }

    public static byte[] Decrypt(byte[] bytes, string passPhrase)
    {
      Profiler.BeginSample("Decrypt");

      var saltBytes = bytes
        .Take(Keysize / 8)
        .ToArray();

      var ivBytes = bytes
        .Skip(Keysize / 8)
        .Take(Keysize / 8)
        .ToArray();

      var cipherBytes = bytes
        .Skip((Keysize / 8) * 2)
        .Take(bytes.Length - ((Keysize / 8) * 2))
        .ToArray();

      using (var password = new Rfc2898DeriveBytes(passPhrase, saltBytes, DerivationIterations))
      {
        var keyBytes = password.GetBytes(Keysize / 8);
        using (var symmetricKey = new RijndaelManaged())
        {
          symmetricKey.BlockSize = 256;
          symmetricKey.Mode = CipherMode.CBC;
          symmetricKey.Padding = PaddingMode.PKCS7;
          using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivBytes))
          {
            using (var encryptedStream = new MemoryStream(cipherBytes))
            {
              using (var decryptedStream = new MemoryStream())
              {
                using (var cryptoStream = new CryptoStream(encryptedStream, decryptor, CryptoStreamMode.Read))
                {
                  cryptoStream.CopyTo(decryptedStream);
                  Profiler.EndSample();
                  return decryptedStream.ToArray();
                }
              }
            }
          }
        }
      }
    }

    private static byte[] Generate256BitsOfRandomEntropy()
    {
      var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
      using (var rngCsp = new RNGCryptoServiceProvider())
      {
        // Fill the array with cryptographically secure random bytes.
        rngCsp.GetBytes(randomBytes);
      }
      return randomBytes;
    }
  }
}