//
//  UniversalSDKCallbackPayload.h
//  UniversalSDKUnityBridge
//
//  Created by james on 2021/03/07.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface UniversalSDKCallbackPayload : NSObject

+ (instancetype)callbackMessage:(NSString *)identifier value:(NSString *)value;

- (instancetype)initWithIdentifier:(NSString *)identifier value:(NSString *)value;
- (void)sendMessageOK;
- (void)sendMessageError;

@end

NS_ASSUME_NONNULL_END
