using System.Runtime.InteropServices;
using GLink.Common.Structs;
using GLink.Helpers;
using Revrs;

namespace GLink.Immutable;

[StructLayout(LayoutKind.Sequential)]
public ref struct ResParamDefineTable
{
    public ConvertibleInt size;
    public ConvertibleInt numUserParam;
    public ConvertibleInt numAssetParam;
    public ConvertibleInt numUserAssetParam;
    public ConvertibleInt numTriggerParam;

    public Span<ResDefaultParam> userParams;
    public Span<ResDefaultParam> assetParams;
    public Span<ResDefaultParam> triggerParams;
    public StringTable stringTable;

    public ResParamDefineTable(ref RevrsReader reader)
    {
        size = reader.Read<ConvertibleInt>();
        numUserParam = reader.Read<ConvertibleInt>();
        numAssetParam = reader.Read<ConvertibleInt>();
        numUserAssetParam = reader.Read<ConvertibleInt>();
        numTriggerParam = reader.Read<ConvertibleInt>();
        
        userParams = reader.ReadStructSpan<ResDefaultParam>(numUserParam);
        assetParams = reader.ReadStructSpan<ResDefaultParam>(numAssetParam);
        triggerParams = reader.ReadStructSpan<ResDefaultParam>(numTriggerParam);
        
        stringTable = new StringTable(reader.ReadUntil<byte>(0, numAssetParam.Int + 4));
        
        reader.Align(4);
    }
}