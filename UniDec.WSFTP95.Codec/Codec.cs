using System;
using UniDecAPI;

namespace UniDec.WSFTP95.Codec
{
    public class Codec : ICodec
    {
        public string FriendlyName { get { return "WS FTP 95"; } }
        public string CallName { get { return "wsftp95"; } }
        public bool NeedsKey { get { return false; } }

        public string Decode(string input)
        {
            if (input.Length % 2 != 0)
                return "Invalid string specified.";

            var decodedText = string.Empty;

            for (var i = 0; i < input.Length / 2; i++)
            {
                var character = input.Substring(i * 2, 2);
                var decodedCharacter = Convert.ToInt32(character, 16) - i;
                decodedText += (char)decodedCharacter;
            }
            return decodedText;
        }

        public string Encode(string input)
        {
            var encodedText = string.Empty;

            for (var i = 0; i < input.Length / 2; i++)
            {
                var character = input.Substring(i * 2, 2);
                var encodedCharacter = Convert.ToInt32(character, 16) + i;
                encodedText += (char)encodedCharacter;
            }
            return encodedText;
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
