using System;
using System.Security.Cryptography;

namespace PasswordManager.Encryption
{
    public static class RfcEncryptor
    {
        private const int _saltLength = 32;
        private const int _iterations = 10000;

        public static byte[] GenerateSalt()
        {
            using var rng = new RNGCryptoServiceProvider();
            var number = new byte[_saltLength];
            rng.GetBytes(number);
            return number;
        }

        private static byte[] Combine(byte[] lh, byte[] rh)
        {
            var retByteArray = new byte[lh.Length + rh.Length];
            Buffer.BlockCopy(lh, 0, retByteArray, 0, lh.Length);
            Buffer.BlockCopy(rh, 0, retByteArray, lh.Length, rh.Length);
            return retByteArray;
        }

        public static byte[] HashWithSalt(byte[] toBehashed)
        {
            using var rfc = new Rfc2898DeriveBytes(toBehashed, GenerateSalt(), _iterations, HashAlgorithmName.SHA512);
            return rfc.GetBytes(32);
        }

        public static byte[] HashWithSalt(byte[] toBehashed, int iterations)
        {
            using var rfc = new Rfc2898DeriveBytes(toBehashed, GenerateSalt(), iterations, HashAlgorithmName.SHA512);
            return rfc.GetBytes(32);
        }

        public static byte[] HashWithSalt(byte[] toBeHashed, byte[] salt)
        {
            using var rfc = new Rfc2898DeriveBytes(toBeHashed, salt, _iterations, HashAlgorithmName.SHA512);
            return rfc.GetBytes(32);
        }

        public static byte[] HashWithSalt(byte[] toBeHashed, byte[] salt, int iterations)
        {
            using var rfc = new Rfc2898DeriveBytes(toBeHashed, salt, iterations, HashAlgorithmName.SHA512);
            return rfc.GetBytes(32);
        }
    }
}
