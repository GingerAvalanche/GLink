using GLink.Common.Structs;
using Revrs;
using ResCurvePoint = GLink.Immutable.ResCurvePoint;

namespace GLink.Helpers;

public class CurvePointTable
{
    private readonly ResCurvePoint[] _curvePoints;
    private readonly uint[] _counts;

    public CurvePointTable(ref RevrsReader reader, uint count)
    {
        _curvePoints = new ResCurvePoint[count];
        for (var i = 0; i < count; ++i)
        {
            _curvePoints[i] = new ResCurvePoint(reader.Read<float>(), reader.Read<float>());
        }
        _counts = new uint[count];
    }

    public ResCurvePoint this[int index]
    {
        get
        {
            ++_counts[index];
            return _curvePoints[index];
        }
    }
    
    public uint[] GetCounts() => _counts;
    public ResCurvePoint[] GetUnused() => _counts.Where(x => x == 0).Select((_, i) => _curvePoints[i]).ToArray();
}