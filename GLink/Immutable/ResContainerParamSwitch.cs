using System.Runtime.InteropServices;
using GLink.Common.Enums;
using GLink.Common.Structs;
using GLink.Helpers;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversable]
[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 24)]
public partial struct ResContainerParamSwitch
{
    [FieldOffset(0)] public ContainerType type;
    [FieldOffset(4)] public ConvertibleInt childrenStartIndex; // TODO: Figure out what "children" refers to
    [FieldOffset(8)] public ConvertibleInt childrenEndIndex; // TODO: Make accessor for children
    [FieldOffset(12)] public ConvertibleInt watchPropertyNamePos;
    [FieldOffset(16)] public ConvertibleInt watchPropertyId;
    [FieldOffset(20)] public ConvertibleShort localPropertyNameIdx;
    [FieldOffset(22)] public bool isGlobal;
    [FieldOffset(23)] private byte _padding;
    
    public string WatchPropName(StringTable table) => table.GetByOffset(watchPropertyNamePos);
    public string LocalWatchPropName(StringTable table) => table[localPropertyNameIdx];
}