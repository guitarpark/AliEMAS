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
using Com.Alibaba.Sdk.Android.Man;
using Com.Alibaba.Sdk.Android.Man.Network;
using Com.Alibaba.Sdk.Android.Push;
using Com.Alibaba.Sdk.Android.Push.Noonesdk;

namespace AliEMAS
{
    /// <summary>
    /// https://help.aliyun.com/document_detail/30037.html
    /// 其余的懒得写了
    /// </summary>
    public class Extensions     {
        /// <summary>
        /// 初始化推送
        /// </summary>
        /// <param name="context"></param>
        /// <param name="callBack"></param>
        /// <returns></returns>        public ICloudPushService InitPush(Context context, ICommonCallback callBack)
        {

            PushServiceFactory.Init(context);
            PushServiceFactory.CloudPushService.Register(context, callBack);
            return PushServiceFactory.CloudPushService;
        }
        /// <summary>
        /// 初始化数据分析
        /// </summary>
        /// <param name="application"></param>
        /// <param name="context"></param>
        /// <param name="debug">是否在控制台输出日志</param>
        public void InitMan(Application application, Context context, bool debug)
        {
            IMANService manService = MANServiceProvider.Service;
            if (debug)
                manService.MANAnalytics.TurnOnDebug();
            manService.MANAnalytics.Init(application, context);
        }
        /// <summary>
        /// 用户注册埋点
        /// </summary>
        /// <param name="data"></param>
        public void UserRegister(string data)
        {
            MANServiceProvider.Service.MANAnalytics.UserRegister(data);
        }
        /// <summary>
        /// 用户登录埋点
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        public void UserUpdateAccount(string data, string userId)
        {
            MANServiceProvider.Service.MANAnalytics.UpdateUserAccount(data, userId);
        }
        /// <summary>
        /// 用户注销
        /// </summary>
        public void UserLoginOut()
        {
            MANServiceProvider.Service.MANAnalytics.UpdateUserAccount("", "");
        }
        /// <summary>
        /// 关闭自动页面埋点
        /// 说明：Mobile Analytics SDK默认会自动采集Android 4.0及以上系统的Activity 页面，如果不需要自动采集可使用下面方法关闭自动页面打点。
        /// 打开页面自动埋点时，默认页面名称为class.getSimpleName()并去除Activity后缀。
        /// </summary>
        public void CloseAutoPageTrack()
        {
            MANServiceProvider.Service.MANAnalytics.TurnOffAutoPageTrack();
        }

        #region 手工埋点
        /// <summary>
        /// 页面显示的时候
        /// </summary>
        /// <param name="activity"></param>
        public void PageAppear(Activity activity)
        {
            MANServiceProvider.Service.MANPageHitHelper.PageAppear(activity);
        }
        /// <summary>
        /// 页面退出的时候
        /// </summary>
        /// <param name="activity"></param>
        public void PageDisAppear(Activity activity)
        {
            MANServiceProvider.Service.MANPageHitHelper.PageDisAppear(activity);
        }
        /// <summary>
        /// 针对页面的自定义属性埋点，比如：同样一个页面，不同的商品信息
        /// </summary>
        /// <param name="data"></param>
        public void PageProperties(Dictionary<string,string> data)
        {
            MANServiceProvider.Service.MANPageHitHelper.UpdatePageProperties(data);
        }
        /// <summary>
        /// 自定义页面埋点
        /// </summary>
        /// <param name="pageName">当前页面</param>
        /// <param name="referPageName">来源页面</param>
        /// <param name="duration">页面停留时间</param>
        /// <param name="properties">自定义属性</param>
        public void CustomPage(string pageName,string referPageName,long duration,Dictionary<string, string> properties)
        {
            MANPageHitBuilder pageHitBuilder = new MANPageHitBuilder(pageName);
            pageHitBuilder.SetReferPage(referPageName);
            pageHitBuilder.SetDurationOnPage(duration);
            pageHitBuilder.SetProperties(properties);
            MANServiceProvider.Service.MANAnalytics.DefaultTracker.Send(pageHitBuilder.Build());

        }
        #endregion

        #region 网络性能统计
        /// <summary>
        /// 创建网络统计
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <returns></returns>

        public MANNetworkPerformanceHitBuilder NetworkPerCreate(string url,string method)
        {
            return new MANNetworkPerformanceHitBuilder(url, method);
        }
        /// <summary>
        /// 打开网络统计
        /// </summary>
        /// <param name="builder"></param>
        public void NetworkPerOpen(MANNetworkPerformanceHitBuilder  builder)
        {
            builder.HitRequestStart();
        }
        /// <summary>
        /// 附加信息
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void NetworkExtraInfo(MANNetworkPerformanceHitBuilder builder,string key,string value)
        {
            builder.WithExtraInfo(key, value);
        }
        /// <summary>
        /// 网络请求正常结束，统计请求响应时间
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="length"></param>
        public void NetworkPerClose(MANNetworkPerformanceHitBuilder builder,int length)
        {
            builder.HitRequestEndWithLoadBytes(length);
        }
        #endregion
    }
}