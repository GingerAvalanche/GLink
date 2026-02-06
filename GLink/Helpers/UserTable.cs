using GLink.Immutable;
using Revrs;

namespace GLink.Helpers;

// I hate this. I hate everything about this
public readonly unsafe ref struct UserTable(ref RevrsReader reader, int start, int end, int numUserParam)
{
    private readonly Endianness _endian = reader.Endianness;
    public Span<byte> Data { get; } = reader.Data[start..end];

    public ResUserData Get(int offset)
    {
        RevrsReader reader = new RevrsReader(Data)
        {
            Position = offset - start,
            Endianness = _endian
        };
        return new ResUserData(ref reader, numUserParam);
    }
}