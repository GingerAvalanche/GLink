using System.Runtime.InteropServices;
using GLink.Common.Enums;
using GLink.Common.Structs;
using GLink.Helpers;
using Revrs.Attributes;

namespace GLink.Immutable;

[Reversable]
[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 4)]
public partial struct ResParam
{
    [FieldOffset(0)] private uint value;

    public ReferenceType Type => (ReferenceType)(value >> 24);
    private ConvertibleInt Offset => value & 0x00FFFFFF;

    /// <summary>
    /// <para>Once you have the ParamValueUnion, use its index in the Span{ResParam}, count that many 1s into the mask,
    /// then match the mask index to the ParamDefinesTable to figure out which type to parse it as</para>
    ///
    /// <para>If you're coming from a userParam: Span{ResParam} then just use the index inside userParam</para>
    /// </summary>
    public ParamValueUnion Direct(ref Span<ParamValueUnion> table) => Type == ReferenceType.Direct ? table[Offset] : throw new InvalidCastException();
    public string String(ref StringTable table) => Type == ReferenceType.String ? table.GetByOffset(Offset) : throw new InvalidCastException();
    // TODO: Curve
    public ResRandomCallTable Random(ref Span<ResRandomCallTable> table) => Type == ReferenceType.Random ? table[Offset] : throw new InvalidCastException();
    // TODO: ArrangeParam
    public byte Bitfield() => Type == ReferenceType.Bitfield ? (byte)(Offset & 0xFF) : throw new InvalidCastException();
}