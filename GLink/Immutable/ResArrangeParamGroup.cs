using System.Runtime.InteropServices;
using GLink.Helpers;
using Revrs;

namespace GLink.Immutable;

[StructLayout(LayoutKind.Sequential)]
public ref struct ResArrangeParamGroup
{
    private int count;
    public Span<ResArrangeParam> Params;

    public ResArrangeParamGroup(ref RevrsReader reader)
    {
        count = reader.Read<int>();
        Params = reader.ReadStructSpan<ResArrangeParam>(count);
    }

    public ResArrangeParamGroup(ref Span<ResArrangeParam> data)
    {
        count = data.Length;
        Params = data;
    }

    public string ToString(ref StringTable table) => count switch
    {
        0 => "{}",
        1 => $"{{ {Params[0].ToString(ref table)} }}",
        2 => $"{{ {Params[0].ToString(ref table)}, {Params[1].ToString(ref table)} }}",
        _ => throw new NotSupportedException()
    };
}