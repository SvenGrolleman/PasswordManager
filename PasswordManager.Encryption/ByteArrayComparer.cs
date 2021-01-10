using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Encryption
{
    public static class ByteArrayComparer
    {
        //Always use the loop to not leak any timings
        public static bool CompareByteArrays(ReadOnlySpan<byte> lh, ReadOnlySpan<byte> rh)
        {
            bool f = lh.Length == rh.Length;
            for (int i = 0; i < lh.Length && i < rh.Length; ++i)
            {
                f &= lh[i] == rh[i];
            }
            return f;
        }
    }
}
