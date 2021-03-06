﻿using System;
using UniDecAPI;

namespace UniDec.WSFTPLegacy.Codec
{
    public class Codec : ICodec
    {
        public string FriendlyName { get { return "WS FTP Legacy"; } }
        public string CallName { get { return "wsftp-le"; } }
        public bool NeedsKey { get { return false; } }

        private string _preamble = "PWD=V";
        private readonly Random _random = new Random();
        public string Decode(string input)
        {
            try
            {
                var actualInput = input;

                if (!input.Contains("PWD="))
                {
                    actualInput = "PWD=" + input;
                }
                var decodedText = "";
                var pw = actualInput.Substring(37, actualInput.Length - 37);

                for (var i = 0; i < pw.Length / 2; i++)
                {
                    var character = pw.Substring(i * 2, 2);
                    var salt = actualInput.Substring(5 + i, (6 + i) - (5 + i));
                    var decodedCharacter = Convert.ToInt32(character, 16) - i - 1 -
                                           ((47 + Convert.ToInt32(salt, 16)) % 57);
                    decodedText += (char)decodedCharacter;
                }
                return decodedText;
            }
            catch
            {
                return "Invalid string specified.";
            }
        }

        public string Encode(string input)
        {
            if (input.Length > 32)
                return "Password too long (32 characters maximum).";

            var salt = "";
            var encodedString = _preamble;

            for (var i = 0; i < 32; i++)
            {
                salt += _random.Next(0, 15).ToString("X");
            }
            encodedString += salt;

            for (var i = 0; i < input.Length; i++)
            {
                var encodedCharacter = input[i] + i + 1 + (47 + Convert.ToInt32(salt.Substring(i, 1), 16)) % 57;
                encodedString += encodedCharacter.ToString("X");
            }
            return encodedString;
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
