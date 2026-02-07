using System.Runtime.InteropServices;
using GLink.Common.Structs;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversible]
[StructLayout(LayoutKind.Explicit, Size = 16)]
public partial struct ResAlwaysTrigger
{
    [FieldOffset(0)] public IntUnion guId;
    [FieldOffset(4)] private IntUnion assetCallTablePos;
    [FieldOffset(8)] public ShortUnion flag;
    [FieldOffset(10)] public ShortUnion overwriteHash; // TODO: Figure out what a ushort hash could be for
    [FieldOffset(12)] private IntUnion overwriteParamPos;

    public unsafe ResAssetCallTable AssetCall(ref Span<ResAssetCallTable> table) => table[assetCallTablePos / sizeof(ResAssetCallTable)];
    public ResTriggerOverwriteParam OverwriteParam(ref Dictionary<IntUnion, ResTriggerOverwriteParam> table) => table[overwriteParamPos];
}