using GLink.Immutable;
using Revrs;

namespace GLink.Helpers;

public ref struct CurveTable
{
    private readonly Span<ResCurveCallTable> _curves;
    private uint[] _counts;

    public CurveTable(ref RevrsReader reader, int count)
    {
        _curves = reader.ReadStructSpan<ResCurveCallTable>(count);
        _counts = new uint[count];
    }
}