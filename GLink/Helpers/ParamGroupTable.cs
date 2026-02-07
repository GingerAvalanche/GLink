using System.Runtime.InteropServices;
using GLink.Immutable;
using Revrs;

namespace GLink.Helpers;

public ref struct ParamGroupTable
{
    private Span<byte> data;

    public ParamGroupTable(ref RevrsReader reader, int end)
    {
        data = reader.Data[reader.Position..end];
        while (reader.Position < end)
        {
            // Reverse them and then dump them
            // This is really inefficient, we might as well just *start off* by parsing them as objects...
            _ = new ResArrangeParamGroup(ref reader);
        }
    }

    public unsafe ResArrangeParamGroup GetByOffset(int offset)
    {
        int count;
        fixed (byte* ptr = &data[offset])
        {
            count = *(int*)ptr;
        }

        var arrangeParams = MemoryMarshal.Cast<byte, ResArrangeParam>(data[(offset + 4)..(offset + 4 + 8 * count)]);
        return new ResArrangeParamGroup(ref arrangeParams);
    }
}