using System;
using System.IO;

namespace UniDec
{
    class Program
    {
        private const string CodecPath = "./codecs";

        private static CodecExecutor _codecExecutor;
        private static CodecLoader _codecLoader;

        static void Main(string[] args)
        {
            if(!CodecDirectoryExists())
                Environment.Exit(0);

            _codecLoader = new CodecLoader(CodecPath);
            _codecExecutor = new CodecExecutor(_codecLoader.LoadCodecs());

        }

        static bool CodecDirectoryExists()
        {
            if (!Directory.Exists(CodecPath))
            {
                Console.WriteLine("No codecs detected.");
                Console.WriteLine("Creating new folder for codecs. You need to drop codec DLLs there to use UniDec.");
                Directory.CreateDirectory("./codecs");

                return false;
            }
            return true;
        }
    }
}
