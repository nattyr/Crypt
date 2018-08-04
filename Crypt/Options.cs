using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypt
{
    public struct Options
    {
        public EncryptionType encryptionType;
        public Byte[] encryptionKey;
    }

    public enum EncryptionType
    {
        Rijndael,
        XOR
    }
}
