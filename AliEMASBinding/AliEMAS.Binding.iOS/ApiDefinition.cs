using System;

using ObjCRuntime;
using Foundation;
using UIKit;

namespace AliEMAS.Binding.iOS
{
    // @interface ALBBMANAnalytics : NSObject
    [BaseType(typeof(NSObject))]
    interface ALBBMANAnalytics
    {
        // +(ALBBMANAnalytics *)getInstance;
        [Static]
        [Export("getInstance")]
        ALBBMANAnalytics Instance { get; }

        // -(void)autoInit;
        [Export("autoInit")]
        void AutoInit();

        // -(void)initWithAppKey:(NSString *)appKey secretKey:(NSString *)secretKey;
        [Export("initWithAppKey:secretKey:")]
        void InitWithAppKey(string appKey, string secretKey);

        // -(void)turnOnDebug;
        [Export("turnOnDebug")]
        void TurnOnDebug();

        // -(void)setAppVersion:(NSString *)pAppVersion;
        [Export("setAppVersion:")]
        void SetAppVersion(string pAppVersion);

        // -(void)setChannel:(NSString *)pChannel;
        [Export("setChannel:")]
        void SetChannel(string pChannel);

        // -(void)updateUserAccount:(NSString *)pNick userid:(NSString *)pUserId;
        [Export("updateUserAccount:userid:")]
        void UpdateUserAccount(string pNick, string pUserId);

        // -(void)userRegister:(NSString *)pUsernick;
        [Export("userRegister:")]
        void UserRegister(string pUsernick);

        // -(void)turnOffCrashHandler;
        [Export("turnOffCrashHandler")]
        void TurnOffCrashHandler();

        // -(ALBBMANTracker *)getDefaultTracker;
        [Export("getDefaultTracker")]
        ALBBMANTracker DefaultTracker { get; }
    }


    // @interface ALBBMANPageHitHelper : NSObject
    [BaseType(typeof(NSObject))]
    interface ALBBMANPageHitHelper
    {
        // +(instancetype)getInstance;
        [Static]
        [Export("getInstance")]
        ALBBMANPageHitHelper GetInstance();

        // -(void)pageAppear:(UIViewController *)pViewController;
        [Export("pageAppear:")]
        void PageAppear(UIViewController pViewController);

        // -(void)pageDisAppear:(UIViewController *)pViewController;
        [Export("pageDisAppear:")]
        void PageDisAppear(UIViewController pViewController);

        // -(void)updatePageProperties:(UIViewController *)pViewController properties:(NSDictionary *)pProperties;
        [Export("updatePageProperties:properties:")]
        void UpdatePageProperties(UIViewController pViewController, NSDictionary pProperties);
    }

    // @interface ALBBMANPageHitBuilder

    [BaseType(typeof(NSObject))]
    interface ALBBMANPageHitBuilder
    {
        // -(void)setProperty:(NSString *)pKey value:(NSString *)pValue;
        [Export("setProperty:value:")]
        void SetProperty(string pKey, string pValue);

        // -(void)setProperties:(NSDictionary *)pPageProperties;
        [Export("setProperties:")]
        void SetProperties(NSDictionary pPageProperties);

        // -(void)setPageName:(NSString *)pPageName;
        [Export("setPageName:")]
        void SetPageName(string pPageName);

        // -(void)setReferPage:(NSString *)pReferPageName;
        [Export("setReferPage:")]
        void SetReferPage(string pReferPageName);

        // -(void)setDurationOnPage:(long long)durationTimeOnPage;
        [Export("setDurationOnPage:")]
        void SetDurationOnPage(long durationTimeOnPage);
    }

    // @interface ALBBMANTracker
    [BaseType(typeof(NSObject))]
    interface ALBBMANTracker
    {
        // -(id)initWithAppKey:(NSString *)appKey;
        [Export("initWithAppKey:")]
        IntPtr Constructor(string appKey);

        // -(void)setGlobalProperty:(NSString *)pKey value:(NSString *)pValue;
        [Export("setGlobalProperty:value:")]
        void SetGlobalProperty(string pKey, string pValue);

        // -(void)removeGlobalProperty:(NSString *)pKey;
        [Export("removeGlobalProperty:")]
        void RemoveGlobalProperty(string pKey);

        // -(NSString *)getGlobalProperty:(NSString *)pKey;
        [Export("getGlobalProperty:")]
        string GetGlobalProperty(string pKey);

        // -(void)send:(NSDictionary *)pLogDict;
        [Export("send:")]
        void Send(NSDictionary pLogDict);
    }

    // @interface ALBBMANNetworkHitBuilder : NSObject
    [BaseType(typeof(NSObject))]
    interface ALBBMANNetworkHitBuilder
    {
        // -(instancetype)initWithHost:(NSString *)host method:(NSString *)method;
        [Export("initWithHost:method:")]
        IntPtr Constructor(string host, string method);

