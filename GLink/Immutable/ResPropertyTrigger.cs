using System.Runtime.InteropServices;
using GLink.Common.Structs;
using GLink.Helpers;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversible]
[StructLayout(LayoutKind.Explicit, Size = 20)]
public partial struct ResPropertyTrigger
{
    [FieldOffset(0)] public IntUnion guId;
    [FieldOffset(4)] private IntUnion assetCallTablePos;
    [FieldOffset(8)] private IntUnion condition;
    [FieldOffset(12)] public ShortUnion flag;
    [FieldOffset(14)] public ShortUnion overwriteHash; // TODO: Figure out what a ushort hash could be for
    [FieldOffset(16)] private IntUnion overwriteParamPos;

    public unsafe ResAssetCallTable AssetCall(ref Span<ResAssetCallTable> table) => table[assetCallTablePos / sizeof(ResAssetCallTable)];
    public ResCondition Condition(ref ConditionTable table) => table.GetByOffset(condition);
    public ResTriggerOverwriteParam OverwriteParam(ref Dictionary<IntUnion, ResTriggerOverwriteParam> table) => table[overwriteParamPos];
}