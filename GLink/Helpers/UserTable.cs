using GLink.Immutable;
using Revrs;

namespace GLink.Helpers;

// I hate this. I hate everything about this
public readonly unsafe ref struct UserTable
{
    private readonly Endianness _endian;
    public Span<byte> Data { get; }
    private readonly int start;
    private readonly int numUserParam;

    public UserTable(ref RevrsReader reader, int start, int end, int numUserParam)
    {
        this.start = start;
        this.numUserParam = numUserParam;
        _endian = reader.Endianness;
        Data = reader.Data[start..end];
        reader.Position = end;
    }

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