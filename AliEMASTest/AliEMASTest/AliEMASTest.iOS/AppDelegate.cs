using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using ObjCRuntime;
using UIKit;

namespace AliEMASTest.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            var result = AliEMAS.iOS.iOSServices.Init("", "", app, options, true);
            return base.FinishedLaunching(app, options);
        }
        #region 阿里推送
        /// <summary>
        /// APNs获取DeviceToken成功和失败
        /// 苹果推送注册成功回调，将苹果返回的deviceToken上传到CloudPush服务器
        /// </summary>
        /// <param name="application"></param>
        /// <param name="deviceToken"></param>
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            AliEMAS.Binding.iOS.CloudPushSDK.RegisterDevice(deviceToken, (result) =>
            {
                if (result.Success)
                    Console.WriteLine("Register deviceToken success");
                else
                    Console.WriteLine("Register deviceToken failed = " + result.Error);
            });
            //base.RegisteredForRemoteNotifications(application, deviceToken);
        }
        /// <summary>
        /// 实现注册APNs失败接口（可选）
        /// </summary>
        /// <param name="application"></param>
        /// <param name="error"></param>
        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {

            // base.FailedToRegisterForRemoteNotifications(application, error);
        }
        /// <summary>
        /// iOS7之后会走下面的方法
        /// </summary>
        /// <param name="application"></param>
        /// <param name="userInfo"></param>
        /// <param name="completionHandler"></param>
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            AliEMAS.iOS.iOSServices.DidReceiveRemoteNotification(application, userInfo);
            //  base.DidReceiveRemoteNotification(application, userInfo, completionHandler);
        }
        #endregion

        public override void OnActivated(UIApplication uiApplication)
        {
            base.OnActivated(uiApplication);
            //进入程序，移除角标
            uiApplication.ApplicationIconBadgeNumber = 0;
        }
        /// <summary>
        /// App处于启动状态时，通知打开回调
        /// </summary>
        /// <param name="application"></param>
        /// <param name="userInfo"></param>
        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            AliEMAS.iOS.iOSServices.DidReceiveRemoteNotification(application, userInfo);
            //   base.ReceivedRemoteNotification(application, userInfo);
        }
    }
}
