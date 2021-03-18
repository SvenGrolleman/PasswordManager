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
        }
    }
}
