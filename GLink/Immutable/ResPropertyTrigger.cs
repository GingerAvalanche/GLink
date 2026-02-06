using System.Runtime.InteropServices;
using GLink.Common.Structs;
using GLink.Helpers;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversible]
[StructLayout(LayoutKind.Explicit, Size = 20)]
public partial struct ResPropertyTrigger
{
    [FieldOffset(0)] public ConvertibleInt guId;
    [FieldOffset(4)] private ConvertibleInt assetCallTablePos;
    [FieldOffset(8)] private ConvertibleInt condition;
    [FieldOffset(12)] public ConvertibleShort flag;
    [FieldOffset(14)] public ConvertibleShort overwriteHash; // TODO: Figure out what a ushort hash could be for
    [FieldOffset(16)] private ConvertibleInt overwriteParamPos;

    public unsafe ResAssetCallTable AssetCall(ref Span<ResAssetCallTable> table) => table[assetCallTablePos / sizeof(ResAssetCallTable)];
    public ResCondition Condition(ref ConditionTable table) => table.GetByOffset(condition);
    public ResTriggerOverwriteParam OverwriteParam(ref Dictionary<ConvertibleInt, ResTriggerOverwriteParam> table) => table[overwriteParamPos];
}