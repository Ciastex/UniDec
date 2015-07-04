using UniDecAPI;

namespace UniDec.Rot13.Codec
{
    public class Codec : ICodec
    {
        public string FriendlyName { get { return "ROT13"; } }
        public string CallName { get { return "rot13"; } }

        public string Decode(string input)
        {
            return Transform(input);
        }

        public string Encode(string input)
        {
            return Transform(input);
        }

        private string Transform(string what)
        {
            var array = what.ToCharArray();
            for (var i = 0; i < array.Length; i++)
            {
                var number = (int)array[i];

                if (number >= 'a' && number <= 'z')
                {
                    if (number > 'm')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                else if (number >= 'A' && number <= 'Z')
                {
                    if (number > 'M')
                    {
                        number -= 13;
                    }
                    else
                    {
                        number += 13;
                    }
                }
                array[i] = (char)number;
            }
            return new string(array);
        }
    }
}
