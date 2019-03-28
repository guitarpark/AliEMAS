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
        public static ICloudPushService Init(Application application, Context context, ICommonCallback callBack, bool debug)
        {
            PushServiceFactory.Init(context);
            PushServiceFactory.CloudPushService.Register(context, callBack);

            IMANService manService = MANServiceProvider.Service;
            if (debug)
                manService.MANAnalytics.TurnOnDebug();
            manService.MANAnalytics.Init(application, context);

            return PushServiceFactory.CloudPushService;
        }

        public void CloseAutoPageTrack()
        {
            MANServiceProvider.Service.MANAnalytics.TurnOffAutoPageTrack();
        }

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
    }
}