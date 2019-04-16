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
        #region 阿里推送
        public static (bool, string) Init(string appKey, string appSecret, UIApplication application, bool debug)
        {
            ALBBMANAnalytics.Instance.InitWithAppKey(appKey, appSecret);
            if (debug)
                ALBBMANAnalytics.Instance.TurnOnDebug();
            //注册移动推送

            bool result = false;
            string message = string.Empty;
            RegisterAPNS(application);
            AliEMAS.Binding.iOS.CloudPushSDK.AsyncInit(appKey, appSecret, (s) =>
            {
                if (s.Success)
                {
                    result = true;
                    message = CloudPushSDK.DeviceId;
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
                    });
                }
                else
                {
                    message = s.Error.ToString();
                }
            });
            RegisterMessageReceive();
            return (result, message);
        }
        /// <summary>
        /// 注册苹果推送，获取deviceToken用于推送
        /// </summary>
        static void RegisterAPNS(UIApplication application)
        {
            if (System.Convert.ToDouble(UIDevice.CurrentDevice.SystemVersion) >= 8.0)
            {
                //  iOS 8 Notifications
                application.RegisterUserNotificationSettings(UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Sound | UIUserNotificationType.Alert | UIUserNotificationType.Badge, null));
                application.RegisterForRemoteNotifications();
            }
            else
            {
                //  ios8以下的放弃了
                // iOS < 8 Notifications
                UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound);
            }
        }
        /// <summary>
        /// 注册推送通道打开监听
        /// </summary>
        void ListenerOnChannelOpened()
        {
            NSNotificationCenter.DefaultCenter.AddObserver((NSString)"CCPDidChannelConnectedSuccess", (NSNotification notification) =>
            {
                Console.WriteLine("消息通道建立成功");
            });
        }
        /// <summary>
        ///  注册推送消息到来监听
        /// </summary>
        public void RegisterMessageReceive()
        {
            NSNotificationCenter.DefaultCenter.AddObserver((NSString)"CCPDidReceiveMessageNotification", (NSNotification notification) =>
            {
                CCPSysMessage message = notification.Object as CCPSysMessage;
                string title = NSString.FromData(message.Title, NSStringEncoding.UTF8);
                string body = NSString.FromData(message.Title, NSStringEncoding.UTF8);
                Console.WriteLine("Receive message: title = " + title + "   " + "body = " + body);
            });
        }

        #endregion

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