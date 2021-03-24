using PasswordManager.Database.Entities;
using PasswordManager.Extensions;
using PasswordManager.Models;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace PasswordManager.HelperClasses
{
    public static class AESPasswordEntry
    {
        public static PasswordEntry EntryModelToEntry(PasswordEntryModel entry, byte[] key)
        {
            var passwordByteArray = entry.Password.ToByteArrayFromString();
            using var AesAlg = Aes.Create();
            AesAlg.Key = key;
            AesAlg.GenerateIV();
            var encryptor = AesAlg.CreateEncryptor(key, AesAlg.IV);
            using var stream = new MemoryStream();
            using var csEncrypt = new CryptoStream(stream, encryptor, CryptoStreamMode.Write);
            csEncrypt.Write(passwordByteArray, 0, passwordByteArray.Length);
            csEncrypt.FlushFinalBlock();
            var encryptedPasswordByteArray = stream.ToArray();
            return new PasswordEntry
            {
                PasswordEntryId = entry.PasswordEntryId,
                Website = entry.Website,
                Username = entry.Username,
                IV = AesAlg.IV.ToBase64FromByteArray(),
                Password = encryptedPasswordByteArray.ToBase64FromByteArray(),
            };
        }

        public static IEnumerable<PasswordEntry> EntryModelsToEntries(IEnumerable<PasswordEntryModel> entryModels, byte[] key)
        {
            var passwordEntries = new List<PasswordEntry>();
            using var AesAlg = Aes.Create();
            AesAlg.Key = key;
            ICryptoTransform encryptor;
            byte[] passwordByteArray;
            byte[] encryptedPasswordByteArray;
            foreach (var entryModel in entryModels)
            {
                AesAlg.GenerateIV();
                encryptor = AesAlg.CreateEncryptor(key, AesAlg.IV);
                passwordByteArray = entryModel.Password.ToByteArrayFromString();
                using var stream = new MemoryStream();
                using var csEncrypt = new CryptoStream(stream, encryptor, CryptoStreamMode.Write);
                csEncrypt.Write(passwordByteArray, 0, passwordByteArray.Length);
                csEncrypt.FlushFinalBlock();
                encryptedPasswordByteArray = stream.ToArray();
                passwordEntries.Add(new PasswordEntry
                {
                    PasswordEntryId = entryModel.PasswordEntryId,
                    Website = entryModel.Website,
                    Username = entryModel.Username,
                    IV = AesAlg.IV.ToBase64FromByteArray(),
                    Password = encryptedPasswordByteArray.ToBase64FromByteArray(),
                });
            }
            return passwordEntries;
        }

        public static PasswordEntryModel EntryToEntryModel(PasswordEntry entry, byte[] key)
        {
            var passwordByteArray = entry.Password.ToByteArrayFromBase64();
            using var AesAlg = Aes.Create();
            AesAlg.Key = key;
            AesAlg.IV = entry.IV.ToByteArrayFromBase64();
            var decrypt = AesAlg.CreateDecryptor(key, AesAlg.IV);
            using var stream = new MemoryStream();
            using var csDecrypt = new CryptoStream(stream, decrypt, CryptoStreamMode.Write);
            csDecrypt.Write(passwordByteArray, 0, passwordByteArray.Length);
            csDecrypt.FlushFinalBlock();
            var decryptedPasswordByteArray = stream.ToArray();
            return new PasswordEntryModel
            {
                PasswordEntryId = entry.PasswordEntryId,
                Website = entry.Website,
                Username = entry.Username,
                Password = decryptedPasswordByteArray.ToStringFromByteArray(),
            };
        }

        public static IEnumerable<PasswordEntryModel> EntriesToEntryModels(IEnumerable<PasswordEntry> entries, byte[] key)
        {
            var passwordEntries = new List<PasswordEntryModel>();
            using var AesAlg = Aes.Create();
            AesAlg.Key = key;
            ICryptoTransform decryptor;
            byte[] passwordByteArray;
            byte[] decryptedPasswordByteArray;
            foreach (var entry in entries)
            {
                AesAlg.IV = entry.IV.ToByteArrayFromBase64();
                decryptor = AesAlg.CreateDecryptor(key, AesAlg.IV);
                passwordByteArray = entry.Password.ToByteArrayFromBase64();
                using var stream = new MemoryStream();
                var csDecrypt = new CryptoStream(stream, decryptor, CryptoStreamMode.Write);
                csDecrypt.Write(passwordByteArray, 0, passwordByteArray.Length);
                csDecrypt.FlushFinalBlock();
                decryptedPasswordByteArray = stream.ToArray();
                passwordEntries.Add(new PasswordEntryModel
                {
                    PasswordEntryId = entry.PasswordEntryId,
                    Website = entry.Website,
                    Username = entry.Username,
                    Password = decryptedPasswordByteArray.ToStringFromByteArray(),
                });
            }
            return passwordEntries;
        }
    }
}
