using System.Runtime.InteropServices;
using GLink.Common.Enums;
using GLink.Common.Structs;
using GLink.Helpers;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversible]
[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 12)]
public partial struct ResDefaultParam
{
    [FieldOffset(0)] public IntUnion namePos;
    [FieldOffset(4)] public ParamType type;
    [FieldOffset(8)] public ParamUnion Value;

    public string GetName(StringTable table) => table.GetByOffset(namePos);
}