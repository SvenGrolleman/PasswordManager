using PasswordManager.Database;
using PasswordManager.Database.Entities;
using PasswordManager.Encryption;
using PasswordManager.Extensions;
using System.Collections.Generic;
using System.Configuration;

namespace PasswordManager.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["PasswordManager"].ConnectionString;
            var repository = new PwRepository(connectionString);
            var mainPw = repository.GetMainPassword();
            var rfcArray = RfcEncryptor.HashWithSalt("secret".ToByteArrayFromString(), mainPw.MainSalt.ToByteArrayFromBase64());
            var verified = ByteArrayComparer.CompareByteArrays(rfcArray, mainPw.Password.ToByteArrayFromBase64());
            var key = RfcEncryptor.HashWithSalt("secret".ToByteArrayFromString(), mainPw.MainSalt.ToByteArrayFromBase64(), 1000);
            //var IV = AESEncryptor.GetIV();
            //var encryted = AESEncryptor.EncryptStringToBytes("test".ToByteArrayFromString(), key, IV);
            //var entry = new PasswordEntry()
            //{
            //    Website = "test",
            //    Username = "test",
            //    IV = IV.ToBase64FromByteArray(),
            //    Password = encryted.ToBase64FromByteArray(),
            //};
            //repository.InsertPasswordEntry(entry);
            var entry = repository.GetPasswordEntry(2);
            var decrypt = AESEncryptor.DecryptBytesToString(entry.Password.ToByteArrayFromBase64(), key, entry.IV.ToByteArrayFromBase64()).ToStringFromByteArray();
        }
    }
}
