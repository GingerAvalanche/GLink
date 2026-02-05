using GLink.Helpers;
using Revrs;

namespace GLink.Immutable;

/// <summary>
/// <para>MaskBits are the bit indices of the mask that are 1. Index into the ResParamDefinesTable
/// to find the names and types of the Params.</para>
/// <para>A given index into MaskBits corresponds to the same index into Params</para>
/// </summary>
public readonly unsafe struct ResAssetParam
{
    private readonly ulong mask;
    private readonly ResParam* paramPtr;
    private readonly int count;

    public ResAssetParam(ref XLinkReader reader)
    {
        mask = reader.Read<ulong>();
        count = mask.OneBits();
        fixed (byte* p = &reader.Data[reader.Position])
            paramPtr = (ResParam*)p;
        reader.Move(sizeof(ResParam) * count);
    }
    
    public Span<ResParam> Params => new(paramPtr, count);

    public int[] MaskBits()
    {
        var arr = new int[count];
        var moved = 0;
        for (var i = 0; i < 64; ++i)
        {
            if ((mask >> i & 1) == 1)
            {
                arr[moved++] = i;
            }

            if (moved == count) break;
        }

        return arr;
    }
}