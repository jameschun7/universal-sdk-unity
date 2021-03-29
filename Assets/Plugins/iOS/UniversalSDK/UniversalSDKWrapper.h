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
         type:(int)loginType
  serviceType:(int)accountServiceType;

- (void)logout:(NSString *)identifier
     loginType:(int)loginType;

- (void)purchaseLaunch:(NSString *)identifier
                   pid:(NSString *)pid;

- (void)imageBanner:(NSString *)identifier
         ratioWidth:(NSString *)ratioWidth
        ratioHeight:(NSString *)ratioHeight
           imageUrl:(NSString *)imageUrl;

- (void)openSafariView:(NSString *)identifier
                   url:(NSString *)url;

@end
