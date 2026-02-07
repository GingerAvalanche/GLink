// See https://aka.ms/new-console-template for more information

using System.Globalization;
using GLink.Common.Enums;
using GLink.Immutable;
using Revrs;

List<string> lines = [];
RevrsReader reader = new(File.ReadAllBytes("/home/Ginger/Documents/CemuShit/SLink2DB.bslnk"));
XLinkLoader loader = new(ref reader);
Console.WriteLine("Loaded WiiU SLink!");
for (var i = 0; i < loader.header.numUser; ++i)
{
    var user = loader.userData.Get(loader.userOffsets[i]);
    lines.Add($"{loader.nameTable.GetByHash(loader.userHashes[i])} {{");
    var paramNameTable = loader.resParamDefineTable.stringTable;
    var userDefaults = loader.resParamDefineTable.userParams;
    for (var j = 0; j < userDefaults.Length; ++j)
    {
        var name = paramNameTable.GetByOffset(userDefaults[j].namePos);
        var param = user.userParamTable[j];
        var value = param.Type switch
        {
            ReferenceType.Direct => userDefaults[j].type switch
            {
                ParamType.Int32 => param.Direct(ref loader.directValueTable).Int.ToString(),
                ParamType.Float => param.Direct(ref loader.directValueTable).Float.ToString(CultureInfo.CurrentCulture),
                ParamType.Bool => param.Direct(ref loader.directValueTable).Bool.ToString(),
                ParamType.Enum =>
                    $"({userDefaults[j].type}) {param.Direct(ref loader.directValueTable).Enum.ToString()}",
                _ => throw new ArgumentOutOfRangeException()
            },
            ReferenceType.String => $"\"{param.String(ref loader.nameTable)}\"",
            ReferenceType.ArrangeParam => $"({param.Type}) {param}",
            ReferenceType.Bitfield => $"({param.Type}) {param.Bitfield().ToString(CultureInfo.CurrentCulture)}",
            _ => throw new ArgumentOutOfRangeException()
        };
        lines.Add($"    {name}: {value}");
    }
    lines.Add("}");
}
File.WriteAllLines("/home/Ginger/Documents/CemuShit/xlink_test_output.txt", lines);

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