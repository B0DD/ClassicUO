using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClassicUO.Assets
{
    public class CryptLoader
    {

        private byte[] KeyBytes;
        private byte[] IVBytes = new byte[16];

        public CryptLoader(string key)
        {
            Key = key;
            KeyBytes = Encoding.UTF8.GetBytes(key);
        }

        public CryptLoader()
        {
            KeyBytes = Encoding.UTF8.GetBytes(Key);
        }

        public string Key { get; private set; } = "BODDCIAOCIAOCIAOCIAOCIAO";

        public void EncryptFileSimple(string inputFile, string outputFile)
        {
            using (FileStream inputFileStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            {
                using (FileStream outputFileStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                {
                    using (Aes aes = Aes.Create())
                    {
                        aes.Key = KeyBytes;
                        aes.IV = IVBytes;

                        using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            inputFileStream.CopyTo(cryptoStream);
                        }
                    }
                }
            }
        }

        public FileStream DecryptFileSimpleToStream(string inputFile)
        {
            using (FileStream inputFileStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = KeyBytes;
                    aes.IV = IVBytes;

                    var Crypto = new CryptoStream(inputFileStream, aes.CreateDecryptor(), CryptoStreamMode.Read);

                    FileStream fs = new FileStream("temp", FileMode.Create, FileAccess.Write);
                    Crypto.CopyTo(fs);
                    return fs;

                }
            }
        }

        public void DecryptFileSimple(string inputFile, string outputFile)
        {
            using (FileStream inputFileStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            {
                using (FileStream outputFileStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                {
                    using (Aes aes = Aes.Create())
                    {
                        aes.Key = KeyBytes;
                        aes.IV = IVBytes;

                        using (CryptoStream cryptoStream = new CryptoStream(inputFileStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            cryptoStream.CopyTo(outputFileStream);
                        }
                    }
                }
            }
        }

        public string CreateSha256(FileStream fio)
        {

            using (SHA256 sha256 = SHA256.Create())
            {
                var shaResult = sha256.ComputeHash(fio);
                return BitConverter.ToString(shaResult).Replace("-", "");
            }
        }







    }



}
