using System.Runtime.InteropServices;
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
}