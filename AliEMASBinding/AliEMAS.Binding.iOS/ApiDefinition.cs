using System;

using ObjCRuntime;
using Foundation;
using UIKit;

namespace AliEMAS
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
    }
}

