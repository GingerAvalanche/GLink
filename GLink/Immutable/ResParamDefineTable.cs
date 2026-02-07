using System.Runtime.InteropServices;
using GLink.Common.Structs;
using GLink.Helpers;
using Revrs;

namespace GLink.Immutable;

[StructLayout(LayoutKind.Sequential)]
public ref struct ResParamDefineTable
{
    public IntUnion size;
    public IntUnion numUserParam;
    public IntUnion numAssetParam;
    public IntUnion numUserAssetParam;
    public IntUnion numTriggerParam;

    public Span<ResDefaultParam> userParams;
    public Span<ResDefaultParam> assetParams;
    public Span<ResDefaultParam> triggerParams;
    public StringTable stringTable;

    public ResParamDefineTable(ref RevrsReader reader)
    {
        size = reader.ReadStruct<IntUnion>();
        numUserParam = reader.ReadStruct<IntUnion>();
        numAssetParam = reader.ReadStruct<IntUnion>();
        numUserAssetParam = reader.ReadStruct<IntUnion>();
        numTriggerParam = reader.ReadStruct<IntUnion>();
        
        userParams = reader.ReadStructSpan<ResDefaultParam>(numUserParam);
        assetParams = reader.ReadStructSpan<ResDefaultParam>(numAssetParam);
        triggerParams = reader.ReadStructSpan<ResDefaultParam>(numTriggerParam);
        
        stringTable = new StringTable(reader.ReadUntil<byte>(0, numAssetParam.Int + 4));
        
        reader.Align(4);
    }
}