using NUnit.Framework;
using PasswordManager.Encryption;
using PasswordManager.Extensions;
using PasswordManager.HelperClasses;
using PasswordManager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace PasswordManager.Tests
{
    public class SymmetricalEncryptionTests
    {
        public string pw = "AnotherStrongPassword";
        public byte[] pwByteArray;
        public byte[] key;

        [SetUp]
        public void Setup()
        {
            pwByteArray = pw.ToByteArrayFromString();
            key = RfcEncryptor.HashWithSalt(pwByteArray, 1000);
        }

        [Test]
        public void EncryptAndDecrypt()
        {
            var stringToBeEncrypted = "Encrypt me baby, all the way";
            var toBeEncrypted = stringToBeEncrypted.ToByteArrayFromString();            
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

        [Test]
        public void EncryptModelToEntry()
        {
            
            var entryModel = new PasswordEntryModel
            {
                PasswordEntryId = 1,
                Website = "website",
                Username = "username",
                Password = "password",
            };
            var entry = AESPasswordEntry.EntryModelToEntry(entryModel, key);
            var returnEntryModel = AESPasswordEntry.EntryToEntryModel(entry, key);
            Assert.AreEqual(entryModel.Password, returnEntryModel.Password);
        }

        [Test]
        public void EncryptModelsToEntries()
        {
            var entriesModels = new List<PasswordEntryModel>()
            {
                new PasswordEntryModel
                {
                   PasswordEntryId = 1,
                    Website = "websiteOne",
                    Username = "usernameOne",
                    Password = "passwordOne",
                },
                new PasswordEntryModel
                {
                   PasswordEntryId = 2,
                    Website = "websiteTwo",
                    Username = "usernameTwo",
                    Password = "passwordTwo",
                },
                new PasswordEntryModel
                {
                   PasswordEntryId = 3,
                    Website = "websiteThree",
                    Username = "usernameThree",
                    Password = "passwordThree",
                },
                new PasswordEntryModel
                {
                   PasswordEntryId = 4,
                    Website = "websiteFour",
                    Username = "usernameFour",
                    Password = "passwordFour",
                },
            };
            var entries = AESPasswordEntry.EntryModelsToEntries(entriesModels, key);
            var returnEntryModels = new List<PasswordEntryModel>(AESPasswordEntry.EntriesToEntryModels(entries, key));
            for (int i = 0; i < returnEntryModels.Count; i++)
            {
                Assert.AreEqual(entriesModels[i].Password, returnEntryModels[i].Password);
            }
        }

    }
}
