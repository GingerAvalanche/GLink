using System.Runtime.InteropServices;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversible]
[StructLayout(LayoutKind.Explicit, Size = 8, Pack = 4)]
public partial struct ResCurvePoint(float x, float y)
{
    [FieldOffset(0)] public float X = x;
    [FieldOffset(4)] public float Y = y;
}