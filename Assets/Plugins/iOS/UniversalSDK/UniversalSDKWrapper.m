//
//  UniversalSDKWrapper.m
//  UniversalSDKUnityBridge
//
//  Created by james on 2021/03/07.
//

#import "UniversalSDKWrapper.h"
#import "UniversalSDKCallbackPayload.h"

@import UniversalSDK;

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

- (void) setupSDK:(NSString *)identifier
             list:(NSString *)list
{
    if(self.setup) {
        return;
    }
    self.setup = YES;
    
    [[UniversalApiClient getInstance] setupSDK:list
                                    completion:^(NSString * _Nullable result,
                                                 NSError * _Nullable error)
    {
        if(error)
        {
            UniversalSDKCallbackPayload *payload = [UniversalSDKCallbackPayload callbackMessage:identifier value:[self wrapError:error]];
            [payload sendMessageError];
        }else{
            UniversalSDKCallbackPayload *payload = [UniversalSDKCallbackPayload callbackMessage:identifier value:result];
            [payload sendMessageOK];
        }
    }];
    
    [[WebviewController GetInstance] InitializeWithParentUIView:UnityGetGLViewController().view
                                                    pubDelegate:nil];
}

- (void) login:(NSString *)identifier
          type:(int)loginType
   serviceType:(int)accountServiceType
{
    [[UniversalApiClient getInstance]login:loginType
                               serviceType:accountServiceType
                            viewController:UnityGetGLViewController()
                                completion:^(NSString * _Nullable result, NSError * _Nullable error)
    {
        if(error){
            UniversalSDKCallbackPayload *payload = [UniversalSDKCallbackPayload callbackMessage:identifier value:[self wrapError:error]];
            [payload sendMessageError];
        }else{
            UniversalSDKCallbackPayload *payload = [UniversalSDKCallbackPayload callbackMessage:identifier value:result];
            [payload sendMessageOK];
        }
    }];    
}

- (void) logout:(NSString *)identifier      
{
    [[UniversalApiClient getInstance] logout:^(NSString * _Nullable result, NSError * _Nullable error)
    {
        if(error){
            UniversalSDKCallbackPayload *payload = [UniversalSDKCallbackPayload callbackMessage:identifier value:[self wrapError:error]];
            [payload sendMessageError];
        }else{
            UniversalSDKCallbackPayload *payload = [UniversalSDKCallbackPayload callbackMessage:identifier value:result];
            [payload sendMessageOK];
        }
    }];
}

- (void) purchaseLaunch:(NSString *)identifier
                    pid:(NSString *)pid
{
    [[UniversalApiClient getInstance] purchaseLaunch:pid
                                          completion:^(NSString * _Nullable result, NSError * _Nullable error)
    {
        if(error){
            UniversalSDKCallbackPayload *payload = [UniversalSDKCallbackPayload callbackMessage:identifier value:[self wrapError:error]];
            [payload sendMessageError];
        }else{
            UniversalSDKCallbackPayload *payload = [UniversalSDKCallbackPayload callbackMessage:identifier value:result];
            [payload sendMessageOK];
        }
    }];
}

- (void) imageBanner:(NSString *)identifier
          ratioWidth:(NSString *)ratioWidth
         ratioHeight:(NSString *)ratioHeight
            imageUrl:(NSString *)imageUrl
{
    [[UniversalApiClient getInstance] imageBanner:imageUrl
                                       ratioWidth:ratioWidth
                                      ratioHeight:ratioHeight];
}

- (void) openSafariView:(NSString *)identifier
                    url:(NSString *)url
{
    [[UniversalApiClient getInstance] openSafariView:UnityGetGLViewController()
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
