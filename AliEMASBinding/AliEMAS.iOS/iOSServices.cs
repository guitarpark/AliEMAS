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
        public static bool Init(string appKey, string appSecret, UIApplication application, NSDictionary options, bool debug = false)
        {
            try
            {
                ALBBMANAnalytics.Instance.InitWithAppKey(appKey, appSecret);
                if (debug)
                    ALBBMANAnalytics.Instance.TurnOnDebug();

                #region 阿里推送
                RegisterAPNS(application);
                InitCloudPush(appKey, appSecret);

                NSNotificationCenter.DefaultCenter.AddObserver((NSString)"CCPDidChannelConnectedSuccess", (NSNotification notification) =>
                {
                    Console.WriteLine("消息通道建立成功");
                });

                options = options ?? new NSDictionary();
                CloudPushSDK.SendNotificationAck(options);

                //同步角标数到服务端
                nint num = UIApplication.SharedApplication.ApplicationIconBadgeNumber;
                //每次打开APP 角标就置空0
                CloudPushSDK.SyncBadgeNum(0, (CloudPushCallbackResult res) =>
                {
                    if (res.Success)
                        Console.WriteLine("同步角标成功");
                    else
                        Console.WriteLine("同步角标失败");
                });
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("初始化阿里错误:" + ex.Message);
                return true;
            }
        }

        #region 阿里推送
        /// <summary>
        /// 注册苹果推送，获取deviceToken用于推送
        /// </summary>
        static void RegisterAPNS(UIApplication application)
        {
            if (System.Convert.ToDouble(UIDevice.CurrentDevice.SystemVersion) >= 8.0)
            {
                // iOS 8 Notifications
                application.RegisterUserNotificationSettings(UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Sound | UIUserNotificationType.Alert | UIUserNotificationType.Badge, null));
                application.RegisterForRemoteNotifications();
            }
            else
            {
                // iOS < 8 Notifications
                UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound);
            }
        }

        /// <summary>
        /// SDK初始化
        /// </summary>
        static void InitCloudPush(string appkey, string appSecret)
        {
            CloudPushSDK.AsyncInit(appkey, appSecret, (CloudPushCallbackResult res) =>
            {
                if (res.Success)
                {
                    Console.WriteLine("Push SDK init success, deviceId:" + CloudPushSDK.DeviceId);
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
                    });
                }
                else
                {
                    Console.WriteLine("Push SDK init failed, error:" + res.Error);
                }
            });
        }

        #region  注册推送消息到来监听

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

        #region  App处于启动状态时，通知打开回调

        public static (string, UIApplicationState) DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            // 通知打开回执上报
            CloudPushSDK.SendNotificationAck(userInfo);
            //active正在打开状态，还没做
            // 应用在前台 或者后台开启状态下，直接跳转页面。
            if (application.ApplicationState == UIApplicationState.Active || application.ApplicationState == UIApplicationState.Inactive || application.ApplicationState == UIApplicationState.Background)
            {
                //取得APNS通知内容
                NSDictionary aps = (NSDictionary)userInfo.ValueForKey((NSString)"aps");
                // 内容
                string content = aps.ValueForKey((NSString)"alert").ToString();
                // 播放声音
                string sound = aps.ValueForKey((NSString)"sound").ToString();
                // badge数量
                //int bajge = aps.ValueForKey((NSString)"badge");
                // 取得Extras字段内容,
                //服务端中Extras字段，key是自己定义的
                string Extras = aps.ValueForKey((NSString)"Extras").ToString();
            }
            return ((NSString)userInfo.ValueForKey((NSString)"Url"), application.ApplicationState);

        }
        #endregion
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