using System.Runtime.InteropServices;
using GLink.Common.Enums;
using GLink.Common.Structs;
using GLink.Helpers;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversible]
[StructLayout(LayoutKind.Explicit, Size = 24)]
public partial struct ResContainerParamSwitch
{
    [FieldOffset(0)] public ContainerType type;
    [FieldOffset(4)] internal ConvertibleInt childrenStartIndex;
    [FieldOffset(8)] internal ConvertibleInt childrenEndIndex;
    [FieldOffset(12)] private ConvertibleInt watchPropertyNamePos;
    [FieldOffset(16)] public ConvertibleInt watchPropertyId; // TODO: Figure out what this is
    [FieldOffset(20)] private ConvertibleShort localPropertyNameIdx;
    [FieldOffset(22)] public bool isGlobal;
    [FieldOffset(23)] private byte _padding;
    
    public Span<ResAssetCallTable> AssetCalls(ref Span<ResAssetCallTable> table) => table[childrenStartIndex..childrenEndIndex];
    public string WatchPropName(StringTable table) => table.GetByOffset(watchPropertyNamePos);
    public string LocalWatchPropName(StringTable table) => table[localPropertyNameIdx];
}