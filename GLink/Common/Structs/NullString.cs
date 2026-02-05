using GLink.Helpers;

namespace GLink.Common.Structs;

public readonly ref struct NullString
{
    private readonly Span<char> _str;

    public NullString(ref XLinkReader reader)
    {
        _str = reader.ReadUntil<char>(0);
        reader.Move(1);
    }
    
    public override string ToString() => _str.ToString();
}