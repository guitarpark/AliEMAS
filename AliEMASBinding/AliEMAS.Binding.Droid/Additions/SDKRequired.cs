using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace com.alibaba.sdk.android.push
{
    [Service(Name = "com.alibaba.sdk.android.push.MsgService", Exported = false), IntentFilter(new[] { "com.alibaba.sdk.android.push.NOTIFY_ACTION" })]
    public class MsgService { }

    [Service(Name = "com.alibaba.sdk.android.push.AliyunPushIntentService", Exported = true), IntentFilter(new[] { "org.agoo.android.intent.action.RECEIVE" })]
    public class AliyunPushIntentService { }
}

namespace com.alibaba.sdk.android.push.channel
{
    [Service(Name = "com.alibaba.sdk.android.push.channel.CheckService", Process = ":channel"), IntentFilter(new[] { "com.alibaba.sdk.android.push.CHECK_SERVICE" })]
    public class CheckService { }

    [Service(Name = "com.alibaba.sdk.android.push.channel.TaobaoRecvService", Process = ":channel", Exported = true), IntentFilter(new[] { "org.android.agoo.client.MessageReceiverService" })]
    public class TaobaoRecvService { }

    [Service(Name = "com.alibaba.sdk.android.push.channel.KeepChannelService", Process = ":channel", Permission = "android.permission.BIND_JOB_SERVICE")]
    public class KeepChannelService { }
}
namespace com.taobao.accs
{
    [Service(Name = "com.taobao.accs.ChannelService", Process = ":channel", Exported = true), IntentFilter(new[] { "com.taobao.accs.intent.action.SERVICE" })]
    public class ChannelService { }

    [Service(Name = "com.taobao.accs.ChannelService$KernelService", Process = ":channel", Exported = false)]
    public class KernelService { }

    [BroadcastReceiver(Name = "com.taobao.accs.EventReceiver", Process = ":channel")]
    [IntentFilter(new[] { "android.intent.action.BOOT_COMPLETED", "android.net.conn.CONNECTIVITY_CHANGE", "android.intent.action.USER_PRESENT" })]
    [IntentFilter(new[] { "android.intent.action.PACKAGE_REMOVED" }, DataScheme = "package")]
    public class EventReceiver { }

    [BroadcastReceiver(Name = "com.taobao.accs.ServiceReceiver", Process = ":channel")]
    [IntentFilter(new[] { "com.taobao.accs.intent.action.COMMAND", "com.taobao.accs.intent.action.START_FROM_AGOO" })]
    public class ServiceReceiver { }
}
namespace com.taobao.accs.data
{
    [Service(Name = "com.taobao.accs.data.MsgDistributeService", Exported = true), IntentFilter(new[] { "com.taobao.accs.intent.action.RECEIVE" })]
    public class MsgDistributeService { }
}
namespace org.android.agoo.accs
{
    [Service(Name = "org.android.agoo.accs.AgooService", Exported = true), IntentFilter(new[] { "com.taobao.accs.intent.action.RECEIVE" })]
    public class AgooService { }
}
namespace com.taobao.agoo
{
    [BroadcastReceiver(Name = "com.taobao.agoo.AgooCommondReceiver", Process = ":channel", Exported = true)]
    [IntentFilter(new[] { "${applicationId}.intent.action.COMMAND" })]
    [IntentFilter(new[] { "android.intent.action.PACKAGE_REMOVED" }, DataScheme = "package")]
    public class AgooCommondReceiver { }
}
namespace com.taobao.accs.@internal
{
    [Service(Name = "com.taobao.accs.internal.AccsJobService", Process = ":channel", Permission = "android.permission.BIND_JOB_SERVICE")]
    public class AccsJobService { }
}
namespace com.alibaba.sdk.android.push
{
    [BroadcastReceiver(Name = "com.alibaba.sdk.android.push.SystemEventReceiver", Process = ":channel")]
    [IntentFilter(new[] { "android.intent.action.MEDIA_MOUNTED", "android.intent.action.ACTION_POWER_CONNECTED", "android.intent.action.ACTION_POWER_DISCONNECTED" })]
    public class SystemEventReceiver { }
}
//namespace com.alibaba.sdk.android.push.keeplive
//{
//    [Activity(Name = "com.alibaba.sdk.android.push.keeplive.PushExtActivity", ExcludeFromRecents = true, Exported = false, FinishOnTaskLaunch = false,
//         LaunchMode = LaunchMode.SingleInstance, Theme = "@android:style/Theme.Translucent.NoTitleBar.Fullscreen", Process = ":channel",
//        ConfigurationChanges = ConfigChanges.KeyboardHidden | ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.Navigation | ConfigChanges.Keyboard
//        )]
//    public partial class PushExtActivity { }
//}