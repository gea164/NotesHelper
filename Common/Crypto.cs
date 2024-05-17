using System.Security.Cryptography;
using System.Text;

namespace NotesHelper.Common
{
    internal class Crypto
    {
        const int AES_IV_LENGTH = 16;

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public static string? Encrypt(string plainText, string key)
        {
            try
            {
                byte[] encrypted;
                
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = PasswordToBytes(key);
                    aesAlg.GenerateIV();

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (var msEncrypt = new MemoryStream())
                    {
                        //The random IV will be added at the begining of the memory buffer.
                        msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                        
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (var swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plainText);
                            }
                            encrypted = msEncrypt.ToArray();
                        }
                    }
                }
                return Convert.ToBase64String(encrypted);
            }
            catch(Exception)
            {

            }
            return null;
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        public static string? Decrypt(string cipherTextInBase64, string key)
        {
            string? plaintext = null;

            try
            {
                using (var aesAlg = Aes.Create())
                {
                    byte[] cipherText = Convert.FromBase64String(cipherTextInBase64);

                    //Reading the IV from the cipher text
                    byte[] iv = new byte[AES_IV_LENGTH];
                    Array.Copy(cipherText, 0, iv, 0, iv.Length);

                    aesAlg.Key = PasswordToBytes(key);
                    aesAlg.IV = iv;

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    //Reading the rest of the memory (the message)
                    using (var msDecrypt = new MemoryStream(cipherText, iv.Length, cipherText.Length - iv.Length))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                plaintext = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return plaintext;
        }

        //---------------------------------------------------------------------
        //---------------------------------------------------------------------
        private static byte[] PasswordToBytes(string password)
        {
            return SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
