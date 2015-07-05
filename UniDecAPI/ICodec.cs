namespace UniDecAPI
{
    public interface ICodec
    {
        string FriendlyName { get; }
        string CallName { get; }
        bool NeedsKey { get; }

        string Decode(string input);
        string Encode(string input);

        string Decode(string input, string key);
        string Encode(string input, string key);
    }
}
