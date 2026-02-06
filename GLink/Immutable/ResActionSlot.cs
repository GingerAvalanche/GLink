using System.Runtime.InteropServices;
using GLink.Common.Structs;
using GLink.Helpers;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversible]
[StructLayout(LayoutKind.Explicit, Size = 8)]
public partial struct ResActionSlot
{
    [FieldOffset(0)] public ConvertibleInt namePos;
    [FieldOffset(4)] public ConvertibleShort actionStartIdx;
    [FieldOffset(6)] public ConvertibleShort actionEndIdx;
    
    public string Name(StringTable table) => table.GetByOffset(namePos);
    
    public Span<ResAction> Actions(ref Span<ResAction> table)
    {
        return table[actionStartIdx..(actionEndIdx - actionStartIdx)];
    }
}