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
    [FieldOffset(4)] internal IntUnion childrenStartIndex;
    [FieldOffset(8)] internal IntUnion childrenEndIndex;
    [FieldOffset(12)] private IntUnion watchPropertyNamePos;
    [FieldOffset(16)] public IntUnion watchPropertyId; // TODO: Figure out what this is
    [FieldOffset(20)] private ShortUnion localPropertyNameIdx;
    [FieldOffset(22)] public bool isGlobal;
    [FieldOffset(23)] private byte _padding;
    
    public static unsafe implicit operator ResContainerParamSwitch(Span<ResContainerParam> cont)
    {
        ResContainerParamSwitch res;
        fixed (void* p = &cont[0])
            res = *(ResContainerParamSwitch*)p;
        return res;
    }
    
    public Span<ResAssetCallTable> AssetCalls(ref Span<ResAssetCallTable> table) => table[childrenStartIndex..childrenEndIndex];
    public string WatchPropName(StringTable table) => table.GetByOffset(watchPropertyNamePos);
    public string LocalWatchPropName(StringTable table) => table[localPropertyNameIdx];
}