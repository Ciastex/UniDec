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

            _codecLoader = new CodecLoader(CodecPath);

            _codecExecutor = new CodecExecutor(
                _codecLoader.LoadCodecs()
            );

            if (args.Length == 0)
            {
                PrintUsage();
                return;
            }

            if (args.Length < 3)
            {
                if (args[0] == "list")
                {
                    foreach (var codecInfo in _codecExecutor.GetCodecs())
                    {
                        Console.WriteLine("Codec '{0}': '{1}' - {2}", codecInfo[1], codecInfo[0], codecInfo[2]);
                    }
                    return;
                }
                
                if (args[0] == "help")
                {
                    PrintUsage();
                    return;
                }

                PrintUsage();
                return;
            }

            var executedCallName = "";
            var executeEncoder = false;
            var codecKey = string.Empty;

            var codecCallNameDetermined = false;
            var codecTypeDetermined = false;
            var nextArgIsKey = false;
            var keyDetermined = false;

            var usedIndexes = new List<int>();

            for (var i = 0; i < args.Length; i++)
            {
                if (nextArgIsKey)
                {
                    codecKey = args[i];
                    nextArgIsKey = false;
                    usedIndexes.Add(i);
                    continue;
                }

                if (_codecExecutor.CodecExists(args[i]) && !codecCallNameDetermined)
                {
                    executedCallName = args[i];
                    usedIndexes.Add(i);
                    codecCallNameDetermined = true;
                    continue;
                }

                if (!codecTypeDetermined)
                {
                    switch (args[i])
                    {
                        case "dec":
                            usedIndexes.Add(i);
                            codecTypeDetermined = true;
                            break;
                        case "enc":
                            executeEncoder = true;
                            usedIndexes.Add(i);
                            codecTypeDetermined = true;
                            break;
                    }
                }

                if (args[i] == "-k" && !keyDetermined)
                {
                    nextArgIsKey = true;
                    keyDetermined = true;

                    usedIndexes.Add(i);
                }
            }

            var inputStrings = args.Where((t, j) => !usedIndexes.Contains(j)).ToArray();
            var codecInput = string.Join(" ", inputStrings);

            if (executedCallName != "")
            {
                switch (executeEncoder)
                {
                    case true:
                        if (codecKey != string.Empty)
                        {
                            Console.WriteLine(
                                _codecExecutor.ExecuteEncoder(executedCallName, codecInput, codecKey)
                            );
                        }
                        else
                        {
                            Console.WriteLine(
                                _codecExecutor.ExecuteEncoder(executedCallName, codecInput)
                            );
                        }
                        break;
                    case false:
                        if (codecKey != string.Empty)
                        {
                            Console.WriteLine(
                                _codecExecutor.ExecuteDecoder(executedCallName, codecInput, codecKey)
                            );
                        }
                        else
                        {
                            Console.WriteLine(
                                _codecExecutor.ExecuteDecoder(executedCallName, codecInput)
                            );
                        }
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

        static void PrintUsage()
        {
            Console.WriteLine("Usage: [help | list] | <codec call name> <enc | dec> <input> [-k KEY]");
            Console.WriteLine("    help: this message");
            Console.WriteLine("    list: List all available plugins (format: Codec 'FRIENDLY_NAME': CALL_NAME)");
            Console.WriteLine("    -k KEY: specify KEY for encoding/decoding");
        }
    }
}
