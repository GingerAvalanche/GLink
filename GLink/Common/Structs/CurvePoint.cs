using Revrs.Attributes;

namespace GLink.Common.Structs;

[Reversible]
public partial struct CurvePoint(float x, float y)
{
    public float X = x;
    public float Y = y;
}