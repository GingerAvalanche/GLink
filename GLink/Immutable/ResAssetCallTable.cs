using System.Runtime.InteropServices;
using GLink.Common.Structs;
using GLink.Helpers;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversible]
[StructLayout(LayoutKind.Explicit, Size = 32)]
public partial struct ResAssetCallTable
{
    [FieldOffset(0)] private IntUnion keyOffset;
    [FieldOffset(4)] public ShortUnion assetId;
    [FieldOffset(6)] private ShortUnion flag;
    [FieldOffset(8)] public IntUnion duration;
    [FieldOffset(12)] public IntUnion parentIndex;
    
    // WoomLink claims this is a ushort and byte, but
    // that leaves another byte that should be padding
    // but always has a unique value, so it can't be that
    [FieldOffset(16)] public IntUnion unknown;
    
    [FieldOffset(20)] private IntUnion keyNameHash;
    [FieldOffset(24)] private IntUnion paramPos;
    [FieldOffset(28)] private IntUnion conditionPos;
    
    public bool IsContainer => (flag & 1) == 1;
    
    public string Key(StringTable table) => table.GetByOffset(keyOffset);
    public string KeyFromHash(StringTable table) => table.GetByHash(keyNameHash);
    public bool IsContainerSwitch(ref ContainerTable table) => IsContainer ? table.IsContainerSwitch(paramPos) : throw new InvalidOperationException();
    public ResContainerParam Container(ref ContainerTable table) => IsContainer ? table.GetContainer(paramPos) : throw new InvalidOperationException();
    public ResContainerParamSwitch ContainerSwitch(ref ContainerTable table) => IsContainer ? table.GetContainerSwitch(paramPos) : throw new InvalidOperationException();
    public ResAssetParam Param(ref Dictionary<IntUnion, ResAssetParam> table) => !IsContainer ? table[paramPos] : throw new InvalidOperationException();
    public ResCondition Condition(ConditionTable table) => table.GetByOffset(conditionPos);
}