using System;
using System.Collections.Generic;
using System.Linq;
using UniDecAPI;

namespace UniDec
{
    class CodecExecutor
    {
        private readonly List<ICodec> _codecs;

        public CodecExecutor(List<ICodec> codecs)
        {
            _codecs = codecs;
        }

        public string ExecuteDecoder(string codecCallName, string input)
        {
            foreach (var codec in _codecs.Where(codec => codec.CallName == codecCallName))
            {
                return codec.Decode(input);
            }
            Console.WriteLine("No codec '{0}' found.", codecCallName);
            return string.Empty;
        }

        public string ExecuteEncoder(string codecCallName, string input)
        {
            foreach (var codec in _codecs.Where(codec => codec.CallName == codecCallName))
            {
                return codec.Encode(input);
            }
            Console.WriteLine("No codec '{0}' found.", codecCallName);
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
    }
}
