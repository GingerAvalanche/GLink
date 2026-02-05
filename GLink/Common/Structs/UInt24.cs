using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace GLink.Common.Structs;

[CLSCompliant( isCompliant: false )]
[StructLayout( LayoutKind.Sequential )]
public readonly struct UInt24 : IEquatable<UInt24>, IComparable<UInt24>
{
    public static readonly UInt24 MaxValue = new( 0xFF, 0xFF, 0xFF );
    public static readonly UInt24 MinValue = new( 0x00, 0x00, 0x00 );

    private const UInt32 _maxValue = 0x00FFFFFF;

    //
    
    public static implicit operator UInt32( UInt24 self ) => self.Value;
    public static implicit operator UInt24( UInt16 u16  ) => new( value: u16 );

    //
    
    private readonly Byte b0;
    private readonly Byte b1;
    private readonly Byte b2;
    
    public UInt24( Byte b0, Byte b1, Byte b2 )
    {
        this.b0 = b0;
        this.b1 = b1;
        this.b2 = b2;
    }
    
    public UInt24( UInt32 value )
    {
        if( value > _maxValue ) throw new ArgumentOutOfRangeException( paramName: nameof(value), actualValue: value, message: $"Value {value:N0} must be between 0 and {_maxValue:N0} (inclusive)." );
        
        //
        
        b0 = (Byte)( ( value       ) & 0xFF );
        b1 = (Byte)( ( value >>  8 ) & 0xFF ); 
        b2 = (Byte)( ( value >> 16 ) & 0xFF );
    }

#if UNSAFE
    public unsafe Byte* Byte0 => &_b0;
#endif

    private Int32  SignedValue => ( b0 | ( b1 << 8 ) | ( b2 << 16 ) );
    public  UInt32 Value       => (UInt32)SignedValue;

    //
    
#region Struct Tedium + IEquatable<UInt24> + IComparable<UInt24>

    public override String  ToString   ()                                  => Value.ToString();
    public override Int32   GetHashCode()                                  => SignedValue;
    public override Boolean Equals     ( [NotNullWhen(true)] Object? obj ) => obj is UInt24 other && Equals( other: other );
    
    public          Boolean Equals     ( UInt24 other )                    => Value.Equals   ( other.Value );
    public          Int32   CompareTo  ( UInt24 other )                    => Value.CompareTo( other.Value );
    
    public static   Int32   Compare    ( UInt24 left, UInt24 right )       => left.Value.CompareTo( right.Value );
    
    //
    
    public static Boolean operator==( UInt24 left, UInt24 right ) =>  left.Equals( right );
    public static Boolean operator!=( UInt24 left, UInt24 right ) => !left.Equals( right );
    
    public static Boolean operator> ( UInt24 left, UInt24 right ) => Compare( left, right ) >  0;
    public static Boolean operator>=( UInt24 left, UInt24 right ) => Compare( left, right ) >= 0;
    
    public static Boolean operator< ( UInt24 left, UInt24 right ) => Compare( left, right ) <  0;
    public static Boolean operator<=( UInt24 left, UInt24 right ) => Compare( left, right ) <= 0;
    
#endregion
}