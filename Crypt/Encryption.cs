using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Crypt
{
    public static class Encryption
    {
        public static byte[] RijndaelEncrypt(byte[] input, byte[] key)
        {
            RijndaelManaged rijAlg = new RijndaelManaged
            {
                Key = key,
                Mode = CipherMode.ECB
            };
            ICryptoTransform encrypter = rijAlg.CreateEncryptor();

            return encrypter.TransformFinalBlock(input, 0, input.Length);
        }
    }
}
