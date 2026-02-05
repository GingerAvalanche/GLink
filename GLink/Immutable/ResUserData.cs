using System.Runtime.InteropServices;
using GLink.Common.Structs;
using GLink.Helpers;
using Revrs;

namespace GLink.Immutable;

[StructLayout(LayoutKind.Sequential)]
public ref struct ResUserData
{
    public ResUserHeader header;
    public Span<ConvertibleInt> localPropertyNameRefIndices;
    public Span<ResParam> userParamTable;
    public Span<ConvertibleShort> sortedIdAssetCallTable;
    public Span<ResAssetCallTable> assetCallTable;
    //public Span<ResContainerParam> containerTable;
    // TODO: Make this zero-allocation
    public ContainerTable containerTable;
    public Span<ResActionSlot> resActionSlotTable;
    public Span<ResAction> resActionTable;
    public Span<ResActionTrigger> resActionTriggerTable;
    public Span<ResProperty> resPropertyTable;
    public Span<ResPropertyTrigger> resPropertyTriggerTable;
    public Span<ResAlwaysTrigger> resAlwaysTriggerTable;
    
    public ResUserData(ref RevrsReader reader, int numUserParam)
    {
        var addressOfThis = reader.Position;
        header = reader.Read<ResUserHeader>();
        var containerEnd = addressOfThis + header.triggerTablePos.Int;
        
        localPropertyNameRefIndices = reader.ReadSpan<ConvertibleInt>(header.numLocalProperty);
        userParamTable = reader.ReadStructSpan<ResParam>(numUserParam);
        sortedIdAssetCallTable = reader.ReadSpan<ConvertibleShort>(header.numCallTable);
        reader.Align(4);
        assetCallTable = reader.ReadStructSpan<ResAssetCallTable>(header.numCallTable);
        
        //containerTable = reader.ReadStructSpan<ResContainerParam>((containerEnd - reader.Position) / 12);
        containerTable = new ContainerTable(ref reader, containerEnd);
        
        resActionSlotTable = reader.ReadStructSpan<ResActionSlot>(header.numResActionSlot);
        resActionTable = reader.ReadStructSpan<ResAction>(header.numResAction);
        resActionTriggerTable = reader.ReadStructSpan<ResActionTrigger>(header.numResActionTrigger);
        resPropertyTable = reader.ReadStructSpan<ResProperty>(header.numResProperty);
        resPropertyTriggerTable = reader.ReadStructSpan<ResPropertyTrigger>(header.numResPropertyTrigger);
        resAlwaysTriggerTable = reader.ReadStructSpan<ResAlwaysTrigger>(header.numResAlwaysTrigger);
    }
}