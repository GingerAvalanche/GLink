using System.Runtime.InteropServices;
using GLink.Common.Structs;
using GLink.Helpers;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversable]
[StructLayout(LayoutKind.Explicit, Size = 20)]
public partial struct ResCurveCallTable
{
    [FieldOffset(0)] public ConvertibleShort curvePointStartPos;
    [FieldOffset(2)] public ConvertibleShort numPoint;
    [FieldOffset(4)] public ConvertibleShort curveType;
    [FieldOffset(6)] public ConvertibleShort isPropGlobal;
    [FieldOffset(8)] public ConvertibleInt propNameOffset;
    [FieldOffset(12)] public ConvertibleInt propIdx;
    [FieldOffset(16)] public ConvertibleShort localPropertyNameIdx;
    [FieldOffset(18)] private ConvertibleShort _padding;

    public Span<CurvePoint> GetCallPoints(Span<CurvePoint> points) => points[curvePointStartPos..(curvePointStartPos + numPoint)];
    public string PropName(StringTable table) => table.GetByOffset(propNameOffset);
    public string LocalPropName(StringTable table) => table[propIdx];
}