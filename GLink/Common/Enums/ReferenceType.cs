namespace GLink.Common.Enums;

public enum ReferenceType : byte
{
    Direct,                           // index into the DirectValueTable
    String,                           // offset into the NameTable
    Curve,                            // index into the ResCurveCallTable
    Random,                           // index into the ResRandomCallTable
    ArrangeParam,                     // offset relative to the start of ExRegion?
    Bitfield,
    RandomPowHalf2,
    RandomPowHalf3,
    RandomPowHalf4,
    RandomPowHalf1Point5,
    RandomPow2,
    RandomPow3,
    RandomPow4,
    RandomPow1Point5,
    RandomPowComplement2,
    RandomPowComplement3,
    RandomPowComplement4,
    RandomPowComplement1Point5,
}