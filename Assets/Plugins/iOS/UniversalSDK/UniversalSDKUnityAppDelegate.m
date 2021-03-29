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
    [[SDKApplicationDelegate sharedInstance] application:application didFinishLaunchingWithOptions:options];
    
    NSLog(@"[UniversalSDKUnityAppDelegate application:%@ didFinishLaunchingWithOptions:%@]", application, options);
    return [super application:application didFinishLaunchingWithOptions:options];
}

@end
