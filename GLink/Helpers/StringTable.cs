using System.IO.Hashing;
using System.Text;
using GLink.Common.Structs;

namespace GLink.Helpers;

public ref struct StringTable(Span<byte> table)
{
    private Span<byte> _table = table;

    public string this[int index]
    {
        get
        {
            if (Count(0) <= index)
            {
                throw new IndexOutOfRangeException();
            }

            ConvertibleInt offset = 0;
            var count = 0;
            while (count < index)
            {
                if (_table[offset++] == 0)
                {
                    ++count;
                }
            }

            return GetByOffset(offset);
        }
    }

    public int Count(byte value) => _table.Count(value);

    public string GetByOffset(ConvertibleInt offset)
    {
        if (offset < 0 || offset >= _table.Length) { throw new IndexOutOfRangeException(); }
        if (offset == 0) return Encoding.UTF8.GetString(_table[.._table.IndexOf((byte)0)]);
        if (_table[offset - 1] != 0)
        {
            throw new ArgumentException("Offset not at beginning of string");
        }

        var end = offset + _table[offset..].IndexOf((byte)0);
        return Encoding.UTF8.GetString(_table[offset..end]);
    }

    public string GetByHash(uint hash)
    {
        var total = Count(0);
        var offset = 0;
        var count = 0;
        while (count < total)
        {
            if (_table[offset++] != 0) continue;
            ++count;
            var end = offset + _table[offset..].IndexOf((byte)0);
            var possible = _table[offset..end];
            if (Crc32.HashToUInt32(possible) == hash)
            {
                return Encoding.UTF8.GetString(possible);
            }
        }

        throw new ArgumentException("Hash not found");
    }
}