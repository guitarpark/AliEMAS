using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AliEMAS.Droid;
using AliEMAS.Forms;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Com.Alibaba.Sdk.Android.Man;
using Com.Alibaba.Sdk.Android.Man.Network;
using Com.Alibaba.Sdk.Android.Push;
using Com.Alibaba.Sdk.Android.Push.Noonesdk;
using Com.Alibaba.Sdk.Android.Push.Register;

[assembly: Xamarin.Forms.Dependency(typeof(DroidServices))]
namespace AliEMAS.Droid
{
    public class DroidServices : IAliEMAS
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="application"></param>
        /// <param name="context"></param>
        /// <param name="callBack"></param>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <param name="channelDescription">配置通知渠道的属性</param>
        /// <param name="channelId">通知渠道的id</param>
        /// <param name="channelName">用户可以看到的通知渠道的名字</param>
        /// <param name="applicationId">接入FCM/GCM初始化推送</param>
        /// <param name="sendId">接入FCM/GCM初始化推送</param>
        /// <param name="debug">启用调试</param>
        /// <param name="xiaomiId">小米Id</param>
        /// <param name="xiaomiKey">小米Key</param>
        /// <returns></returns>
        public static ICloudPushService Init(string appKey, string appSecret, Application application, Context context, ICommonCallback callBack,
            string channelId, string channelName, string channelDescription, bool debug = false,
            string xiaomiId = "", string xiaomiKey = "", string sendId = "", string applicationId = ""
            )
        {
            IMANService manService = MANServiceProvider.Service;
            if (debug)
                manService.MANAnalytics.TurnOnDebug();
            manService.MANAnalytics.Init(application, context, appKey, appSecret);

            PushServiceFactory.Init(context);
            PushServiceFactory.CloudPushService.Register(context, appKey, appSecret, callBack);

            // Android 8.0以上设备无法收到处理
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                NotificationManager notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);

                NotificationChannel mChannel = new NotificationChannel(channelId, channelName, NotificationImportance.High);
                // 
                mChannel.Description = channelDescription;
                // 设置通知出现时的闪灯（如果 android 设备支持的话）
                mChannel.EnableLights(true);
                //mChannel.LightColor = ;
                // 设置通知出现时的震动（如果 android 设备支持的话）
                mChannel.EnableVibration(true);
                mChannel.SetVibrationPattern(new long[] { 100, 200, 300, 400, 500, 400, 300, 200, 400 });
                //最后在notificationmanager中创建该通知渠道
                notificationManager.CreateNotificationChannel(mChannel);
            }

            MiPushRegister.Register(context, xiaomiId, xiaomiKey); // 初始化小米辅助推送
            HuaWeiRegister.Register(context); // 接入华为辅助推送
            GcmRegister.Register(context, sendId, applicationId); // 接入FCM/GCM初始化推送

            return PushServiceFactory.CloudPushService;
        }

        public void CloseAutoPageTrack()
        {
            MANServiceProvider.Service.MANAnalytics.TurnOffAutoPageTrack();
        }

        #region 页面埋点
        public void CustomPage(string pageName, string referPageName, long duration, Dictionary<string, string> properties)
        {
            MANPageHitBuilder pageHitBuilder = new MANPageHitBuilder(pageName);
            pageHitBuilder.SetReferPage(referPageName);
            pageHitBuilder.SetDurationOnPage(duration);
            pageHitBuilder.SetProperties(properties);
            MANServiceProvider.Service.MANAnalytics.DefaultTracker.Send(pageHitBuilder.Build());

        }

        public void PageAppear()
        {
            MANServiceProvider.Service.MANPageHitHelper.PageAppear(Application.Context as Activity);
        }

        public void PageDisAppear()
        {
            MANServiceProvider.Service.MANPageHitHelper.PageDisAppear(Application.Context as Activity);
        }

        public void PageProperties(Dictionary<string, string> data)
        {
            MANServiceProvider.Service.MANPageHitHelper.UpdatePageProperties(data);
        }
        #endregion

        #region 登陆注册
        public void UserLoginOut()
        {
            MANServiceProvider.Service.MANAnalytics.UpdateUserAccount("", "");
        }

        public void UserRegister(string data)
        {
            MANServiceProvider.Service.MANAnalytics.UserRegister(data);
        }

        public void UserUpdateAccount(string data, string userId)
        {
            MANServiceProvider.Service.MANAnalytics.UpdateUserAccount(data, userId);
        }
        #endregion

        #region 网络性能统计
        private static MANNetworkPerformanceHitBuilder networkPerf;
        public void NetworkPerfFinish(long length)
        {
            networkPerf.HitRequestEndWithLoadBytes(length);
            networkPerf.Dispose();
            networkPerf = null;
        }

        public void NetworkPerfExtraInfo(string key, string value)
        {
            networkPerf.WithExtraInfo(key, value);
        }

        public void NetworkPerfInit(string url, string method)
        {
            networkPerf = new MANNetworkPerformanceHitBuilder(url, method);
        }

        public void NetworkPerfStart()
        {
            networkPerf.HitRequestStart();
        }
        #endregion
    }
}