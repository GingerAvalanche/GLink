using System.Runtime.InteropServices;
using System.Text;
using GLink.Common.Enums;
using GLink.Common.Structs;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversable]
[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 72)]
public partial struct ResourceHeader
{
    [FieldOffset(0)] private byte magic0;
    [FieldOffset(1)] private byte magic1;
    [FieldOffset(2)] private byte magic2;
    [FieldOffset(3)] private byte magic3;
    [FieldOffset(4)] public ConvertibleInt dataSize;
    [FieldOffset(8)] public XLinkVersion version;
    [FieldOffset(12)] public ConvertibleInt numResParam;
    [FieldOffset(16)] public ConvertibleInt numResAssetParam;
    [FieldOffset(20)] public ConvertibleInt numResTriggerOverwriteParam;
    [FieldOffset(24)] public ConvertibleInt triggerOverwriteParamTablePos;
    [FieldOffset(28)] public ConvertibleInt localPropertyNameRefTablePos;
    [FieldOffset(32)] public ConvertibleInt numLocalPropertyNameRefTable;
    [FieldOffset(36)] public ConvertibleInt numLocalPropertyEnumNameRefTable;
    [FieldOffset(40)] public ConvertibleInt numDirectValueTable;
    [FieldOffset(44)] public ConvertibleInt numRandomTable;
    [FieldOffset(48)] public ConvertibleInt numCurveTable;
    [FieldOffset(52)] public ConvertibleInt numCurvePointTable;
    [FieldOffset(56)] public ConvertibleInt exRegionPos;
    [FieldOffset(60)] public ConvertibleInt numUser;
    [FieldOffset(64)] public ConvertibleInt conditionTablePos;
    [FieldOffset(68)] public ConvertibleInt nameTablePos;

    public string Magic => Encoding.ASCII.GetString([magic0, magic1, magic2, magic3]);
}