namespace GLink.Helpers;

public static class IntegerExtensions
{
    static int[] BitsSetTable256 = new int[256];
    static bool initialized = false;

    private static void initialize()
    {
        BitsSetTable256[0] = 0;
        for (int i = 0; i < 256; i++)
        {
            BitsSetTable256[i] = (i & 1) + BitsSetTable256[i / 2];
        }
    }

    public static int OneBits(this ulong value)
    {
        return ((uint)value).OneBits()
             + ((uint)(value >> 32)).OneBits();
    }

    public static int OneBits(this uint value)
    {
        if (!initialized) initialize();
        return BitsSetTable256[value & 0xff]
             + BitsSetTable256[(value >> 8) & 0xff]
             + BitsSetTable256[(value >> 16) & 0xff]
             + BitsSetTable256[value >> 24];
    }
}