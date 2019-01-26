# UniDec
Universal password encoder/decoder. Plug-in based approach allows everyone to write their own codecs.

### Usage
```
unidec.exe [help | list] <codec> <enc | dec> <input> [-k key]
```

| Argument | Information | Notes |
| ---------| ------------|-------- |
|help| Displays usage informations | Has to be the only one parameter. |
|list| Displays a list of all usable codecs | As above. |
|codec| Specifies a codec you want to use. | This argument is required.|
|enc/dec| Tells UniDec encode/decode given text using the specified codec. | Only one of these can be specified at once.|
|input| Text you want UniDec to encode/decode. |
|-k| Tells UniDec to use the specified key for encoding/decoding the given text. | The next value immediately after this argument becomes a key. |


### Q & A
##### The following line is executed correctly, why?
```
unidec.exe codecname The enc test string -k key for encryption
```
UniDec does not care about the argument order and if you're not a fan of readable commands, it will gladly encrypt the text in a correct, just like this:
```
unidec.exe codecname enc The test string for encryption -k key 
```
The same rule applies for the decryption process, but it's very unlikely for it to work with a codec, because codecs operate on hashes (most of the time).

##### I want to write a plug-in codec, how?
Everything you need is already in the solution, but if it's unclear to you, here's the basic breakdown.
First, you have to inherit from the main ICodec interface, like this:
```C#
using UniDecAPI;

namespace MyNamespace.Example.Codec {
  // Class name doesn't have to be named like this, but it's nice to have a consistent code.
  public class Codec : ICodec {
    //...
  }
}
```

Next, you simply implement required fields and methods. Basic explanation of fields:

```C# 
public string FriendlyName { get; private set; }
```
This is the name that appears after a user specifies the **list** argument for execution. Used to provide a basic info about (for what it is and target algorithm version) your plugin.

```C#
public string CallName { get; private set; }
```
This is your codec's call name. It's not allowed to have any spaces, because this field enables a user to invoke your codec and, apparently, UniDec does not like spaces in these names.

```C#
public bool NeedsKey { get; private set; }
```
This field specifies whether or not your codec requires a key to function properly.

```C#
string Encode(string input);
string Decode(string input);
```
These methods are used to manipulate the input without the need to know a key.

```C#
string Encode(string input, string key);
string Decode(string input, string key);
```
These methods are used to manipulate the input text with the specified key.

Basic ROT-13 codec implementation example can be found [here](https://github.com/Ciastex/UniDec/blob/master/UniDec.Rot13.Codec/Codec.cs).
