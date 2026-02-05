using System.Runtime.InteropServices;
using Revrs.Attributes;

namespace GLink.Common.Structs;

// Used when you really, really DGAF whether it's signed or not
[Reversable]
[StructLayout(LayoutKind.Explicit, Size = 2, Pack = 2)]
public partial struct ConvertibleShort : IEquatable<ConvertibleShort>
{
    [field: FieldOffset(0)] public short Short { get; set; }
    [field: FieldOffset(0)] [field: DoNotReverse] public ushort UShort { get; set; }

    private ConvertibleShort(short value) => Short = value;
    private ConvertibleShort(ushort value) => UShort = value;

    public static implicit operator ushort(ConvertibleShort value) => value.UShort;
    public static implicit operator short(ConvertibleShort value) => value.Short;
    public static implicit operator uint(ConvertibleShort value) => value.UShort;
    public static implicit operator int(ConvertibleShort value) => value.Short;

    public static implicit operator ConvertibleShort(ushort value) => new(value);
    public static implicit operator ConvertibleShort(short value) => new(value);
    public static implicit operator ConvertibleShort(uint value) => new((ushort)value);
    public static implicit operator ConvertibleShort(int value) => new((short)value);

    public static implicit operator Index(ConvertibleShort value) => value.Short;

    public static ConvertibleShort operator ++(ConvertibleShort value) => value.Short + 1;
    public static ConvertibleShort operator --(ConvertibleShort value) => value.Short - 1;

    public static ConvertibleShort operator +(ConvertibleShort left, ConvertibleShort right) => left.UShort + right.UShort;
    public static ConvertibleShort operator -(ConvertibleShort left, ConvertibleShort right) => left.UShort - right.UShort;
    
    public static short operator +(ConvertibleShort left, short right) => (short)(left.Short + right);
    public static short operator -(ConvertibleShort left, short right) => (short)(left.Short - right);
    public static short operator *(ConvertibleShort left, short right) => (short)(left.Short * right);
    public static short operator /(ConvertibleShort left, short right) => (short)(left.Short / right);
    public static short operator %(ConvertibleShort left, short right) => (short)(left.Short % right);
    
    public static ushort operator +(ConvertibleShort left, ushort right) => (ushort)(left.UShort + right);
    public static ushort operator -(ConvertibleShort left, ushort right) => (ushort)(left.UShort - right);
    public static ushort operator *(ConvertibleShort left, ushort right) => (ushort)(left.UShort * right);
    public static ushort operator /(ConvertibleShort left, ushort right) => (ushort)(left.UShort / right);
    public static ushort operator %(ConvertibleShort left, ushort right) => (ushort)(left.UShort % right);

    public void operator +=(ConvertibleShort operand) => UShort += operand.UShort;
    public void operator -=(ConvertibleShort operand) => UShort -= operand.UShort;

    public static bool operator ==(ConvertibleShort left, ConvertibleShort right) => left.Equals(right);
    public static bool operator !=(ConvertibleShort left, ConvertibleShort right) => !(left == right);
    public bool Equals(ConvertibleShort other) => UShort == other.UShort;
    public override bool Equals(object? obj) => obj is ConvertibleShort other && Equals(other);
    public override int GetHashCode() => UShort;
}