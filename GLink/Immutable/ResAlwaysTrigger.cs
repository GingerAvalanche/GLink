using System.Runtime.InteropServices;
using GLink.Common.Structs;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversable]
[StructLayout(LayoutKind.Explicit, Size = 16)]
public partial struct ResAlwaysTrigger
{
    [FieldOffset(0)] public ConvertibleInt guId;
    [FieldOffset(4)] private ConvertibleInt assetCallTablePos;
    [FieldOffset(8)] public ConvertibleShort flag;
    [FieldOffset(10)] public ConvertibleShort overwriteHash; // TODO: Figure out what a ushort hash could be for
    [FieldOffset(12)] private ConvertibleInt overwriteParamPos;

    public unsafe ResAssetCallTable AssetCall(ref Span<ResAssetCallTable> table) => table[assetCallTablePos / sizeof(ResAssetCallTable)];
    public ResTriggerOverwriteParam OverwriteParam(ref Dictionary<ConvertibleInt, ResTriggerOverwriteParam> table) => table[overwriteParamPos];
}