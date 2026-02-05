using System.Runtime.InteropServices;
using GLink.Common.Enums;
using GLink.Common.Structs;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversable]
[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 12)]
public partial struct ResContainerParam
{
    [FieldOffset(0)] public ContainerType type;
    [FieldOffset(4)] public ConvertibleInt childrenStartIndex; // TODO: Figure out what "children" refers to
    [FieldOffset(8)] public ConvertibleInt childrenEndIndex; // TODO: Make accessor for children
}