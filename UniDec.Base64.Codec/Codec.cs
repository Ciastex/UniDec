using System;
using System.Text;
using UniDecAPI;

namespace UniDec.Base64.Codec
{
    public class Codec : ICodec
    {
        public string FriendlyName { get { return "Base64 Codec"; } }
        public string CallName { get { return "base64"; } }
        public bool NeedsKey { get { return false; }}

        public string Decode(string input)
        {
            try
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(input));
            }
            catch
            {
                return "Invalid Base64 string.";
            }
        }

        public string Encode(string input)
        {
            try
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
            }
            catch
            {
                return "Couldn't encode input to a Base64 string.";
            }
        }

        public string Decode(string input, string key)
        {
            return "No key required.";
        }

        public string Encode(string input, string key)
        {
            return "No key required.";
        }
    }
}
