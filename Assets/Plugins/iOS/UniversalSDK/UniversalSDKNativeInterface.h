//
//  UniversalSDKNativeInterface.h
//  UniversalSDKUnityBridge
//
//  Created by james on 2021/03/07.
//

#ifndef UniversalSDKNativeInterface_h
#define UniversalSDKNativeInterface_h

#if __cplusplus
extern "C"
{
#endif /* __cplusplus */

    void universal_sdk_UnitySendMessage(const char *name, const char *method, NSString *params);

#if __cplusplus
}
#endif /* __cplusplus */

#endif /* UniversalSDKNativeInterface_h */
