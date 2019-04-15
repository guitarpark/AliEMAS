using System;
using ObjCRuntime;

[assembly: LinkWith ("libEmptyLibrary.a", LinkTarget.ArmV7 | LinkTarget.Simulator, ForceLoad = true,SmartLink =true,
    Frameworks = "CoreTelephony SystemConfiguration QuartzCore CoreGraphics", LinkerFlags = "-ObjC -lz -lresolv -lsqlite3",
    IsCxx =true)]
