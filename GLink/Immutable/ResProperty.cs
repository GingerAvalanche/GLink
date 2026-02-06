using System.Runtime.InteropServices;
using GLink.Common.Structs;
using GLink.Helpers;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversible]
[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 16)]
public partial struct ResProperty
{
    [FieldOffset(0)] public ConvertibleInt watchPropertyNamePos;
    [FieldOffset(4)] public ConvertibleInt isGlobal;
    [FieldOffset(8)] public ConvertibleInt triggerStartIdx;
    [FieldOffset(12)] public ConvertibleInt triggerEndIdx;
    
    public string WatchPropName(StringTable table) => table.GetByOffset(watchPropertyNamePos);
    public Span<ResPropertyTrigger> PropertyTriggers(ref Span<ResPropertyTrigger> table) => table[triggerStartIdx..(triggerEndIdx - triggerStartIdx)];
}