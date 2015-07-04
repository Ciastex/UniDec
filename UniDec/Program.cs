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

            if (args.Length > 3)
            {
                Console.WriteLine("Invalid usage.");
                return;
            }

            _codecLoader = new CodecLoader(CodecPath);

            _codecExecutor = new CodecExecutor(
                _codecLoader.LoadCodecs()
            );

            var executedCallName = "";
            var executeEncoder = false;
            var codecInput = "";

            var codecCallNameFound = false;
            var codecTypeDetermined = false;

            foreach (var arg in args)
            {
                if (_codecExecutor.CodecExists(arg))
                {
                    executedCallName = arg;
                    codecCallNameFound = true;
                    continue;
                }

                switch (arg)
                {
                    case "dec":
                        executeEncoder = false;
                        codecTypeDetermined = true;
                        break;
                    case "enc":
                        executeEncoder = true;
                        codecTypeDetermined = true;
                        break;
                }

                if (codecCallNameFound && codecTypeDetermined)
                    codecInput = arg;
            }

            if (executedCallName != "")
            {
                switch (executeEncoder)
                {
                    case true:
                        _codecExecutor.ExecuteEncoder(executedCallName, codecInput);
                        break;
                    case false:
                        _codecExecutor.ExecuteDecoder(executedCallName, codecInput);
                        break;
                }
            }
            else
            {
                Console.WriteLine("No such codec found.");
            }
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
