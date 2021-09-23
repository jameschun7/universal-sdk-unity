//
//  UniversalSDKWrapper.h
//  UniversalSDKUnityBridge
//
//  Created by james on 2021/03/07.
//

#import <Foundation/Foundation.h>

@interface UniversalSDKWrapper : NSObject

+ (instancetype)sharedInstance;

- (void)setupSDK:(NSString *)identifier
            list:(NSString *)list;

- (void)login:(NSString *)identifier
         type:(int)loginType;

- (void)logout:(NSString *)identifier;

- (void)purchaseLaunch:(NSString *)identifier
                   pid:(NSString *)pid;

- (void)openSafariView:(NSString *)identifier
                   url:(NSString *)url;

@end
