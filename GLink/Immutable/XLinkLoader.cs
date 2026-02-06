using GLink.Common.Structs;
using GLink.Helpers;
using Revrs;

namespace GLink.Immutable;

public ref struct XLinkLoader
{
    private const string Magic = "XLNK";
    public ResourceHeader header;
    public Span<ConvertibleInt> userHashes;
    public Span<ConvertibleInt> userOffsets;
    public UserTable userData;
    public ResParamDefineTable resParamDefineTable;
    public Dictionary<ConvertibleInt, ResAssetParam> assetParamTable;
    public Dictionary<ConvertibleInt, ResTriggerOverwriteParam> triggerOverwriteParamTable;
    //public NameTable localPropertyNameRefTable;
    public StringTable localPropertyNameRefTable;
    //public NameTable localPropertyEnumNameRefTable;
    public StringTable localPropertyEnumNameRefTable;
    public Span<ParamValueUnion> directValueTable;
    public Span<ResRandomCallTable> randomTable;
    public Span<ResCurveCallTable> curveTable;
    public Span<CurvePoint> curvePointTable;
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
        
        userHashes = reader.ReadStructSpan<ConvertibleInt>(header.numUser);
        userOffsets = reader.ReadStructSpan<ConvertibleInt>(header.numUser);
        
        resParamDefineTable = new ResParamDefineTable(ref reader);
        
        // TODO: Make zero-allocation
        assetParamTable = [];
        while (reader.Position < header.triggerOverwriteParamTablePos)
        {
            var index = reader.Position;
            assetParamTable[index] = new ResAssetParam(ref reader);
        }

        // TODO: Make zero-allocation
        triggerOverwriteParamTable = [];
        while (reader.Position < header.localPropertyNameRefTablePos)
        {
            var index = reader.Position;
            triggerOverwriteParamTable[index] = new ResTriggerOverwriteParam(ref reader);
        }
        
        localPropertyNameRefTable = new StringTable(reader.ReadUntil<byte>(0, header.numLocalPropertyNameRefTable));
        localPropertyEnumNameRefTable = new StringTable(reader.ReadUntil<byte>(0, header.numLocalPropertyEnumNameRefTable));
        
        directValueTable = reader.ReadSpan<ParamValueUnion>(header.numDirectValueTable);
        
        randomTable = reader.ReadStructSpan<ResRandomCallTable>(header.numRandomTable);
        
        curveTable = reader.ReadStructSpan<ResCurveCallTable>(header.numCurveTable);
        curvePointTable = reader.ReadStructSpan<CurvePoint>(header.numCurvePointTable);
        
        userData = new UserTable(ref reader, header.exRegionPos, header.conditionTablePos, resParamDefineTable.numUserParam);
        
        conditionTable = new ConditionTable(ref reader, header.nameTablePos - header.conditionTablePos);
        
        nameTable = new StringTable(reader.Data[reader.Position..]);
    }
}