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
        /// <returns></returns>
        public static ICloudPushService Init(string appKey, string appSecret, Application application, Context context, ICommonCallback callBack, bool debug)
        {
            IMANService manService = MANServiceProvider.Service;
            if (debug)
                manService.MANAnalytics.TurnOnDebug();
            manService.MANAnalytics.Init(application, context, appKey, appSecret);

            PushServiceFactory.Init(context);
            PushServiceFactory.CloudPushService.Register(context, appKey, appSecret, callBack);


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