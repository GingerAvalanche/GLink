using GLink.Common.Structs;

namespace GLink.Helpers;

public class CurvePointTable
{
    private readonly CurvePoint[] _curvePoints;
    private readonly uint[] _counts;

    public CurvePointTable(ref XLinkReader reader, uint count)
    {
        _curvePoints = new CurvePoint[count];
        for (var i = 0; i < count; ++i)
        {
            _curvePoints[i] = new CurvePoint(reader.Read<float>(), reader.Read<float>());
        }
        _counts = new uint[count];
    }

    public CurvePoint this[int index]
    {
        get
        {
            ++_counts[index];
            return _curvePoints[index];
        }
    }
    
    public uint[] GetCounts() => _counts;
    public CurvePoint[] GetUnused() => _counts.Where(x => x == 0).Select((_, i) => _curvePoints[i]).ToArray();
}