using GLink.Common.Structs;
using GLink.Immutable;

namespace GLink.Mutable;

public class ParamDefineTable
{
    public ConvertibleInt NumUserAssetParam;
    public Dictionary<string, DefaultParam> UserParams;
    public Dictionary<string, DefaultParam> AssetParams;
    public Dictionary<string, DefaultParam> TriggerParams;

    public ParamDefineTable(ResParamDefineTable defineTable)
    {
        NumUserAssetParam = defineTable.numUserAssetParam;
        UserParams = new Dictionary<string, DefaultParam>();
        foreach (var param in defineTable.userParams)
        {
            UserParams.Add(defineTable.stringTable.GetByOffset(param.namePos), DefaultParam.FromImmutable(param));
        }
        
        AssetParams = new Dictionary<string, DefaultParam>();
        foreach (var param in defineTable.assetParams)
        {
            AssetParams.Add(defineTable.stringTable.GetByOffset(param.namePos), DefaultParam.FromImmutable(param));
        }
        
        TriggerParams = new Dictionary<string, DefaultParam>();
        foreach (var param in defineTable.triggerParams)
        {
            TriggerParams.Add(defineTable.stringTable.GetByOffset(param.namePos), DefaultParam.FromImmutable(param));
        }
    }
}