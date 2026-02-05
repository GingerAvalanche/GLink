using System.Runtime.InteropServices;
using GLink.Common.Structs;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversable]
[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 48)]
public partial struct ResUserHeader
{
    [FieldOffset(0)] public ConvertibleInt isSetup;
    [FieldOffset(4)] public ConvertibleInt numLocalProperty;
    [FieldOffset(8)] public ConvertibleInt numCallTable;
    [FieldOffset(12)] public ConvertibleInt numAsset;
    [FieldOffset(16)] public ConvertibleInt numRandomContainer2;
    [FieldOffset(20)] public ConvertibleInt numResActionSlot;
    [FieldOffset(24)] public ConvertibleInt numResAction;
    [FieldOffset(28)] public ConvertibleInt numResActionTrigger;
    [FieldOffset(32)] public ConvertibleInt numResProperty;
    [FieldOffset(36)] public ConvertibleInt numResPropertyTrigger;
    [FieldOffset(40)] public ConvertibleInt numResAlwaysTrigger;
    [FieldOffset(44)] public ConvertibleInt triggerTablePos;
}