using Revrs;

namespace GLink.Helpers;

public static class RevrsExtensions
{
    extension(RevrsReader reader)
    {
        public Span<T> ReadUntil<T>(byte delimiter) where T : unmanaged =>
            reader.ReadSpan<T>(reader.Data[reader.Position..].IndexOf(delimiter));

        public Span<T> ReadUntil<T>(byte delimiter, int count) where T : unmanaged
        {
            var length = 0;
            var found = 0;
            while (found != count)
            {
                if (reader.Data[reader.Position + length++] == delimiter)
                {
                    ++found;
                }
            }
            return reader.ReadSpan<T>(length);
        }

        public string ReadNullTerminatedString()
        {
            var chars = reader.ReadUntil<char>(0);
            reader.Move(1);
            return chars.ToString();
        }
    }
}