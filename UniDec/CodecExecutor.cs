using System;
using System.Collections.Generic;
using System.Linq;
using UniDecAPI;

namespace UniDec
{
    class CodecExecutor
    {
        private List<ICodec> _codecs;
 
        public CodecExecutor(List<ICodec> codecs)
        {
            _codecs = codecs;
        }

        public void ExecuteDecoder(string codecCallName, string input)
        {
            foreach (var codec in _codecs.Where(codec => codec.CallName == codecCallName))
            {
                codec.Decode(input);
                return;
            }
            Console.WriteLine("No codec '{0}' found.", codecCallName);
        }

        public void ExecuteEncoder(string codecCallName, string input)
        {
            foreach (var codec in _codecs.Where(codec => codec.CallName == codecCallName))
            {
                codec.Encode(input);
                return;
            }
            Console.WriteLine("No codec '{0}' found.", codecCallName);
        }
    }
}
