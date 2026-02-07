using System.Runtime.InteropServices;
using Revrs.Attributes;

namespace GLink.Common.Structs;

// Used when you really, really DGAF whether it's signed or not
[Reversible]
[StructLayout(LayoutKind.Explicit, Size = 4, Pack = 4)]
public partial struct IntUnion : IEquatable<IntUnion>
{
    [field: FieldOffset(0)] public int Int { get; set; }
    [field: FieldOffset(0)] [field: DoNotReverse] public uint UInt { get; set; }

    private IntUnion(int value) => Int = value;
    private IntUnion(uint value) => UInt = value;

    public static implicit operator uint(IntUnion value) => value.UInt;
    public static implicit operator int(IntUnion value) => value.Int;

    public static implicit operator IntUnion(uint value) => new(value);
    public static implicit operator IntUnion(int value) => new(value);

    public static implicit operator Index(IntUnion value) => value.Int;

    public static IntUnion operator ++(IntUnion value) => value.Int + 1;
    public static IntUnion operator --(IntUnion value) => value.Int - 1;

    public static IntUnion operator +(IntUnion left, IntUnion right) => left.UInt + right.UInt;
    public static IntUnion operator -(IntUnion left, IntUnion right) => left.UInt - right.UInt;
    
    public static int operator +(IntUnion left, int right) => left.Int + right;
    public static int operator -(IntUnion left, int right) => left.Int - right;
    public static int operator *(IntUnion left, int right) => left.Int * right;
    public static int operator /(IntUnion left, int right) => left.Int / right;
    public static int operator %(IntUnion left, int right) => left.Int % right;
    
    public static uint operator +(IntUnion left, uint right) => left.UInt + right;
    public static uint operator -(IntUnion left, uint right) => left.UInt - right;
    public static uint operator *(IntUnion left, uint right) => left.UInt * right;
    public static uint operator /(IntUnion left, uint right) => left.UInt / right;
    public static uint operator %(IntUnion left, uint right) => left.UInt % right;

    public void operator +=(IntUnion operand) => UInt += operand.UInt;
    public void operator -=(IntUnion operand) => UInt -= operand.UInt;

    public static bool operator ==(IntUnion left, IntUnion right) => left.Equals(right);
    public static bool operator !=(IntUnion left, IntUnion right) => !(left == right);
    public bool Equals(IntUnion other) => UInt == other.UInt;
    public override bool Equals(object? obj) => obj is IntUnion other && Equals(other);
    public override int GetHashCode() => (int)UInt;
}