using NUnit.Framework;
using PasswordManager.Encryption;
using PasswordManager.Extensions;

namespace PasswordManager.Tests
{
    public class EncryptionTests
    {
        public string base64Salt = "m8JxT9dOH0bPJT1NeBr9sN88zJFr04D9VgkIpXMZdtA=";
        public string base64Hash = "rQJdaZtvDmiaZtJqjzIEDkojF4GG1LtBSbbeRwDH0q4=";

        public byte[] Salt;
        public byte[] PwHash;

        [SetUp]
        public void Setup()
        {
            Salt = base64Salt.ToByteArrayFromBase64();
            PwHash = base64Hash.ToByteArrayFromBase64();
        }

        [Test]
        public void RFCWithCorrectPassword()
        {
            var pw = "testPassword";
            var pwByteArray = pw.ToByteArrayFromString();
            var hash = RfcEncryptor.HashWithSalt(pwByteArray, Salt);

            Assert.IsTrue(ByteArrayComparer.CompareByteArrays(hash, PwHash));
        }

        [Test]
        public void RFCWithIncorrectPassword()
        {
            var pw = "wrongtestpassword";
            var pwByteArray = pw.ToByteArrayFromString();
            var hash = RfcEncryptor.HashWithSalt(pwByteArray, Salt);

            Assert.IsFalse(ByteArrayComparer.CompareByteArrays(hash, PwHash));
        }

        [Test]
        public void RFCIterationSummation()
        {
            var pwByteArray = "superpassword".ToByteArrayFromString();
            var salt = RfcEncryptor.GenerateSalt();
            var hashIterions1000 = RfcEncryptor.HashWithSalt(pwByteArray, salt, 1000);
            var hashIterations9000 = RfcEncryptor.HashWithSalt(hashIterions1000, salt, 9000);
            var hashIterations10000 = RfcEncryptor.HashWithSalt(pwByteArray, salt, 10000);

            Assert.IsFalse(ByteArrayComparer.CompareByteArrays(hashIterations10000, hashIterations9000));
        }
    }
}