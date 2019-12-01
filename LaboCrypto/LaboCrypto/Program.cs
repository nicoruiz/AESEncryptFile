using System;
using System.IO;
using System.Security.Cryptography;

namespace LaboCrypto
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = Path.GetRelativePath("@LaboCrypto", "..\\..\\TestFolder\\test_file.txt");
            Console.WriteLine($"Reading file: {filePath}");
            string fileText = System.IO.File.ReadAllText(filePath);
            EncryptAesManaged(fileText);
            Console.ReadLine();
        }

        static void EncryptAesManaged(string rawString)
        {
            try
            {
                using (AesManaged aes = new AesManaged())
                {
                    //Encrypt string
                    byte[] encrypted = AesEncryptor.Encrypt(rawString, aes.Key, aes.IV);
                    //Write encrypted file
                    string encryptedText = System.Text.Encoding.UTF8.GetString(encrypted);
                    Console.WriteLine("Writing encrypted file...");
                    System.IO.File.WriteAllText(Path.GetRelativePath("@LaboCrypto", "..\\..\\TestFolder\\encrypted_file.txt"), encryptedText);
                    //Write decrypted file
                    Console.WriteLine("Writing decrypted file...");
                    string decrypted = AesEncryptor.Decrypt(encrypted, aes.Key, aes.IV);
                    System.IO.File.WriteAllText(Path.GetRelativePath("@LaboCrypto", "..\\..\\TestFolder\\decrypted_file.txt"), decrypted);
                    Console.WriteLine("Done!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
