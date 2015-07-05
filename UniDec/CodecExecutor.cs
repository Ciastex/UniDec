using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UniDecAPI;

namespace UniDec
{
    class CodecExecutor
    {
        private const string ExceptionLogDirectory = "./exceptions";
        private readonly List<ICodec> _codecs;

        public CodecExecutor(List<ICodec> codecs)
        {
            _codecs = codecs;
        }

        public string ExecuteDecoder(string codecCallName, string input)
        {
            try
            {
                foreach (var codec in _codecs.Where(codec => codec.CallName == codecCallName))
                {
                    return codec.Decode(input);
                }
                Console.WriteLine("No codec '{0}' found.", codecCallName);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unable to decode.");
                WriteExceptionToFile(true, ex, codecCallName);
            }
            return string.Empty;
        }

        public string ExecuteEncoder(string codecCallName, string input)
        {
            try
            {
                foreach (var codec in _codecs.Where(codec => codec.CallName == codecCallName))
                {
                    return codec.Encode(input);
                }
                Console.WriteLine("No codec '{0}' found.", codecCallName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to encode.");
                WriteExceptionToFile(false, ex, codecCallName);
            }
            return string.Empty;
        }

        public List<string[]> GetCodecs()
        {
            var codecInfos = new List<string[]>();

            foreach (var codec in _codecs)
            {
                var codecInfo = new string[2];

                codecInfo[0] = codec.CallName;
                codecInfo[1] = codec.FriendlyName;

                codecInfos.Add(codecInfo);
            }
            return codecInfos;
        }

        public bool CodecExists(string codecCallName)
        {
            return _codecs.Any(codec => codec.CallName == codecCallName);
        }

        private void WriteExceptionToFile(bool decoding, Exception ex, string codecCallName)
        {
            Console.WriteLine("Check exception directory to see what caused the exception.");

            var fullyQualifiedFileName = decoding ? codecCallName + "_dec.log" : codecCallName + "_enc.log";

            using (var sw = new StreamWriter(ExceptionLogDirectory + "/" + fullyQualifiedFileName))
            {
                sw.WriteLine(ex);
            }
        }
    }
}
