using System.Runtime.CompilerServices;
using GLink.Common.Enums;
using GLink.Immutable;
using Revrs;

namespace GLink.Helpers;

public ref struct ConditionTable(ref XLinkReader reader, int length)
{
    private Span<byte> _table = reader.Read(length);
    private readonly Endianness _endian = reader.Endianness;

    public ResCondition this[int index]
    {
        get
        {
            var be_hack = _endian == Endianness.Big ? 3 : 0;
            var count = 0;
            var offset = 0;
            while (count < index)
            {
                var tmp = _table[offset + be_hack];
                if (tmp is 1 or 2)
                {
                    offset += 8;
                }
                else
                {
                    offset += 20;
                }

                ++count;
            }

            return GetByOffset(offset);
        }
    }

    public ResCondition GetByOffset(int offset)
    {
        RevrsReader reader = new(_table[offset..], _endian);
        return new ResCondition(ref reader);
    }

    public ref struct Enumerator
    {
        /// <summary>The span being enumerated.</summary>
        private readonly Span<byte> _span;
        /// <summary>The next index to yield.</summary>
        private int _index;
        /// <summary>The endianness of the span.</summary>
        private readonly bool _be;

        /// <summary>Initialize the enumerator.</summary>
        /// <param name="span">The span to enumerate.</param>
        /// <param name="be">True if the span is Big Endian.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Enumerator(Span<byte> span, bool be)
        {
            _span = span;
            _index = -1;
            _be = be;
        }

        /// <summary>Advances the enumerator to the next element of the span.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            var type = (ContainerType)_span[_index + (_be ? 3 : 0)];
            var index = _index + (type is ContainerType.Random or ContainerType.Random2 ? 8 : 20);
            if (index >= _span.Length) return false;
            _index = index;
            return true;
        }

        /// <summary>Gets the element at the current position of the enumerator.</summary>
        public ResCondition Current
        {
            get
            {
                var reader = new RevrsReader(_span[_index..], _be ? Endianness.Big : Endianness.Little);
                var condition = new ResCondition(ref reader);
                return condition;
            }
        }
    }
}