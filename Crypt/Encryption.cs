using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Crypt
{
    static class Encryption
    {
        static byte[] RijndaelEncrypt(byte[] input, byte[] key)
        {
            RijndaelManaged rijAlg = new RijndaelManaged
            {
                Key = key,
                Mode = CipherMode.ECB
            };
            ICryptoTransform encrypter = rijAlg.CreateEncryptor();

            return encrypter.TransformFinalBlock(input, 0, input.Length);
        }

        static byte[] Xor(byte[] input, byte[] key)
        {
            byte[] encrypted = new byte[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                encrypted[i] = (byte)(input[i] ^ key[i % key.Length]);
            }

            return encrypted;
        }
    }
}
