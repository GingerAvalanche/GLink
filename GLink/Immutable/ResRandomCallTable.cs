using System.Runtime.InteropServices;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversable]
[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 8)]
public partial struct ResRandomCallTable(float min, float max)
{
    [FieldOffset(0)] public float Min = min;
    [FieldOffset(4)] public float Max = max;
}