        // -(void)setProperty:(NSString *)pKey value:(NSString *)pValue;
        [Export("setProperty:value:")]
        void SetProperty(string pKey, string pValue);

        // -(void)setproperties:(NSMutableDictionary *)properties;
        [Export("setproperties:")]
        void Setproperties(NSMutableDictionary properties);

        // -(void)requestStart;
        [Export("requestStart")]
        void RequestStart();

        // -(void)connectFinished;
        [Export("connectFinished")]
        void ConnectFinished();

        // -(void)requestFirstBytes;
        [Export("requestFirstBytes")]
        void RequestFirstBytes();

        // -(void)requestEndWithBytes:(long)loadBytes;
        [Export("requestEndWithBytes:")]
        void RequestEndWithBytes(nint loadBytes);

        // -(void)requestEndWithError:(ALBBMANNetworkError *)error;
        [Export("requestEndWithError:")]
        void RequestEndWithError(ALBBMANNetworkError error);

        // -(NSDictionary *)build;
        [Export("build")]

        NSDictionary Build { get; }
    }

    // @interface ALBBMANNetworkError : NSObject
    [BaseType(typeof(NSObject))]
    interface ALBBMANNetworkError
    {
        // @property (nonatomic, strong) NSMutableDictionary * properties;
        [Export("properties", ArgumentSemantic.Strong)]
        NSMutableDictionary Properties { get; set; }

        // +(ALBBMANNetworkError *)ErrorWithHttpException4;
        [Static]
        [Export("ErrorWithHttpException4")]
        ALBBMANNetworkError ErrorWithHttpException4 { get; }

        // +(ALBBMANNetworkError *)ErrorWithHttpException5;
        [Static]
        [Export("ErrorWithHttpException5")]
        ALBBMANNetworkError ErrorWithHttpException5 { get; }

        // +(ALBBMANNetworkError *)ErrorWithSocketTimeout;
        [Static]
        [Export("ErrorWithSocketTimeout")]
        ALBBMANNetworkError ErrorWithSocketTimeout { get; }

        // +(ALBBMANNetworkError *)ErrorWithIOInterrupted;
        [Static]
        [Export("ErrorWithIOInterrupted")]
        ALBBMANNetworkError ErrorWithIOInterrupted { get; }

        // -(id)initWithErrorCode:(NSString *)code;
        [Export("initWithErrorCode:")]
        IntPtr Constructor(string code);

        // -(void)setProperty:(NSString *)property value:(NSString *)value;
        [Export("setProperty:value:")]
        void SetProperty(string property, string value);
    }

    // @interface NSMutableURLRequest : NSURLRequest
    [BaseType(typeof(NSUrlRequest))]
    interface NSMutableURLRequest
    {
        // @property (copy) NSURL * _Nullable URL;
        [NullAllowed, Export("URL", ArgumentSemantic.Copy)]
        NSUrl URL { get; set; }

        // @property NSURLRequestCachePolicy cachePolicy;
        //[Export("cachePolicy", ArgumentSemantic.Assign)]
        //NSURLRequestCachePolicy CachePolicy { get; set; }

        // @property NSTimeInterval timeoutInterval;
        [Export("timeoutInterval")]
        double TimeoutInterval { get; set; }

        // @property (copy) NSURL * _Nullable mainDocumentURL;
        [NullAllowed, Export("mainDocumentURL", ArgumentSemantic.Copy)]
        NSUrl MainDocumentURL { get; set; }

        // @property NSURLRequestNetworkServiceType networkServiceType __attribute__((availability(tvos, introduced=9.0))) __attribute__((availability(watchos, introduced=2.0))) __attribute__((availability(ios, introduced=4.0))) __attribute__((availability(macos, introduced=10.7)));
        //[Watch(2, 0), TV(9, 0), Mac(10, 7), iOS(4, 0)]
        //[Export("networkServiceType", ArgumentSemantic.Assign)]
        //NSURLRequestNetworkServiceType NetworkServiceType { get; set; }

        // @property BOOL allowsCellularAccess __attribute__((availability(tvos, introduced=9.0))) __attribute__((availability(watchos, introduced=2.0))) __attribute__((availability(ios, introduced=6.0))) __attribute__((availability(macos, introduced=10.8)));
        [Watch(2, 0), TV(9, 0), Mac(10, 8), iOS(6, 0)]
        [Export("allowsCellularAccess")]
        bool AllowsCellularAccess { get; set; }
    }

    // @interface CloudPushSDK : NSObject
    [BaseType(typeof(NSObject))]
    interface CloudPushSDK
    {
        // +(void)autoInit:(CallbackHandler)callback;
        [Static]
        [Export("autoInit:")]
        void AutoInit(CallbackHandler callback);

        // +(void)asyncInit:(NSString *)appKey appSecret:(NSString *)appSecret callback:(CallbackHandler)callback;
        [Static]
        [Export("asyncInit:appSecret:callback:")]
        void AsyncInit(string appKey, string appSecret, CallbackHandler callback);

