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

namespace AliEMAS.Binding.Droid
{
    public class PushApplication : Application
    {
        public PushApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
        public override void OnCreate()
        {
            base.OnCreate();
            PushServiceFactory.Init(this);
            PushServiceFactory.CloudPushService.Register(this, new CallBack());
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