using NUnit.Framework;
using PasswordManager.Encryption;
using PasswordManager.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace PasswordManager.Tests
{
    public class SymmetricalEncryptionTests
    {
        public string pw = "AnotherStrongPassword";
        public byte[] pwByteArray;

        [SetUp]
        public void Setup()
        {
            pwByteArray = pw.ToByteArrayFromString();
        }

        [Test]
        public void EncryptAndDecrypt()
        {
            var stringToBeEncrypted = "Encrypt me baby, all the way";
            var toBeEncrypted = stringToBeEncrypted.ToByteArrayFromString();
            var key = RfcEncryptor.HashWithSalt(pwByteArray, 1000);
            var myAes = Aes.Create();
            var IV = myAes.IV;
            //encrypt
            var encrypted = AESEncryptor.EncryptStringToBytes(toBeEncrypted, key, IV);
            //decrypt 
            var decryptedByteArray = AESEncryptor.DecryptBytesToString(encrypted, key, IV);
            var decryptedString = decryptedByteArray.ToStringFromByteArray();

            Assert.AreEqual(stringToBeEncrypted, decryptedString);
        }

        [Test]
        public void EncryptAndDecryptMultiplePasswords()
        {
            var tuplePwIV = new List<(byte[] pw, byte[] IV)>();
            var pwList = new List<string>();
            var key = RfcEncryptor.HashWithSalt(pwByteArray, 1000);

            for (int i = 0; i < 10; i++)
            {
                pwList.Add($"password{i}");
                tuplePwIV.Add((pw: $"password{i}".ToByteArrayFromString(), IV: Aes.Create().IV));
            }

            var encryptred = new List<byte[]>(AESEncryptor.EncryptMultipleStrings(tuplePwIV, key));

            var toBeDecrypted = new List<(byte[] encryptedPw, byte[] IV)>();
            for (int i = 0; i < 10; i++)
            {
                toBeDecrypted.Add((encryptedPw: encryptred[i], IV: tuplePwIV[i].IV));
            }

            var decryptedPw = AESEncryptor.DecryptMultipleBytes(toBeDecrypted, key).Select(b => b.ToStringFromByteArray());

            Assert.AreEqual(pwList, decryptedPw);
        }
    }
}
