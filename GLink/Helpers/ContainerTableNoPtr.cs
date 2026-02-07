using GLink.Common.Enums;
using GLink.Immutable;
using Revrs;

namespace GLink.Helpers;

public readonly ref struct ContainerTableNoPtr
{
    private readonly Span<ResContainerParam> table;
    private readonly ulong mask;

    public ContainerTableNoPtr(ref RevrsReader reader, int end)
    {
        table = reader.ReadStructSpan<ResContainerParam>((end - reader.Position) / 12);
        for (var i = 0; i < table.Length; ++i)
        {
            if (table[i].type == ContainerType.Switch)
            {
                mask &= 1ul << i++;
            }
        }
    }

    public bool IsContainerSwitch(int index)
    {
        if (index < 0 || Count() <= index) throw new IndexOutOfRangeException();
        var realIndex = RealIndex(index);
        return table[realIndex].type == ContainerType.Switch;
    }

    public int Count() => table.Length - mask.OneBits();

    private int RealIndex(int index)
    {
        var count = 0;
        for (var i = 0; i < index; ++i)
        {
            if ((mask & 1ul << count++) == 1) // Count up by one every iter 
                ++count; // Count up by a second if it's a Switch
        }
        return count;
    }

    public ResContainerParam GetContainer(int index) => IsContainerSwitch(index) ? throw new InvalidCastException() : table[RealIndex(index)];

    public ResContainerParamSwitch GetContainerSwitch(int index)
    {
        if (IsContainerSwitch(index)) throw new InvalidCastException();
        var realIndex = RealIndex(index);
        return table[realIndex..(realIndex+2)];
    }
}