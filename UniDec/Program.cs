using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UniDec
{
    class Program
    {
        private const string CodecPath = "./codecs";

        private static CodecExecutor _codecExecutor;
        private static CodecLoader _codecLoader;

        static void Main(string[] args)
        {
            if (!CodecDirectoryExists())
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

            var usedIndexes = new List<int>();

            for (var i = 0; i < args.Length; i++)
            {
                if (_codecExecutor.CodecExists(args[i]))
                {
                    executedCallName = args[i];
                    usedIndexes.Add(i);
                    continue;
                }

                switch (args[i])
                {
                    case "dec":
                        executeEncoder = false;
                        usedIndexes.Add(i);
                        break;
                    case "enc":
                        executeEncoder = true;
                        usedIndexes.Add(i);
                        break;
                }
            }

            for (var j = 0; j < args.Length; j++)
            {
                if (usedIndexes.Contains(j))
                    continue;

                codecInput = args[j];
                break;
            }

            if (executedCallName != "")
            {
                switch (executeEncoder)
                {
                    case true:
                        Console.WriteLine(
                            _codecExecutor.ExecuteEncoder(executedCallName, codecInput)
                        );
                        break;
                    case false:
                        Console.WriteLine(
                            _codecExecutor.ExecuteDecoder(executedCallName, codecInput)
                        );
                        break;
                }
            }
            else
            {
                Console.WriteLine("Codec does not exist.");
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
