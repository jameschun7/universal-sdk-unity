//
//  UniversalSDKCallbackPayload.h
//  UniversalSDKUnityBridge
//
//  Created by coolishbee on 2022/11/09.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface UniversalSDKCallbackPayload : NSObject

+ (instancetype)payloadWithIdentifier:(NSString *)identifier value:(NSString *)value;

- (instancetype)initWithIdentifier:(NSString *)identifier value:(NSString *)value;
- (void)sendMessageOK;
- (void)sendMessageError;

@end

NS_ASSUME_NONNULL_END
