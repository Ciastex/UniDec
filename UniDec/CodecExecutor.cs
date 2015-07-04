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

        public bool CodecExists(string codecCallName)
        {
            return _codecs.Any(codec => codec.CallName == codecCallName);
        }
    }
}
