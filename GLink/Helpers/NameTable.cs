using System.IO.Hashing;
using System.Text;

namespace GLink.Helpers;

public class NameTable
{
    private List<string> _names;
    private List<uint> _offsets;
    private List<uint> _hashes;
    private List<int> _counts;

    public NameTable(ref XLinkReader reader)
    {
        var start = reader.Position;
        _names = [];
        _offsets = [];
        _hashes = [];
        _counts = [];
        while (true)
        {
            if (reader.Position == reader.Length) break;
            _offsets.Add((uint)(reader.Position - start));
            var name = reader.ReadNullTerminatedString();
            _names.Add(name);
            _hashes.Add(Crc32.HashToUInt32(Encoding.UTF8.GetBytes(name)));
            _counts.Add(0);
        }
    }

    public string ByIndex(int index)
    {
        ++_counts[index];
        return _names[index];
    }

    public string ByOffset(uint offset)
    {
        var index = _offsets.IndexOf(offset);
        ++_counts[index];
        return _names[index];
    }

    public string ByHash(uint hash)
    {
        var index = _hashes.IndexOf(hash);
        ++_counts[index];
        return _names[index];
    }

    public Dictionary<string, int> GetCounts() => _names.Zip(_counts).ToDictionary(x => x.First, x => x.Second);
    public string[] GetUnused() => _counts.Where(x => x == 0).Select(x => _names[x]).ToArray();
}