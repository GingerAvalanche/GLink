// See https://aka.ms/new-console-template for more information

using GLink.Immutable;
using Revrs;

RevrsReader reader = new(File.ReadAllBytes("/home/Ginger/Documents/CemuShit/SLink2DB.bslnk"));
XLinkLoader loader = new(ref reader);
Console.WriteLine("Loaded WiiU SLink!");

reader = new(File.ReadAllBytes("/home/Ginger/Documents/CemuShit/ELink2DB.belnk"));
loader = new(ref reader);
Console.WriteLine("Loaded WiiU ELink!");

reader = new(File.ReadAllBytes("/home/Ginger/Documents/CemuShit/actually_switch/SLink2DB.bslnk"), Endianness.Little);
loader = new(ref reader);
Console.WriteLine("Loaded Switch SLink!");

reader = new(File.ReadAllBytes("/home/Ginger/Documents/CemuShit/actually_switch/ELink2DB.belnk"), Endianness.Little);
loader = new(ref reader);
Console.WriteLine("Loaded Switch ELink!");