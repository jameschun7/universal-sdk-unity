//
//  UniversalSDKUnityAppDelegate.m
//  UniversalSDKUnityBridge
//
//  Created by james on 2021/03/07.
//

#import <Foundation/Foundation.h>
#import "UnityAppController.h"
@import UniversalSDK;

@interface UniversalSDKUnityAppDelegate : UnityAppController
@end

IMPL_APP_CONTROLLER_SUBCLASS(UniversalSDKUnityAppDelegate)

@implementation UniversalSDKUnityAppDelegate

-(BOOL)application:(UIApplication*) application didFinishLaunchingWithOptions:(NSDictionary*) options
{
    if (@available(iOS 10.0, *)) {
        if ([UNUserNotificationCenter class] != nil) {
            // iOS 10 or later
            // For iOS 10 display notification (sent via APNS)
            [UNUserNotificationCenter currentNotificationCenter].delegate = self;
            UNAuthorizationOptions authOptions = UNAuthorizationOptionAlert |
            UNAuthorizationOptionSound | UNAuthorizationOptionBadge;
            
            [[UNUserNotificationCenter currentNotificationCenter]
             requestAuthorizationWithOptions:authOptions
             completionHandler:^(BOOL granted,NSError * _Nullable error)
            {
                if(error){
                    NSLog(@"%@", error);
                }else{
                    dispatch_async(dispatch_get_main_queue(),^{
                        [[UIApplication sharedApplication] registerForRemoteNotifications];
                    });
                }
            }];
        }
    }
    
    [[SDKApplicationDelegate sharedInstance] application:application didFinishLaunchingWithOptions:options];        
    return [super application:application didFinishLaunchingWithOptions:options];
}

-(void)application:(UIApplication *)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData *)deviceToken {
    const unsigned char *dataBuffer = (const unsigned char *)deviceToken.bytes;
    NSMutableString *hexString  = [NSMutableString stringWithCapacity:(deviceToken.length * 2)];
    for (int i = 0; i < deviceToken.length; ++i) {
        [hexString appendFormat:@"%02x", dataBuffer[i]];
    }
    NSString *result = [hexString copy];
    NSLog(@"token : %@", result);
    
    [[UniversalApiClient getInstance] setDeviceToken:result];
}

- (void)userNotificationCenter:(UNUserNotificationCenter *)center
       willPresentNotification:(UNNotification *)notification
         withCompletionHandler:(void (^)(UNNotificationPresentationOptions))completionHandler  API_AVAILABLE(ios(10.0)) {
    NSDictionary *userInfo = notification.request.content.userInfo;
    NSLog(@"push data : %@", userInfo);
    if (@available(iOS 14.0, *)) {
        completionHandler(UNNotificationPresentationOptionList);
    } else {
        // 포그라운드 상태에서 푸시왔을 때 푸시 마노출
        completionHandler(UNNotificationPresentationOptionNone);
    }
}

- (void)userNotificationCenter:(UNUserNotificationCenter *)center
didReceiveNotificationResponse:(UNNotificationResponse *)response
         withCompletionHandler:(void(^)(void))completionHandler  API_AVAILABLE(ios(10.0)){
    NSDictionary *userInfo = response.notification.request.content.userInfo;
    NSLog(@"push data : %@", userInfo);
    completionHandler();
}

@end
