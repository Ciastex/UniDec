using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniDecAPI;

namespace UniDec.ObcyXOR.Codec
{
    public class Codec : ICodec
    {
        public string FriendlyName => "ObcyProto XOR";
        public string CallName => "obcy_proto";
        public bool NeedsKey => false;

        public string Decode(string input)
        {
            var stringBuilder = new StringBuilder();

            foreach (var c in input)
            {
                if (c >= 65 && c <= 90)
                    stringBuilder.Append((char)(90 - (c - 65)));
                else if (c >= 97 && c <= 122)
                    stringBuilder.Append((char) (122 - (c - 97)));
                else
                    stringBuilder.Append(c);
            }
            return stringBuilder.ToString();
        }

        public string Encode(string input)
        {
            var stringBuilder = new StringBuilder();

            foreach (var c in input)
            {
                if (c >= 65 && c <= 90)
                    stringBuilder.Append((char)(90 - (c + 65)));
                else if (c >= 97 && c <= 122)
                    stringBuilder.Append((char)(122 - (c + 97)));
                else
                    stringBuilder.Append(c);
            }
            return stringBuilder.ToString();
        }

        public string Decode(string input, string key) => "No key required.";
        public string Encode(string input, string key) => "No key required.";
    }
}
