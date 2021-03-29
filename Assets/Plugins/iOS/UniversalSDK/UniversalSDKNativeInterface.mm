//
//  UniversalSDKNativeInterface.m
//  UniversalSDKUnityBridge
//
//  Created by james on 2021/03/07.
//

#import <Foundation/Foundation.h>
#import "UniversalSDKWrapper.h"

#define UNIVERSAL_SDK_EXTERNC extern "C"

// MARK: - Helpers
NSString* UniversalSDKMakeNSString(const char* string)
{
    if(string) {
        return [NSString stringWithUTF8String:string];
    }else{
        return [NSString stringWithUTF8String:""];
    }
}

char* UniversalSDKMakeCString(NSString *str)
{
    const char* string = [str UTF8String];
    if(string == NULL) {
        return NULL;
    }
    
    char *buffer = (char*)malloc(strlen(string)+1);
    strcpy(buffer, string);
    return buffer;
}

UNIVERSAL_SDK_EXTERNC void universal_sdk_UnitySendMessage(const char *name,
                                                          const char *method,
                                                          NSString *params)
{
    UnitySendMessage(name, method, UniversalSDKMakeCString(params));
}

// MARK: - Extern APIs
UNIVERSAL_SDK_EXTERNC void universal_sdk_setup(const char* identifier, const char* list);
void universal_sdk_setup(const char* identifier, const char* list)
{
    NSString *nsIdentifier = UniversalSDKMakeNSString(identifier);
    NSString *nsProductList = UniversalSDKMakeNSString(list);
    [[UniversalSDKWrapper sharedInstance] setupSDK:nsIdentifier
                                              list:nsProductList];
}

UNIVERSAL_SDK_EXTERNC void universal_sdk_login(const char* identifier,
                                               int loginType,
                                               int serviceType);
void universal_sdk_login(const char* identifier,
                         int loginType,
                         int serviceType)
{
    NSString *nsIdentifier = UniversalSDKMakeNSString(identifier);
    [[UniversalSDKWrapper sharedInstance] login:nsIdentifier
                                           type:loginType
                                    serviceType:serviceType];
}

UNIVERSAL_SDK_EXTERNC void universal_sdk_logout(const char* identifier, int loginType);
void universal_sdk_logout(const char* identifier, int loginType)
{
    NSString *nsIdentifier = UniversalSDKMakeNSString(identifier);
    [[UniversalSDKWrapper sharedInstance] logout:nsIdentifier
                                       loginType:loginType];
}

UNIVERSAL_SDK_EXTERNC void universal_sdk_inAppPurchase(const char* identifier,
                                                       const char* pid);
void universal_sdk_inAppPurchase(const char* identifier,
                                 const char* pid)
{
    NSString *nsIdentifier = UniversalSDKMakeNSString(identifier);
    NSString *nsPid = UniversalSDKMakeNSString(pid);
    [[UniversalSDKWrapper sharedInstance] purchaseLaunch:nsIdentifier
                                                     pid:nsPid];
}

UNIVERSAL_SDK_EXTERNC void universal_sdk_imageBanner(const char* identifier,
                                                     const char* ratioWidth,
                                                     const char* ratioHeight,
                                                     const char* imageUrl);
void universal_sdk_imageBanner(const char* identifier,
                               const char* ratioWidth,
                               const char* ratioHeight,
                               const char* imageUrl)
{
    NSString *nsIdentifier = UniversalSDKMakeNSString(identifier);
    NSString *nsRatioWidth = UniversalSDKMakeNSString(ratioWidth);
    NSString *nsRatioHeight = UniversalSDKMakeNSString(ratioHeight);
    NSString *nsImageUrl = UniversalSDKMakeNSString(imageUrl);
    [[UniversalSDKWrapper sharedInstance] imageBanner:nsIdentifier
                                           ratioWidth:nsRatioWidth
                                          ratioHeight:nsRatioHeight
                                             imageUrl:nsImageUrl];
}

UNIVERSAL_SDK_EXTERNC void universal_sdk_openSafariView(const char* identifier,
                                                        const char* url);
void universal_sdk_openSafariView(const char* identifier, const char* url)
{
    NSString *nsIdentifier = UniversalSDKMakeNSString(identifier);
    NSString *nsUrl = UniversalSDKMakeNSString(url);
    [[UniversalSDKWrapper sharedInstance] openSafariView:nsIdentifier
                                                     url:nsUrl];
}
