using System.Runtime.InteropServices;
using Revrs.Attributes;

namespace GLink.Common.Structs;

/// <summary>
/// <para>Once you have the ParamValueUnion, use its index in the Span{ResParam}, count that many 1s into the mask,
/// then match the mask index to the ParamDefinesTable to figure out which type to parse it as</para>
///
/// <para>If you're coming from a userParam: Span{ResParam} then just use the index inside userParam</para>
/// </summary>
[Reversable]
[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 4)]
public partial struct ParamValueUnion
{
    [field: FieldOffset(0)] public uint UInt;
    [field: FieldOffset(0)] [field: DoNotReverse] public float Float;
    [field: FieldOffset(0)] [field: DoNotReverse] public int Bool;
    [field: FieldOffset(0)] [field: DoNotReverse] public int Enum;
    //[field: FieldOffset(0)] public int StringOffset; // handled by ResParam.String()
    //[field: FieldOffset(0)] public int BitField; // handled by ResParam.Bitfield()
}