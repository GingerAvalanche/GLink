using System.Runtime.InteropServices;
using GLink.Common.Structs;
using GLink.Helpers;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversible]
[StructLayout(LayoutKind.Explicit, Size = 8)]
public partial struct ResArrangeParam
{
    [FieldOffset(0)] private IntUnion _namePos;
    [FieldOffset(4)] public byte unk4;
    [FieldOffset(5)] public byte unk5;
    [FieldOffset(6)] [DoNotReverse] private ushort _padding;

    public string Name(ref StringTable table) => table.GetByOffset(_namePos);

    public string ToString(ref StringTable table) => $"{table.GetByOffset(_namePos)}: {{ unk4: {unk4}, unk5: {unk5} }}";
}