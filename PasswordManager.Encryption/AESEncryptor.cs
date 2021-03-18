using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace PasswordManager.Encryption
{
    public static class AESEncryptor
    {
        public static byte[] GetIV()
        {
            using var AesAlg = Aes.Create();
            AesAlg.GenerateIV();
            return AesAlg.IV;
        }
        public static byte[] EncryptStringToBytes(byte[] pw, byte[] key, byte[] IV)
        {
            using var AesAlg = Aes.Create();
            AesAlg.Key = key;
            AesAlg.IV = IV;
            var encryptor = AesAlg.CreateEncryptor(AesAlg.Key, AesAlg.IV);

            using var stream = new MemoryStream();
            using var csEncrypt = new CryptoStream(stream, encryptor, CryptoStreamMode.Write);
            csEncrypt.Write(pw, 0, pw.Length);
            csEncrypt.FlushFinalBlock();

            return stream.ToArray();
        }

        public static IEnumerable<byte[]> EncryptMultipleStrings(IEnumerable<(byte[] pw, byte[] IV)> toBeEncrypted, byte[] key)
        {
            var encryptedBytes = new List<byte[]>();
            using var AesAlg = Aes.Create();
            foreach (var (pw, IV) in toBeEncrypted)
            {
                using var stream = new MemoryStream();
                var encryptor = AesAlg.CreateEncryptor(key, IV);
                using var csEncrypt = new CryptoStream(stream, encryptor, CryptoStreamMode.Write);
                csEncrypt.Write(pw, 0, pw.Length);
                csEncrypt.FlushFinalBlock();
                encryptedBytes.Add(stream.ToArray());
            }

            return encryptedBytes;
        }

        public static byte[] DecryptBytesToString(byte[] toBeDecrypthed, byte[] key, byte[] IV)
        {
            using var AesAlg = Aes.Create();
            AesAlg.Key = key;
            AesAlg.IV = IV;
            var decryptor = AesAlg.CreateDecryptor(AesAlg.Key, AesAlg.IV);

            using var stream = new MemoryStream();
            using var csDecrypt = new CryptoStream(stream, decryptor, CryptoStreamMode.Write);
            csDecrypt.Write(toBeDecrypthed, 0, toBeDecrypthed.Length);
            csDecrypt.FlushFinalBlock();

            return stream.ToArray();
        }

        public static IEnumerable<byte[]> DecryptMultipleBytes(IEnumerable<(byte[] encryptedPw, byte[] IV)> toBeDecrypted, byte[] key)
        {
            var decryptedBytes = new List<byte[]>();
            using var AesAlg = Aes.Create();
            foreach (var (encryptedPw, IV) in toBeDecrypted)
            {
                using var stream = new MemoryStream();
                var decryptor = AesAlg.CreateDecryptor(key, IV);
                using var csDecrypt = new CryptoStream(stream, decryptor, CryptoStreamMode.Write);
                csDecrypt.Write(encryptedPw, 0, encryptedPw.Length);
                csDecrypt.FlushFinalBlock();
                decryptedBytes.Add(stream.ToArray());
            }

            return decryptedBytes;
        }
    }
}
