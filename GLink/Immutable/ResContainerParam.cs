using System.Runtime.InteropServices;
using GLink.Common.Enums;
using GLink.Common.Structs;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversible]
[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 12)]
public partial struct ResContainerParam
{
    [FieldOffset(0)] public ContainerType type;
    [FieldOffset(4)] internal ConvertibleInt childrenStartIndex;
    [FieldOffset(8)] internal ConvertibleInt childrenEndIndex;
    
    public Span<ResAssetCallTable> AssetCalls(ref Span<ResAssetCallTable> table) => table[childrenStartIndex..childrenEndIndex];
}