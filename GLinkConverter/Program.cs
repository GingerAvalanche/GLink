// See https://aka.ms/new-console-template for more information

using System.Globalization;
using GLink.Common.Enums;
using GLink.Immutable;
using Revrs;

RevrsReader reader = new(File.ReadAllBytes("/home/Ginger/Documents/CemuShit/SLink2DB.bslnk"));
XLinkLoader loader = new(ref reader);
Console.WriteLine("Loaded WiiU SLink!");
for (var i = 0; i < loader.header.numUser; ++i)
{
    var user = loader.userData.Get(loader.userOffsets[i]);
    Console.WriteLine($"Loading: {loader.nameTable.GetByHash(loader.userHashes[i])}...");
    var userDefaults = loader.resParamDefineTable.userParams;
    for (var j = 0; j < userDefaults.Length; ++j)
    {
        var name = loader.nameTable.GetByOffset(userDefaults[j].namePos);
        var param = user.userParamTable[j];
        string value;
        switch (userDefaults[j].type)
        {
            case ParamType.UInt32:
                value = param.Direct(ref loader.directValueTable).UInt.ToString();
                break;
            case ParamType.Float:
                value = param.Direct(ref loader.directValueTable).Float.ToString(CultureInfo.CurrentCulture);
                break;
            case ParamType.Bool:
                value = param.Direct(ref loader.directValueTable).Bool.ToString();
                break;
            case ParamType.Enum:
                value = param.Direct(ref loader.directValueTable).Enum.ToString();
                break;
            case ParamType.String:
                value = param.String(ref loader.nameTable);
                break;
            case ParamType.Bitfield:
                value = param.Bitfield().ToString(CultureInfo.CurrentCulture);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        Console.WriteLine($"Param {j} - Name: {name} - Type: {userDefaults[j].type} - Value: {value}");
    }
}

// reader = new(File.ReadAllBytes("/home/Ginger/Documents/CemuShit/ELink2DB.belnk"));
// loader = new(ref reader);
// Console.WriteLine("Loaded WiiU ELink!");
//
// reader = new(File.ReadAllBytes("/home/Ginger/Documents/CemuShit/actually_switch/SLink2DB.bslnk"), Endianness.Little);
// loader = new(ref reader);
// Console.WriteLine("Loaded Switch SLink!");
//
// reader = new(File.ReadAllBytes("/home/Ginger/Documents/CemuShit/actually_switch/ELink2DB.belnk"), Endianness.Little);
// loader = new(ref reader);
// Console.WriteLine("Loaded Switch ELink!");