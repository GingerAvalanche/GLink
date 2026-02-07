using System.Diagnostics;
using GLink.Common.Structs;
using GLink.Helpers;
using Revrs;

namespace GLink.Immutable;

public ref struct XLinkLoader
{
    private const string Magic = "XLNK";
    public ResourceHeader header;
    public Span<IntUnion> userHashes;
    public Span<IntUnion> userOffsets;
    public ResParamDefineTable resParamDefineTable;
    public Dictionary<IntUnion, ResAssetParam> assetParamTable;
    public Dictionary<IntUnion, ResTriggerOverwriteParam> triggerOverwriteParamTable;
    public StringRefTable localPropertyNameRefTable;
    public StringRefTable localPropertyEnumNameRefTable;
    public Span<ParamValueUnion> directValueTable;
    public Span<ResRandomCallTable> randomTable;
    public Span<ResCurveCallTable> curveTable;
    public Span<ResCurvePoint> curvePointTable;
    public ParamGroupTable paramGroupTable;
    public UserTable userData;
    public ConditionTable conditionTable;
    //public NameTable nameTable;
    public StringTable nameTable;

    public XLinkLoader(ref RevrsReader reader)
    {
        header = reader.ReadStruct<ResourceHeader>();
        if (header.Magic != Magic)
        {
            throw new FormatException($"Invalid XLink resource header: {header.Magic}");
        }
        
        if (!Enum.IsDefined(header.version))
        {
            throw new FormatException($"Unsupported XLink version: {header.version}");
        }
        
        userHashes = reader.ReadStructSpan<IntUnion>(header.numUser);
        userOffsets = reader.ReadStructSpan<IntUnion>(header.numUser);
        
        resParamDefineTable = new ResParamDefineTable(ref reader);
        
        // TODO: Make zero-allocation
        assetParamTable = [];
        while (reader.Position < header.triggerOverwriteParamTablePos)
        {
            var index = reader.Position;
            assetParamTable[index] = new ResAssetParam(ref reader);
        }

        Debug.Assert(reader.Position == header.triggerOverwriteParamTablePos);
        // TODO: Make zero-allocation
        triggerOverwriteParamTable = [];
        while (reader.Position < header.localPropertyNameRefTablePos)
        {
            var index = reader.Position;
            triggerOverwriteParamTable[index] = new ResTriggerOverwriteParam(ref reader);
        }
        
        Debug.Assert(reader.Position == header.localPropertyNameRefTablePos);
        localPropertyNameRefTable = new StringRefTable(reader.ReadStructSpan<IntUnion>(header.numLocalPropertyNameRefTable));
        localPropertyEnumNameRefTable = new StringRefTable(reader.ReadStructSpan<IntUnion>(header.numLocalPropertyEnumNameRefTable));
        
        directValueTable = reader.ReadStructSpan<ParamValueUnion>(header.numDirectValueTable);
        
        randomTable = reader.ReadStructSpan<ResRandomCallTable>(header.numRandomTable);
        
        curveTable = reader.ReadStructSpan<ResCurveCallTable>(header.numCurveTable);
        curvePointTable = reader.ReadStructSpan<ResCurvePoint>(header.numCurvePointTable);
        
        Debug.Assert(reader.Position == header.exRegionPos);
        paramGroupTable = new ParamGroupTable(ref reader, userOffsets[0]);
        
        userData = new UserTable(ref reader, header.exRegionPos, header.conditionTablePos, resParamDefineTable.numUserParam);
        
        Debug.Assert(reader.Position == header.conditionTablePos);
        conditionTable = new ConditionTable(ref reader, header.nameTablePos - header.conditionTablePos);
        
        Debug.Assert(reader.Position == header.nameTablePos);
        nameTable = new StringTable(reader.Data[reader.Position..]);
    }
}