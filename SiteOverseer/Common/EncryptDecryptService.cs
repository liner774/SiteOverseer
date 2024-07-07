using System.Security.Cryptography;
using System.Text;

namespace SiteOverseer.Common.EncryptDecryptService
{
    public class EncryptDecryptService
    {
            private readonly string key = "tkbH1omfiqg13aqVusoCialf7pE6whfU";
            private readonly byte[] iv = new byte[16];

            public string EncryptString(string plainInput)
            {
           
                byte[] array;
                using var aes = Aes.Create();

                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using var memoryStream = new MemoryStream();
                using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                using (var streamWriter = new StreamWriter(cryptoStream))
                {
                    streamWriter.Write(plainInput);
                }

                array = memoryStream.ToArray();


                return Convert.ToBase64String(array);
            }

            public string DecryptString(string cipherText)
            {
           
                var buffer = Convert.FromBase64String(cipherText);

                using var aes = Aes.Create();
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using var memoryStream = new MemoryStream(buffer);
                using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                using var streamReader = new StreamReader(cryptoStream);

                return streamReader.ReadToEnd();
            }
    }
}