        // +(void)turnOnDebug;
        [Static]
        [Export("turnOnDebug")]
        void TurnOnDebug();

        // +(NSString *)getDeviceId;
        [Static]
        [Export("getDeviceId")]
        string DeviceId { get; }

        // +(NSString *)getVersion;
        [Static]
        [Export("getVersion")]
        string Version { get; }

        // +(BOOL)isChannelOpened;
        [Static]
        [Export("isChannelOpened")]
        bool IsChannelOpened { get; }

        // +(void)sendNotificationAck:(NSDictionary *)userInfo;
        [Static]
        [Export("sendNotificationAck:")]
        void SendNotificationAck(NSDictionary userInfo);

        // +(void)handleLaunching:(NSDictionary *)launchOptions __attribute__((deprecated("Use 'sendNotificationAck:' instead")));
        [Static]
        [Export("handleLaunching:")]
        void HandleLaunching(NSDictionary launchOptions);

        // +(void)handleReceiveRemoteNotification:(NSDictionary *)userInfo __attribute__((deprecated("Use 'sendNotificationAck:' instead")));
        [Static]
        [Export("handleReceiveRemoteNotification:")]
        void HandleReceiveRemoteNotification(NSDictionary userInfo);

        // +(void)bindAccount:(NSString *)account withCallback:(CallbackHandler)callback;
        [Static]
        [Export("bindAccount:withCallback:")]
        void BindAccount(string account, CallbackHandler callback);

        // +(void)unbindAccount:(CallbackHandler)callback;
        [Static]
        [Export("unbindAccount:")]
        void UnbindAccount(CallbackHandler callback);

        // +(void)bindTag:(int)target withTags:(NSArray *)tags withAlias:(NSString *)alias withCallback:(CallbackHandler)callback;
        [Static]
        [Export("bindTag:withTags:withAlias:withCallback:")]
        void BindTag(int target, NSObject[] tags, string alias, CallbackHandler callback);

        // +(void)unbindTag:(int)target withTags:(NSArray *)tags withAlias:(NSString *)alias withCallback:(CallbackHandler)callback;
        [Static]
        [Export("unbindTag:withTags:withAlias:withCallback:")]
        void UnbindTag(int target, NSObject[] tags, string alias, CallbackHandler callback);

        // +(void)listTags:(int)target withCallback:(CallbackHandler)callback;
        [Static]
        [Export("listTags:withCallback:")]
        void ListTags(int target, CallbackHandler callback);

        // +(void)addAlias:(NSString *)alias withCallback:(CallbackHandler)callback;
        [Static]
        [Export("addAlias:withCallback:")]
        void AddAlias(string alias, CallbackHandler callback);

        // +(void)removeAlias:(NSString *)alias withCallback:(CallbackHandler)callback;
        [Static]
        [Export("removeAlias:withCallback:")]
        void RemoveAlias(string alias, CallbackHandler callback);

        // +(void)listAliases:(CallbackHandler)callback;
        [Static]
        [Export("listAliases:")]
        void ListAliases(CallbackHandler callback);

        // +(void)registerDevice:(NSData *)deviceToken withCallback:(CallbackHandler)callback;
        [Static]
        [Export("registerDevice:withCallback:")]
        void RegisterDevice(NSData deviceToken, CallbackHandler callback);

        // +(NSString *)getApnsDeviceToken;
        [Static]
        [Export("getApnsDeviceToken")]
        string ApnsDeviceToken { get; }

        // +(void)syncBadgeNum:(NSUInteger)num withCallback:(CallbackHandler)callback;
        [Static]
        [Export("syncBadgeNum:withCallback:")]
        void SyncBadgeNum(nuint num, CallbackHandler callback);
    }

    // typedef void (^CallbackHandler)(CloudPushCallbackResult *);
    delegate void CallbackHandler(CloudPushCallbackResult arg0);

    // @interface CloudPushCallbackResult : NSObject
    [BaseType(typeof(NSObject))]
    interface CloudPushCallbackResult
    {
        // @property (readonly, nonatomic) BOOL success;
        [Export("success")]
        bool Success { get; }

        // @property (readonly, nonatomic) id _Nullable data;
        [NullAllowed, Export("data")]
        NSObject Data { get; }

        // @property (readonly, nonatomic) NSError * _Nullable error;
        [NullAllowed, Export("error")]
        NSError Error { get; }

        // +(instancetype _Nonnull)resultWithData:(id _Nullable)data;
        [Static]
        [Export("resultWithData:")]
        CloudPushCallbackResult ResultWithData([NullAllowed] NSObject data);

        // +(instancetype _Nonnull)resultWithError:(NSError * _Nullable)error;
        [Static]
        [Export("resultWithError:")]
        CloudPushCallbackResult ResultWithError([NullAllowed] NSError error);
    }
}

