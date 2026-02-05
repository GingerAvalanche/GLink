using GLink.Common.Enums;
using GLink.Immutable;
using Revrs;

namespace GLink.Helpers;

public readonly unsafe struct ContainerTable
{
    private readonly void*[] table;

    public ContainerTable(ref RevrsReader reader, int end)
    {
        var containerMax = new void*[(end - reader.Position) / 12];
        var slotsUsed = 0;
        while (reader.Position < end)
        {
            fixed (void* p = &reader.Data[reader.Position])
            {
                containerMax[slotsUsed++] = p;
            }
            var type = reader.Read<ContainerType>();
            reader.Move(type == ContainerType.Switch ? 20 : 8);
        }
        table = containerMax[..slotsUsed];
    }

    public bool IsContainerSwitch(int index) => *(int*)table[index] == 0;

    public ResContainerParam GetContainer(int index)
    {
        var container = *(ResContainerParam*)table[index];
        if (container.type is ContainerType.Switch) throw new InvalidCastException();
        if (container.type > ContainerType.Asset) ResContainerParam.Reverse(new Span<byte>(table[index], 12));
        // I'ma be mad if this check fails
        return container.type > ContainerType.Asset ? throw new InvalidOperationException() : container;
    }

    public ResContainerParamSwitch GetContainerSwitch(int index)
    {
        var container = *(ResContainerParamSwitch*)table[index];
        if (container.type is not ContainerType.Switch) throw new InvalidCastException();
        if (container.childrenEndIndex > 0xFFFF) ResContainerParamSwitch.Reverse(new Span<byte>(table[index], 24));
        // I'ma be mad if this check fails
        return container.childrenEndIndex > 0xFFFF ? throw new InvalidOperationException() : container;
    }
}