using System;
using ObjCRuntime;

[assembly: LinkWith("libEmptyLibrary.a", LinkTarget.ArmV7 | LinkTarget.Simulator, ForceLoad = true, Frameworks = "CoreTelephony SystemConfiguration", LinkerFlags = "-ObjC -lz -lresolv -lsqlite3")]
