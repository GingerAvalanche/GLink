using GLink.Common.Structs;

namespace GLink.Helpers;

public readonly ref struct StringRefTable(Span<IntUnion> refs)
{
    private readonly Span<IntUnion> _refs = refs;

    public string Get(StringTable table, int index)
    {
        var @ref = _refs[index];
        return table.GetByOffset(@ref);
    }
}