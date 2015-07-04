using System;
using System.IO;

namespace UniDec
{
    class Program
    {
        static void Main(string[] args)
        {
            if(!CodecDirectoryExists())
                Environment.Exit(0);
        }

        static bool CodecDirectoryExists()
        {
            if (!Directory.Exists("./codecs"))
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
