using System.Runtime.InteropServices;
using Revrs.Attributes;

namespace GLink.Common.Structs;

[Reversable]
[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 4)]
public partial struct ParamUnion
{
    [FieldOffset(0)] public uint UInt;
    [FieldOffset(0)] public float Float;
    [FieldOffset(0)] public bool Bool;
    [FieldOffset(0)] public uint Enum;
    [FieldOffset(0)] public uint StringOffset;
    [FieldOffset(0)] public byte Bitfield;
}