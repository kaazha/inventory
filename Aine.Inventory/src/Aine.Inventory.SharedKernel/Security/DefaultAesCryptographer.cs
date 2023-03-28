using System;
using System.IO;
using System.Security.Cryptography;
using Aine.Inventory.SharedKernel.Security.Interfaces;

namespace Aine.Inventory.SharedKernel.Security;

[Inject(InstanceScope.Singleton)]
public class DefaultAesEncrypter : IEncryptor
{
  private static readonly byte[] _key = { 199, 236, 140, 22, 102, 123, 199, 166, 234, 144, 48, 32, 59, 181, 220, 25, 120, 81, 120, 237, 85, 92, 141, 94, 93, 136, 154, 243, 161, 246, 50, 122 };
  private static readonly byte[] _iv = { 57, 115, 37, 128, 96, 155, 189, 30, 254, 213, 116, 21, 137, 148, 114, 31 };

  private static DefaultAesEncrypter? _instance;

  public static IEncryptor Instance => _instance ??= new DefaultAesEncrypter();

  public string Encrypt(string plainText)
  {
    // Check arguments.
    if (string.IsNullOrEmpty(plainText))
      throw new ArgumentNullException("plainText");

    var encrypted = Encrypt(plainText, _key, _iv);

    return GetString(encrypted);
  }

  public string Decrypt(string encryptedText)
  {
    // Check arguments.
    if (encryptedText == null || encryptedText.Length <= 0)
      throw new ArgumentNullException("cipherText");

    return Decrypt(GetBytes(encryptedText), _key, _iv);
  }

  private static byte[] GetBytes(string text) => Convert.FromBase64String(text);  // Encoding.UTF8.GetBytes(text);

  private static string GetString(byte[] bytes) => Convert.ToBase64String(bytes); // Encoding.UTF8.GetString(bytes);

  private byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
  {
    Validate(null, Key, IV);

    byte[] encrypted;

    // Create an Aes object
    // with the specified key and IV.
    using (Aes aesAlg = Aes.Create())
    {
      aesAlg.Key = Key;
      aesAlg.IV = IV;

      // Create an encryptor to perform the stream transform.
      ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

      // Create the streams used for encryption.
      using (MemoryStream msEncrypt = new MemoryStream())
      {
        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        {
          using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
          {
            //Write all data to the stream.
            swEncrypt.Write(plainText);
          }
          encrypted = msEncrypt.ToArray();
        }
      }
    }

    // Return the encrypted bytes from the memory stream.
    return encrypted;
  }

  private string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
  {
    Validate(cipherText, Key, IV);

    // Declare the string used to hold
    // the decrypted text.
    string? plaintext = null;

    // Create an Aes object
    // with the specified key and IV.
    using (Aes aesAlg = Aes.Create())
    {
      aesAlg.Key = Key;
      aesAlg.IV = IV;

      // Create a decryptor to perform the stream transform.
      ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

      // Create the streams used for decryption.
      using (MemoryStream msDecrypt = new MemoryStream(cipherText))
      {
        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
        {
          using (StreamReader srDecrypt = new StreamReader(csDecrypt))
          {

            // Read the decrypted bytes from the decrypting stream
            // and place them in a string.
            plaintext = srDecrypt.ReadToEnd();
          }
        }
      }
    }

    return plaintext;
  }

  private static void Validate(byte[]? cipherText, byte[] Key, byte[] IV)
  {
    // Check arguments.
    //if (cipherText == null || cipherText.Length <= 0)
    //    throw new ArgumentNullException("cipherText");
    if (Key == null || Key.Length <= 0)
      throw new ArgumentNullException("Key");
    if (IV == null || IV.Length <= 0)
      throw new ArgumentNullException("IV");
  }
}
