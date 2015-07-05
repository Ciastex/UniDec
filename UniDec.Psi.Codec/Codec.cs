using System;
using System.Text;
using UniDecAPI;

namespace UniDec.Psi.Codec
{
    public class Codec : ICodec
    {
        public string FriendlyName { get { return "PSI/PSI+ Instant Messenger"; } }
        public string CallName { get { return "psi-im"; } }
        public bool NeedsKey { get { return true; } }


        public string Decode(string input)
        {
            return "Please specify the key.";
        }

        public string Encode(string input)
        {
            return "Please specify the key.";
        }

        public string Decode(string input, string key)
        {
            var decodedPassword = new StringBuilder();

            if (input.Length % 4 != 0)
                return "Encoded password is invalid.";

            if (key.Length == 0)
                return input;

            int passwordIndex;
            var keyIndex = 0;

            for (passwordIndex = 0; passwordIndex < input.Length; passwordIndex += 4)
            {
                var x = input.Substring(passwordIndex, 4);
                var z = Convert.ToInt32(x, 16);

                var c = (char)(z ^ key[keyIndex++]);
                decodedPassword.Append(c);

                if (keyIndex >= key.Length)
                {
                    keyIndex = 0;
                }
            }
            return decodedPassword.ToString();
        }

        public string Encode(string input, string key)
        {
            var encodedPassword = string.Empty;

            int passwordIndex;
            var keyIndex = 0;

            if (key.Length == 0)
                return input;

            for (passwordIndex = 0; passwordIndex < input.Length; ++passwordIndex)
            {
                var x = input[passwordIndex] ^ key[keyIndex++];
                var result = x.ToString("X4");

                encodedPassword += result;

                if (keyIndex >= key.Length)
                {
                    keyIndex = 0;
                }
            }
            return encodedPassword;
        }
    }
}
