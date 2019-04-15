using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Android.App;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("AliEMAS.Binding.Droid")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("AliEMAS.Binding.Droid")]
[assembly: AssemblyCopyright("Copyright ©  2018")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]


#region 阿里云推送相关
[assembly: UsesPermission(Android.Manifest.Permission.WriteSettings)]
[assembly: UsesPermission(Android.Manifest.Permission.Internet)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessNetworkState)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessWifiState)]
[assembly: UsesPermission(Android.Manifest.Permission.WakeLock)]
[assembly: UsesPermission(Android.Manifest.Permission.ReadPhoneState)]
[assembly: UsesPermission(Android.Manifest.Permission.RestartPackages)]
[assembly: UsesPermission(Android.Manifest.Permission.GetTasks)]
[assembly: UsesPermission(Android.Manifest.Permission.GetAccounts)]
[assembly: UsesPermission(Android.Manifest.Permission.ReceiveBootCompleted)]
[assembly: UsesPermission(Android.Manifest.Permission.Vibrate)]
[assembly: UsesPermission(Android.Manifest.Permission.ReorderTasks)]
[assembly: UsesPermission(Android.Manifest.Permission.ReadExternalStorage)]
[assembly: UsesPermission(Android.Manifest.Permission.WriteExternalStorage)]
[assembly: UsesPermission(Android.Manifest.Permission.MountUnmountFilesystems)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessNotificationPolicy)]
[assembly:UsesPermission("com.huawei.android.push.intent.REGISTER")]
#endregion