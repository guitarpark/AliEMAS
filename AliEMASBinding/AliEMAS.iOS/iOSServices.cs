using AliEMAS.Binding.iOS;
using AliEMAS.Forms;
using AliEMAS.iOS;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(iOSServices))]
namespace AliEMAS.iOS
{
    public class iOSServices : IAliEMAS
    {
        public static void Init(string appKey, string appSecret, bool debug)
        {
            ALBBMANAnalytics.Instance.InitWithAppKey(appKey, appSecret);
            if (debug)
                ALBBMANAnalytics.Instance.TurnOnDebug();

        }
        public void CloseAutoPageTrack()
        {
            //无此接口
        }
        #region 页面埋点
        public void CustomPage(string pageName, string referPageName, long duration, Dictionary<string, string> properties)
        {
            var builder = new ALBBMANPageHitBuilder();
            builder.SetPageName(pageName);
            builder.SetReferPage(referPageName);
            builder.SetDurationOnPage(duration);
            builder.SetProperties(NSDictionary.FromObjectsAndKeys(properties.Values.ToArray(), properties.Keys.ToArray()));

            //库里没有builder方法
            //ALBBMANAnalytics.Instance.DefaultTracker.Send(builder.Builder());
        }

        public void PageAppear()
        {
            ALBBMANPageHitHelper.GetInstance().PageAppear(CurrentUIViewController);
        }

        public void PageDisAppear()
        {
            ALBBMANPageHitHelper.GetInstance().PageDisAppear(CurrentUIViewController);
        }

        public void PageProperties(Dictionary<string, string> data)
        {
            ALBBMANPageHitHelper.GetInstance().UpdatePageProperties(CurrentUIViewController, NSDictionary.FromObjectsAndKeys(data.Values.ToArray(), data.Keys.ToArray()));
        }
        #endregion

        #region 注册相关
        public void UserLoginOut()
        {
            ALBBMANAnalytics.Instance.UpdateUserAccount("", "");
        }

        public void UserRegister(string data)
        {
            ALBBMANAnalytics.Instance.UserRegister(data);
        }

        public void UserUpdateAccount(string data, string userId)
        {
            ALBBMANAnalytics.Instance.UpdateUserAccount(data, userId);
        }

        #endregion

        #region 网络性能统计

        private static ALBBMANNetworkHitBuilder networkPerf;
        public void NetworkPerfInit(string url, string method)
        {
            networkPerf = new ALBBMANNetworkHitBuilder(url, method);
        }

        public void NetworkPerfStart()
        {
            networkPerf.RequestStart();
        }

        public void NetworkPerfExtraInfo(string key, string value)
        {
            networkPerf.SetProperty(key, value);
        }

        public void NetworkPerfFinish(long length)
        {
            networkPerf.RequestEndWithBytes((nint)length);
            networkPerf.Dispose();
            networkPerf = null;
        }
        #endregion

        private UIViewController CurrentUIViewController
        {
            get
            {
                var controller = UIApplication.SharedApplication.KeyWindow.RootViewController;
                while (controller.PresentedViewController != null)
                {
                    controller = controller.PresentedViewController;
                }
                return controller;
            }
        }
    }
}