using System.Runtime.InteropServices;
using GLink.Common.Enums;
using Revrs;
using Revrs.Attributes;

namespace GLink.Immutable;

[StructLayout(LayoutKind.Explicit, Size = 20)]
public readonly struct ResCondition
{
    [FieldOffset(0)]
    public readonly ContainerType Type;
    [FieldOffset(18)]
    private readonly byte isSolved;
    [FieldOffset(19)]
    private readonly byte isGlobal;

    [field: FieldOffset(4)] public float Weight => IsRandom() ? field : throw new InvalidOperationException("Only Randoms have Weight");
    [field: FieldOffset(4)] public PropertyType PropertyType => !IsRandom() ? field : throw new InvalidOperationException("Randoms do not have PropertyType");
    [field: FieldOffset(8)] public CompareType CompareType => !IsRandom() ? field : throw new InvalidOperationException("Randoms do not have CompareType");
    [field: FieldOffset(12)] public int Value => !IsRandom() ? field : throw new InvalidOperationException("Randoms do not have Value");
    [field: FieldOffset(16)] public short LocalPropertyEnumNameIdx => !IsRandom() ? field : throw new InvalidOperationException("Randoms do not have LocalPropertyEnumNameIdx");
    public bool IsSolved => !IsRandom() ? isSolved == 1 : throw new InvalidOperationException("Randoms do not have IsSolved");
    public bool IsGlobal => !IsRandom() ? isGlobal == 1 : throw new InvalidOperationException("Randoms do not have IsGlobal");

    public ResCondition(ref RevrsReader reader)
    {
        Type = (ContainerType)reader.Read<uint>();
        if (Type is ContainerType.Random or ContainerType.Random2)
        {
            Weight = reader.Read<float>();
        }
        else
        {
            PropertyType = reader.Read<PropertyType>();
            CompareType = reader.Read<CompareType>();
            Value = reader.Read<int>();
            LocalPropertyEnumNameIdx = reader.Read<short>();
            isSolved = reader.Read<byte>();
            isGlobal = reader.Read<byte>();
        }
    }

    public ResCondition(ContainerType randomType, float weight)
    {
        if (randomType is not (ContainerType.Random or ContainerType.Random2))
            throw new ArgumentException($"{nameof(randomType)} must be a random type");
        Type = randomType;
        Weight = weight;
    }

    public ResCondition(ContainerType nonRandomType, PropertyType propertyType, CompareType compareType, int value,
        short localPropertyEnumNameIdx, bool isSolved, bool isGlobal)
    {
        if (nonRandomType is ContainerType.Random or ContainerType.Random2)
            throw new ArgumentException($"{nameof(nonRandomType)} must not be a random type");
        Type = nonRandomType;
        PropertyType = propertyType;
        CompareType = compareType;
        Value = value;
        LocalPropertyEnumNameIdx = localPropertyEnumNameIdx;
        unsafe { this.isSolved = *(byte*)&isSolved; }
        unsafe { this.isGlobal = *(byte*)&isGlobal; }
    }
    
    public bool IsRandom() => Type is ContainerType.Random or ContainerType.Random2;
}