using System.Runtime.InteropServices;
using Revrs.Attributes;

namespace GLink.Common.Structs;

[Reversible]
[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 4)]
public partial struct ParamUnion
{
    [FieldOffset(0)] public uint UInt;
    [FieldOffset(0)] [field: DoNotReverse] public float Float;
    [FieldOffset(0)] [field: DoNotReverse] public bool Bool;
    [FieldOffset(0)] [field: DoNotReverse] public uint Enum;
    [FieldOffset(0)] [field: DoNotReverse] public uint StringOffset;
    [FieldOffset(0)] [field: DoNotReverse] public byte Bitfield;
}