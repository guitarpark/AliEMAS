using System;
using System.Collections.Generic;
using System.Text;

namespace AliEMAS.Forms
{
    public interface IAliEMAS
    {
        /// <summary>
        /// 用户注册埋点
        /// </summary>
        /// <param name="data"></param>
        void UserRegister(string data);
        /// <summary>
        /// 用户登录埋点
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        void UserUpdateAccount(string data, string userId);
        /// <summary>
        /// 用户注销
        /// </summary>
        void UserLoginOut();
        /// <summary>
        /// 关闭自动页面埋点
        /// 说明：Mobile Analytics SDK默认会自动采集Android 4.0及以上系统的Activity 页面，如果不需要自动采集可使用下面方法关闭自动页面打点。
        /// 打开页面自动埋点时，默认页面名称为class.getSimpleName()并去除Activity后缀。
        /// </summary>
        void CloseAutoPageTrack();

        #region 手工埋点
        //void PageAppear(Activity activity);
        ///// <summary>
        ///// 页面退出的时候
        ///// </summary>
        ///// <param name="activity"></param>
        //void PageDisAppear(Activity activity);
        ///// <summary>
        ///// 针对页面的自定义属性埋点，比如：同样一个页面，不同的商品信息
        ///// </summary>
        ///// <param name="data"></param>
        //void PageProperties(Dictionary<string, string> data);
        ///// <summary>
        ///// 自定义页面埋点
        ///// </summary>
        ///// <param name="pageName">当前页面</param>
        ///// <param name="referPageName">来源页面</param>
        ///// <param name="duration">页面停留时间</param>
        ///// <param name="properties">自定义属性</param>
        //void CustomPage(string pageName, string referPageName, long duration, Dictionary<string, string> properties);
        #endregion

        #region 网络性能统计
        /// <summary>
        /// 附加信息
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        //void NetworkExtraInfo(MANNetworkPerformanceHitBuilder builder, string key, string value);
        //void NetworkPerClose(MANNetworkPerformanceHitBuilder builder, int length);
        //MANNetworkPerformanceHitBuilder NetworkPerCreate(string url, string method);
        //void NetworkPerOpen(MANNetworkPerformanceHitBuilder builder);
        #endregion
    }
}
