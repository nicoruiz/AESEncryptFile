using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LaboCrypto
{
    public class AesEncryptor
    {
        public static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;

            using (AesManaged aes = new AesManaged())
            {
                //Create encryptor
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                //Create memory stream
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                            encrypted = ms.ToArray();
                    }
                }
            }
            return encrypted;
        }

        public static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plainText = String.Empty;

            using (AesManaged aes = new AesManaged())
            {
                //Create decryptor
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                //Create memory stream
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                            plainText = reader.ReadToEnd();
                    }
                }
            }
            return plainText;
        }
    }
}
