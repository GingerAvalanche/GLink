// See https://aka.ms/new-console-template for more information

using GLink.Helpers;
using GLink.Immutable;
using Revrs;

RevrsReader rev = new(File.ReadAllBytes("/home/Ginger/Documents/CemuShit/SLink2DB.bslnk"));
XLinkReader reader = new(ref rev);
XLinkLoader loader = new(ref reader);
Console.WriteLine("Loaded WiiU SLink!");

rev = new(File.ReadAllBytes("/home/Ginger/Documents/CemuShit/ELink2DB.belnk"));
reader = new(ref rev);
loader = new(ref reader);
Console.WriteLine("Loaded WiiU ELink!");

rev = new(File.ReadAllBytes("/home/Ginger/Documents/CemuShit/actually_switch/SLink2DB.bslnk"));
reader = new(ref rev);
loader = new(ref reader);
Console.WriteLine("Loaded Switch SLink!");

rev = new(File.ReadAllBytes("/home/Ginger/Documents/CemuShit/actually_switch/ELink2DB.belnk"));
reader = new(ref rev);
loader = new(ref reader);
Console.WriteLine("Loaded Switch ELink!");