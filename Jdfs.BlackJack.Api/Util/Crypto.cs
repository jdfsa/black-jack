using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Jdfs.BlackJack.Api.Util
{
    /// <summary>
    /// Util class to encrypt the game state data
    /// </summary>
    public static class Crypto
    {
        /// <summary>
        /// Default key
        /// </summary>
        private const string key = "!BlackJackApi!";

        /// <summary>
        /// Encrypt a string
        /// </summary>
        /// <param name="source">String to be encrypted</param>
        /// <returns>String encrypted</returns>
        public static string Encrypt(string source)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(source);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// String decryption
        /// </summary>
        /// <param name="hash">Hash to be decrypted</param>
        /// <returns>Readable string</returns>
        public static string Decrypt(string hash)
        {
            hash = hash.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(hash);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    return Encoding.Unicode.GetString(ms.ToArray());
                }
            }
        }
    }
}
