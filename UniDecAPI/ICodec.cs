namespace UniDecAPI
{
    public interface ICodec
    {
        string FriendlyName { get; }
        string CallName { get; }

        string Decode(string input);
        string Encode(string input);
    }
}
