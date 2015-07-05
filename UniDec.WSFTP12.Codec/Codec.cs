using System;
using System.Security.Cryptography;
using System.Text;
using UniDecAPI;

namespace UniDec.WSFTP12.Codec
{
    public class Codec : ICodec
    {
        public string FriendlyName { get { return "WS FTP Professional version >12.0"; } }
        public string CallName { get { return "wsftp12"; } }
        public bool NeedsKey { get { return false; } }

        private readonly byte[] _wsftpkey = {
            0xE1, 0xF0, 0xC3, 0xD2, 0xA5, 0xB4, 0x87, 0x96,
            0x69, 0x78, 0x4B, 0x5A, 0x2D, 0x3C, 0x0F, 0x1E,
            0x34, 0x12, 0x78, 0x56, 0xAB, 0x90, 0xEF, 0xCD
        };

        private readonly byte[] _iv =
        {
            // Last 8 bytes of key make up the initialization vector,
            // Crafty S-O-Bs...
            0x34, 0x12, 0x78, 0x56, 0xAB, 0x90, 0xEF, 0xCD
        };

        public string Decode(string input)
        {
            var actualInput = input;
            if (input[0] == '_')
            {
                // stripping _ from beginning, it's useless and deceiving
                actualInput = input.Substring(1);
            }
            byte[] inputArray = Convert.FromBase64String(actualInput);

            var tripleDes = new TripleDESCryptoServiceProvider
            {
                Key = _wsftpkey,
                Mode = CipherMode.CBC,
                IV = _iv,
                Padding = PaddingMode.None
            };
            ICryptoTransform transform = tripleDes.CreateDecryptor();

            byte[] decodedArray = transform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            var result = Encoding.UTF8.GetString(decodedArray);

            if (result.Contains(" "))
            {
                // don't need the currently unknown value, so split
                return result.Split(' ')[0];
            }
            // probably decrypting own hash, so no split needed
            return result;
        }

        public string Encode(string input)
        {
            byte[] inputArray = Encoding.UTF8.GetBytes(input);

            var tripleDes = new TripleDESCryptoServiceProvider
            {
                Key = _wsftpkey,
                Mode = CipherMode.CBC,
                IV = _iv,
                Padding = PaddingMode.None
            };
            ICryptoTransform transform = tripleDes.CreateEncryptor();

            //FIXME: Result is shorter than normal encryption algorithm.
            //  NOTE: It's correct, though.
            byte[] encodedArray = transform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            return Convert.ToBase64String(encodedArray);
        }

        public string Decode(string input, string key)
        {
            return "No key needed.";
        }

        public string Encode(string input, string key)
        {
            return "No key needed.";
        }
    }
}
