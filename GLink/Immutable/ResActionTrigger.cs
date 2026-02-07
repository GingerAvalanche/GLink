using System.Runtime.InteropServices;
using GLink.Common.Structs;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversible]
[StructLayout(LayoutKind.Explicit, Size = 24)]
public partial struct ResActionTrigger
{
    [FieldOffset(0)] public IntUnion guId;
    [FieldOffset(4)] private IntUnion assetCallTablePos;
    [FieldOffset(8)] public IntUnion startFrame;
    [FieldOffset(12)] public IntUnion endFrame;
    [FieldOffset(16)] public ShortUnion flag;
    [FieldOffset(18)] public ShortUnion overwriteHash; // TODO: Figure out what a ushort hash could be for
    [FieldOffset(20)] private IntUnion overwriteParamPos;

    public unsafe ResAssetCallTable AssetCall(ref Span<ResAssetCallTable> table) => table[assetCallTablePos / sizeof(ResAssetCallTable)];
    public ResTriggerOverwriteParam OverwriteParam(ref Dictionary<IntUnion, ResTriggerOverwriteParam> table) => table[overwriteParamPos];
}