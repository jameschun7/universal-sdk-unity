//
//  UniversalSDKWrapper.h
//  UniversalSDKUnityBridge
//
//  Created by coolishbee on 2022/11/09.
//

#import <Foundation/Foundation.h>

@interface UniversalSDKWrapper : NSObject

+ (instancetype)sharedInstance;

- (void)setupSDK;

- (void)login:(NSString *)identifier
         type:(int)loginType;

- (void)logout;

- (void)openSafariView:(NSString *)url;

@end
