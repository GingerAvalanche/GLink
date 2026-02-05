using Revrs;

namespace GLink.Helpers;

public ref struct XLinkReader(ref RevrsReader reader)
{
    private RevrsReader _reader = reader;
    
    public readonly Span<byte> Data => _reader.Data;
    public Endianness Endianness => _reader.Endianness;
    public int Position => _reader.Position;
    public readonly int Length => _reader.Length;
    public void Seek(int position) => _reader.Seek(position);
    public void Move(int size) => _reader.Move(size);
    public void Align(int alignment) => _reader.Align(alignment);
    public void AlignDown(int alignment) => _reader.AlignDown(alignment);
    public Span<byte> Read(int length) => _reader.Read(length);
    public Span<byte> Read(int length, int offset) => _reader.Read(length, offset);
    public T Read<T>() where T : unmanaged => _reader.Read<T>();
    public void Reverse<T>() where T : unmanaged => _reader.Reverse<T>();
    public T Read<T>(int offset) where T : unmanaged => _reader.Read<T>(offset);
    public void Reverse<T>(int offset) where T : unmanaged => _reader.Reverse<T>(offset);
    public T Read<T, TReverser>() where T : unmanaged where TReverser : IStructReverser => _reader.Read<T, TReverser>();
    public T ReadStruct<T>() where T : unmanaged, IStructReverser => _reader.ReadStruct<T>();
    public void Reverse<T, TReverser>() where T : unmanaged where TReverser : IStructReverser => _reader.Reverse<T, TReverser>();
    public void ReverseStruct<T>() where T : unmanaged, IStructReverser => _reader.ReverseStruct<T>();
    public T Read<T, TReverser>(int offset) where T : unmanaged where TReverser : IStructReverser => _reader.Read<T, TReverser>(offset);
    public T ReadStruct<T>(int offset) where T : unmanaged, IStructReverser => _reader.ReadStruct<T>(offset);
    public void Reverse<T, TReverser>(int offset) where T : unmanaged where TReverser : IStructReverser => _reader.Reverse<T, TReverser>(offset);
    public void ReverseStruct<T>(int offset) where T : unmanaged, IStructReverser => _reader.ReverseStruct<T>(offset);
    public Span<T> ReadSpan<T>(int count) where T : unmanaged => _reader.ReadSpan<T>(count);
    public void ReverseSpan<T>(int count) where T : unmanaged => _reader.ReverseSpan<T>(count);
    public Span<T> ReadSpan<T>(int count, int offset) where T : unmanaged => _reader.ReadSpan<T>(count, offset);
    public void ReverseSpan<T>(int count, int offset) where T : unmanaged => _reader.ReverseSpan<T>(count, offset);
    public Span<T> ReadSpan<T, TReverser>(int count) where T : unmanaged where TReverser : IStructReverser => _reader.ReadSpan<T, TReverser>(count);
    public Span<T> ReadStructSpan<T>(int count) where T : unmanaged, IStructReverser => _reader.ReadStructSpan<T>(count);
    public void ReverseSpan<T, TReverser>(int count) where T : unmanaged where TReverser : IStructReverser => _reader.ReverseSpan<T, TReverser>(count);
    public void ReverseStructSpan<T>(int count) where T : unmanaged, IStructReverser => _reader.ReverseStructSpan<T>(count);
    public Span<T> ReadSpan<T, TReverser>(int count, int offset) where T : unmanaged where TReverser : IStructReverser => _reader.ReadSpan<T, TReverser>(count, offset);
    public Span<T> ReadStructSpan<T>(int count, int offset) where T : unmanaged, IStructReverser => _reader.ReadStructSpan<T>(count, offset);
    public void ReverseSpan<T, TReverser>(int count, int offset) where T : unmanaged where TReverser : IStructReverser => _reader.ReverseSpan<T, TReverser>(count, offset);
    public void ReverseStructSpan<T>(int count, int offset) where T : unmanaged, IStructReverser => _reader.ReverseStructSpan<T>(count, offset);

    public Span<T> ReadUntil<T>(byte delimiter) where T : unmanaged => _reader.ReadSpan<T>(_reader.Data[_reader.Position..].IndexOf(delimiter));

    public Span<T> ReadUntil<T>(byte delimiter, int count) where T : unmanaged
    {
        var length = 0;
        var found = 0;
        while (found != count)
        {
            if (_reader.Data[_reader.Position + length++] == delimiter)
            {
                ++found;
            }
        }
        return _reader.ReadSpan<T>(length);
    }
    
    public string ReadNullTerminatedString()
    {
        var chars = ReadUntil<char>(0);
        _reader.Move(1);
        return chars.ToString();
    }
}