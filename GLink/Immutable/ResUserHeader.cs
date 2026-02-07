using System.Runtime.InteropServices;
using GLink.Common.Structs;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversible]
[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 48)]
public partial struct ResUserHeader
{
    [FieldOffset(0)] public IntUnion isSetup;
    [FieldOffset(4)] public IntUnion numLocalProperty;
    [FieldOffset(8)] public IntUnion numCallTable;
    [FieldOffset(12)] public IntUnion numAsset;
    [FieldOffset(16)] public IntUnion numRandomContainer2;
    [FieldOffset(20)] public IntUnion numResActionSlot;
    [FieldOffset(24)] public IntUnion numResAction;
    [FieldOffset(28)] public IntUnion numResActionTrigger;
    [FieldOffset(32)] public IntUnion numResProperty;
    [FieldOffset(36)] public IntUnion numResPropertyTrigger;
    [FieldOffset(40)] public IntUnion numResAlwaysTrigger;
    [FieldOffset(44)] public IntUnion triggerTablePos;
}