using System.Runtime.InteropServices;
using System.Text;
using GLink.Common.Enums;
using GLink.Common.Structs;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversible]
[StructLayout(LayoutKind.Explicit, Size = 72)]
public partial struct ResourceHeader
{
    [FieldOffset(0)] private byte magic0;
    [FieldOffset(1)] private byte magic1;
    [FieldOffset(2)] private byte magic2;
    [FieldOffset(3)] private byte magic3;
    [FieldOffset(4)] public IntUnion dataSize;
    [FieldOffset(8)] public XLinkVersion version;
    [FieldOffset(12)] public IntUnion numResParam;
    [FieldOffset(16)] public IntUnion numResAssetParam;
    [FieldOffset(20)] public IntUnion numResTriggerOverwriteParam;
    [FieldOffset(24)] public IntUnion triggerOverwriteParamTablePos;
    [FieldOffset(28)] public IntUnion localPropertyNameRefTablePos;
    [FieldOffset(32)] public IntUnion numLocalPropertyNameRefTable;
    [FieldOffset(36)] public IntUnion numLocalPropertyEnumNameRefTable;
    [FieldOffset(40)] public IntUnion numDirectValueTable;
    [FieldOffset(44)] public IntUnion numRandomTable;
    [FieldOffset(48)] public IntUnion numCurveTable;
    [FieldOffset(52)] public IntUnion numCurvePointTable;
    [FieldOffset(56)] public IntUnion exRegionPos;
    [FieldOffset(60)] public IntUnion numUser;
    [FieldOffset(64)] public IntUnion conditionTablePos;
    [FieldOffset(68)] public IntUnion nameTablePos;

    public string Magic => Encoding.ASCII.GetString([magic0, magic1, magic2, magic3]);
}