using GLink.Immutable;
using Revrs;

namespace GLink.Helpers;

// I hate this. I hate everything about this
public readonly ref struct UserTable(ref XLinkReader reader, int length, int numUserParam)
{
    private readonly Endianness _endian = reader.Endianness;
    public Span<byte> Data { get; } = reader.Read(length);

    public ResUserData Get(ref RevrsReader reader, int offset)
    {
        reader.Position = offset;
        reader.Endianness = _endian;
        return new ResUserData(ref reader, numUserParam);
    }
}