using System.Runtime.InteropServices;
using GLink.Common.Structs;
using GLink.Helpers;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversible]
[StructLayout(LayoutKind.Explicit, Size = 8)]
public partial struct ResAction
{
    [FieldOffset(0)] public IntUnion namePos;
    [FieldOffset(4)] public ShortUnion triggerStartIdx;
    [FieldOffset(6)] public ShortUnion triggerEndIdx;
    
    public string Name(StringTable table) => table.GetByOffset(namePos);
    public Span<ResActionTrigger> ActionTriggers(ref Span<ResActionTrigger> table) => table[triggerStartIdx..(triggerEndIdx - triggerStartIdx)];
}