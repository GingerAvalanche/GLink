using Revrs.Attributes;

namespace GLink.Common.Structs;

[Reversable]
public partial struct CurvePoint(float x, float y)
{
    public float X = x;
    public float Y = y;
}