//
//  UniversalSDKWrapper.m
//  UniversalSDKUnityBridge
//
//  Created by coolishbee on 2022/11/09.
//

#import "UniversalSDKWrapper.h"
#import "UniversalSDKCallbackPayload.h"

@import UniversalSDKSwift;

@interface UniversalSDKWrapper()
@property (nonatomic, assign) BOOL setup;
@end

@implementation UniversalSDKWrapper
+ (instancetype)sharedInstance
{
    static UniversalSDKWrapper *sharedInstance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        sharedInstance = [[UniversalSDKWrapper alloc] init];
    });
    return sharedInstance;
}

- (void) setupSDK
{
    if(self.setup) {
        return;
    }
    self.setup = YES;
    
    [[UniversalAPIClient shared] setupSDK];
}

- (void) login:(NSString *)identifier
          type:(int)loginType
{
    [[UniversalAPIClient shared] socialLoginWithLoginType:loginType
                                         inViewController:UnityGetGLViewController()
                                        completionHandler:^(SDKLoginResult *result,
                                                            NSError *error)
     {
        if(error){
            UniversalSDKCallbackPayload *payload = [UniversalSDKCallbackPayload payloadWithIdentifier:identifier value:[self wrapError:error]];
            [payload sendMessageError];
        }else{
            UniversalSDKCallbackPayload *payload = [UniversalSDKCallbackPayload payloadWithIdentifier:identifier value:[result json]];
            [payload sendMessageOK];
        }
    }];
}

- (void) logout
{
    
}

- (void) openSafariView:(NSString *)url
{
    [[UniversalAPIClient shared] openSafariViewWithVc:UnityGetGLViewController()
                                                  url:url];
}

- (NSString *)wrapError:(NSError *)error
{
    NSDictionary *dic = @{@"code": @(error.code), @"message": error.localizedDescription};
    NSData *data = [NSJSONSerialization dataWithJSONObject:dic options:kNilOptions error:nil];
    if(!data) { return nil; }
    return [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
}

@end
