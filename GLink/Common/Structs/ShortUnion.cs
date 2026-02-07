using System.Runtime.InteropServices;
using Revrs.Attributes;

namespace GLink.Common.Structs;

// Used when you really, really DGAF whether it's signed or not
[Reversible]
[StructLayout(LayoutKind.Explicit, Size = 2, Pack = 2)]
public partial struct ShortUnion : IEquatable<ShortUnion>
{
    [field: FieldOffset(0)] public short Short { get; set; }
    [field: FieldOffset(0)] [field: DoNotReverse] public ushort UShort { get; set; }

    private ShortUnion(short value) => Short = value;
    private ShortUnion(ushort value) => UShort = value;

    public static implicit operator ushort(ShortUnion value) => value.UShort;
    public static implicit operator short(ShortUnion value) => value.Short;
    public static implicit operator uint(ShortUnion value) => value.UShort;
    public static implicit operator int(ShortUnion value) => value.Short;

    public static implicit operator ShortUnion(ushort value) => new(value);
    public static implicit operator ShortUnion(short value) => new(value);
    public static implicit operator ShortUnion(uint value) => new((ushort)value);
    public static implicit operator ShortUnion(int value) => new((short)value);

    public static implicit operator Index(ShortUnion value) => value.Short;

    public static ShortUnion operator ++(ShortUnion value) => value.Short + 1;
    public static ShortUnion operator --(ShortUnion value) => value.Short - 1;

    public static ShortUnion operator +(ShortUnion left, ShortUnion right) => left.UShort + right.UShort;
    public static ShortUnion operator -(ShortUnion left, ShortUnion right) => left.UShort - right.UShort;
    
    public static short operator +(ShortUnion left, short right) => (short)(left.Short + right);
    public static short operator -(ShortUnion left, short right) => (short)(left.Short - right);
    public static short operator *(ShortUnion left, short right) => (short)(left.Short * right);
    public static short operator /(ShortUnion left, short right) => (short)(left.Short / right);
    public static short operator %(ShortUnion left, short right) => (short)(left.Short % right);
    
    public static ushort operator +(ShortUnion left, ushort right) => (ushort)(left.UShort + right);
    public static ushort operator -(ShortUnion left, ushort right) => (ushort)(left.UShort - right);
    public static ushort operator *(ShortUnion left, ushort right) => (ushort)(left.UShort * right);
    public static ushort operator /(ShortUnion left, ushort right) => (ushort)(left.UShort / right);
    public static ushort operator %(ShortUnion left, ushort right) => (ushort)(left.UShort % right);

    public void operator +=(ShortUnion operand) => UShort += operand.UShort;
    public void operator -=(ShortUnion operand) => UShort -= operand.UShort;

    public static bool operator ==(ShortUnion left, ShortUnion right) => left.Equals(right);
    public static bool operator !=(ShortUnion left, ShortUnion right) => !(left == right);
    public bool Equals(ShortUnion other) => UShort == other.UShort;
    public override bool Equals(object? obj) => obj is ShortUnion other && Equals(other);
    public override int GetHashCode() => UShort;
}