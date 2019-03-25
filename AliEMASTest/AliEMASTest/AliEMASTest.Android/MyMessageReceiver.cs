using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Alibaba.Sdk.Android.Push;
using Com.Alibaba.Sdk.Android.Push.Noonesdk;
using Com.Alibaba.Sdk.Android.Push.Notification;

namespace AliEMASTest.Droid
{
    [BroadcastReceiver(Name = "com.guitarpark.app.MyMessageReceiver", Exported = false), IntentFilter(new string[] { "com.alibaba.push2.action.NOTIFICATION_OPENED", "com.alibaba.push2.action.NOTIFICATION_REMOVED", "com.alibaba.sdk.android.push.RECEIVE" })]
    public class MyMessageReceiver : MessageReceiver
    {
        protected override void OnNotification(Context p0, string p1, string p2, IDictionary<string, string> p3)
        {
            
            base.OnNotification(Android.App.Application.Context, p1, p2, p3);
        }
        protected override void OnMessage(Context p0, CPushMessage p1)
        {
            base.OnMessage(Android.App.Application.Context, p1);
        }
        protected override void OnNotificationOpened(Context p0, string p1, string p2, string p3)
        {
            base.OnNotificationOpened(p0, p1, p2, p3);
        }

        protected override void OnNotificationClickedWithNoAction(Context p0, string p1, string p2, string p3)
        {
            base.OnNotificationClickedWithNoAction(p0, p1, p2, p3);
        }
        protected override void OnNotificationReceivedInApp(Context p0, string p1, string p2, IDictionary<string, string> p3, int p4, string p5, string p6)
        {
            base.OnNotificationReceivedInApp(p0, p1, p2, p3, p4, p5, p6);
        }
        protected override void OnNotificationRemoved(Context p0, string p1)
        {
            base.OnNotificationRemoved(p0, p1);
        }
    }

    [Application(Name = "com.guitarpark.app.MainApplication")]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }


        public override void OnCreate()
        {
            base.OnCreate();

            var aaa = Build.VERSION.SdkInt;
            var bbb = Build.VERSION_CODES.O;


            var nnn = aaa > bbb;

            PushServiceFactory.Init(this);
            PushServiceFactory.CloudPushService.Register(this, new CallBack());
            var deviceId = PushServiceFactory.CloudPushService.DeviceId;
        }

        public class CallBack : Java.Lang.Object, ICommonCallback
        {
            public void OnFailed(string p0, string p1)
            {
                var fff = p1;
            }

            public void OnSuccess(string p0)
            {
                var ggg = p0;
            }
        }
    }
}