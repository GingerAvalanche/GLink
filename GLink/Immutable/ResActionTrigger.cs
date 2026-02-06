using System.Runtime.InteropServices;
using GLink.Common.Structs;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversible]
[StructLayout(LayoutKind.Explicit, Size = 24)]
public partial struct ResActionTrigger
{
    [FieldOffset(0)] public ConvertibleInt guId;
    [FieldOffset(4)] private ConvertibleInt assetCallTablePos;
    [FieldOffset(8)] public ConvertibleInt startFrame;
    [FieldOffset(12)] public ConvertibleInt endFrame;
    [FieldOffset(16)] public ConvertibleShort flag;
    [FieldOffset(18)] public ConvertibleShort overwriteHash; // TODO: Figure out what a ushort hash could be for
    [FieldOffset(20)] private ConvertibleInt overwriteParamPos;

    public unsafe ResAssetCallTable AssetCall(ref Span<ResAssetCallTable> table) => table[assetCallTablePos / sizeof(ResAssetCallTable)];
    public ResTriggerOverwriteParam OverwriteParam(ref Dictionary<ConvertibleInt, ResTriggerOverwriteParam> table) => table[overwriteParamPos];
}