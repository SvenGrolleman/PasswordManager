using System;
using System.Text;

namespace PasswordManager.Extensions
{
    public static class ByteArrayExtensions
    {
        public static string ToBase64FromByteArray(this byte[] array)
        {
            return Convert.ToBase64String(array);
        }

        public static byte[] ToByteArrayFromBase64(this string base64)
        {
            return Convert.FromBase64String(base64);
        }

        public static byte[] ToByteArrayFromString(this string normalString)
        {
            return Encoding.UTF8.GetBytes(normalString);
        }

        public static string ToStringFromByteArray(this byte[] array)
        {
            return Encoding.UTF8.GetString(array);
        }
    }

}
