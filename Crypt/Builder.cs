using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypt
{
    class Builder
    {
        Byte[] payload;
        Options options;

        public Builder(byte[] payload, Options options)
        {
            this.payload = payload;
            this.options = options;
        }
    }
}
