//
//  UniversalSDKCallbackPayload.m
//  UniversalSDKUnityBridge
//
//  Created by james on 2021/03/07.
//

#import "UniversalSDKCallbackPayload.h"
#import "UniversalSDKNativeInterface.h"

@interface UniversalSDKCallbackPayload()
@property (nonatomic, copy) NSString *identifier;
@property (nonatomic, copy) NSString *value;
@end

@implementation UniversalSDKCallbackPayload

+ (instancetype)callbackMessage:(NSString *)identifier
                          value:(NSString *)value
{
    return [[self alloc] initWithIdentifier:identifier value:value];
}

- (instancetype)initWithIdentifier:(NSString *)identifier
                             value:(NSString *)value
{
    self = [super init];
    if (self) {
        _identifier = identifier;
        _value = value;
    }
    return self;
}

- (NSString *)generateMessageJson {
    if (!self.identifier || !self.value) {
        NSLog(@"[UniversalSDK] Either `identifier` and `value` is nil. Check response value to make sure a valid result.");
        return nil;
    }

    NSDictionary *dic = @{@"identifier": self.identifier, @"value": self.value};
    NSData *data = [NSJSONSerialization dataWithJSONObject:dic options:kNilOptions error:nil];
    
    if (!data){
        return nil;
    }

    return [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
}

- (void)sendMessageOK {
    universal_sdk_UnitySendMessage("UniversalSDK", "OnApiOk", [self generateMessageJson]);
}

- (void)sendMessageError {
    universal_sdk_UnitySendMessage("UniversalSDK", "OnApiError", [self generateMessageJson]);
}

@end
