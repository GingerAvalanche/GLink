using System.Runtime.InteropServices;
using GLink.Common.Enums;
using GLink.Common.Structs;
using GLink.Helpers;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversable]
[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 32)]
public partial struct ResAssetCallTable
{
    [FieldOffset(0)] private ConvertibleInt keyOffset;
    [FieldOffset(4)] public ConvertibleShort assetId;
    [FieldOffset(6)] private ConvertibleShort flag;
    [FieldOffset(8)] public ConvertibleInt duration;
    [FieldOffset(12)] public ConvertibleInt parentIndex;
    
    // WoomLink claims this is a ushort and byte, but
    // that leaves another byte that should be padding
    // but always has a unique value, so it can't be that
    [FieldOffset(16)] public ConvertibleInt unknown;
    
    [FieldOffset(20)] private ConvertibleInt keyNameHash;
    [FieldOffset(24)] private ConvertibleInt paramPos;
    [FieldOffset(28)] private ConvertibleInt conditionPos;
    
    public bool IsContainer => (flag & 1) == 1;
    
    public string Key(StringTable table) => table.GetByOffset(keyOffset);
    public string KeyFromHash(StringTable table) => table.GetByHash(keyNameHash);
    public bool IsContainerSwitch(ref ContainerTable table) => IsContainer ? table.IsContainerSwitch(paramPos) : throw new InvalidOperationException();
    public ResContainerParam Container(ref ContainerTable table) => IsContainer ? table.GetContainer(paramPos) : throw new InvalidOperationException();
    public ResContainerParamSwitch ContainerSwitch(ref ContainerTable table) => IsContainer ? table.GetContainerSwitch(paramPos) : throw new InvalidOperationException();
    public ResAssetParam Param(ref Dictionary<ConvertibleInt, ResAssetParam> table) => !IsContainer ? table[paramPos] : throw new InvalidOperationException();
    public ResCondition Condition(ConditionTable table) => table.GetByOffset(conditionPos);
}