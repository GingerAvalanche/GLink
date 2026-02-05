using System.Runtime.InteropServices;
using GLink.Common.Enums;
using GLink.Common.Structs;
using GLink.Immutable;

namespace GLink.Mutable;

[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 8)]
public struct DefaultParam(ParamType type, ParamUnion value)
{
    [FieldOffset(0)] public ParamType Type = type;
    [FieldOffset(4)] public ParamUnion Value = value;

    public static DefaultParam FromImmutable(ResDefaultParam param) => new(param.type, param.Value);
}