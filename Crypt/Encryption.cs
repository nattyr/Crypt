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
        public static byte[] Encrypt(EncryptionType encryptionType, Byte[] encryptionKey, Byte[] input)
        {
            Byte[] encryptedPL;
            switch (encryptionType)
            {
                case EncryptionType.Rijndael:
                    encryptedPL = Encryption.RijndaelEncrypt(input, encryptionKey);
                    break;
                default:
                    encryptedPL = Encryption.Xor(input, encryptionKey);
                    break;
            }

            return encryptedPL;
        }

        //Not to be used as an example of a propper Rijndael implementation
        private static byte[] RijndaelEncrypt(byte[] input, byte[] key)
        {
            RijndaelManaged rijAlg = new RijndaelManaged
            {
                Key = key,
                Mode = CipherMode.ECB
            };
            ICryptoTransform encrypter = rijAlg.CreateEncryptor();

            return encrypter.TransformFinalBlock(input, 0, input.Length);
        }

        private static byte[] Xor(byte[] input, byte[] key)
        {
            byte[] encrypted = new byte[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                encrypted[i] = (byte)(input[i] ^ key[i % key.Length]);
            }

            return encrypted;
        }

        public static byte[] GenerateKey()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] key = new byte[32];
            rng.GetBytes(key);
            return key;
        }
    }
}
