using System.Runtime.InteropServices;
using GLink.Common.Structs;
using GLink.Helpers;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversible]
[StructLayout(LayoutKind.Explicit, Size = 20)]
public partial struct ResCurveCallTable
{
    [FieldOffset(0)] private ShortUnion curvePointStartPos;
    [FieldOffset(2)] private ShortUnion numPoint;
    [FieldOffset(4)] public ShortUnion curveType; // TODO: This is probably an enum
    [FieldOffset(6)] public ShortUnion isPropGlobal;
    [FieldOffset(8)] private IntUnion propNameOffset;
    [FieldOffset(12)] public IntUnion propIdx; // TODO: What does this property refer to?
    [FieldOffset(16)] private ShortUnion localPropertyNameIdx;
    [FieldOffset(18)] private ShortUnion _padding;

    public Span<ResCurvePoint> GetCallPoints(Span<ResCurvePoint> points) => points[curvePointStartPos..(curvePointStartPos + numPoint)];
    public string PropName(StringTable table) => table.GetByOffset(propNameOffset);
    public string LocalPropName(StringTable table) => table[propIdx];
}