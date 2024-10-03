//
//  UniversalSDKNativeInterface.m
//  UniversalSDKUnityBridge
//
//  Created by coolishbee on 2022/11/09.
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
UNIVERSAL_SDK_EXTERNC void universal_sdk_setup();
void universal_sdk_setup()
{
    [[UniversalSDKWrapper sharedInstance] setupSDK];
}

UNIVERSAL_SDK_EXTERNC void universal_sdk_login(const char* identifier,
                                               int loginType);
void universal_sdk_login(const char* identifier,
                         int loginType)
{
    NSString *nsIdentifier = UniversalSDKMakeNSString(identifier);
    [[UniversalSDKWrapper sharedInstance] login:nsIdentifier
                                           type:loginType];
}

UNIVERSAL_SDK_EXTERNC void universal_sdk_logout();
void universal_sdk_logout()
{
    [[UniversalSDKWrapper sharedInstance] logout];
}

UNIVERSAL_SDK_EXTERNC void universal_sdk_openSafariView(const char* url);
void universal_sdk_openSafariView(const char* url)
{
    NSString *nsUrl = UniversalSDKMakeNSString(url);
    [[UniversalSDKWrapper sharedInstance] openSafariView:nsUrl];
}
