using System.Runtime.InteropServices;

namespace GLink.Common.Structs;

// Used when you really, really DGAF whether it's signed or not
[StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
public struct ConvertibleInt : IEquatable<ConvertibleInt>
{
    [field: FieldOffset(0)] public int Int { get; set; }
    [field: FieldOffset(0)] public uint UInt { get; set; }

    private ConvertibleInt(int value) => Int = value;
    private ConvertibleInt(uint value) => UInt = value;

    public static implicit operator uint(ConvertibleInt value) => value.UInt;
    public static implicit operator int(ConvertibleInt value) => value.Int;

    public static implicit operator ConvertibleInt(uint value) => new(value);
    public static implicit operator ConvertibleInt(int value) => new(value);

    public static implicit operator Index(ConvertibleInt value) => value.Int;

    public static ConvertibleInt operator ++(ConvertibleInt value) => value.Int + 1;
    public static ConvertibleInt operator --(ConvertibleInt value) => value.Int - 1;

    public static ConvertibleInt operator +(ConvertibleInt left, ConvertibleInt right) => left.UInt + right.UInt;
    public static ConvertibleInt operator -(ConvertibleInt left, ConvertibleInt right) => left.UInt - right.UInt;
    
    public static int operator +(ConvertibleInt left, int right) => left.Int + right;
    public static int operator -(ConvertibleInt left, int right) => left.Int - right;
    public static int operator *(ConvertibleInt left, int right) => left.Int * right;
    public static int operator /(ConvertibleInt left, int right) => left.Int / right;
    public static int operator %(ConvertibleInt left, int right) => left.Int % right;
    
    public static uint operator +(ConvertibleInt left, uint right) => left.UInt + right;
    public static uint operator -(ConvertibleInt left, uint right) => left.UInt - right;
    public static uint operator *(ConvertibleInt left, uint right) => left.UInt * right;
    public static uint operator /(ConvertibleInt left, uint right) => left.UInt / right;
    public static uint operator %(ConvertibleInt left, uint right) => left.UInt % right;

    public void operator +=(ConvertibleInt operand) => UInt += operand.UInt;
    public void operator -=(ConvertibleInt operand) => UInt -= operand.UInt;

    public static bool operator ==(ConvertibleInt left, ConvertibleInt right) => left.Equals(right);
    public static bool operator !=(ConvertibleInt left, ConvertibleInt right) => !(left == right);
    public bool Equals(ConvertibleInt other) => UInt == other.UInt;
    public override bool Equals(object? obj) => obj is ConvertibleInt other && Equals(other);
    public override int GetHashCode() => (int)UInt;
}