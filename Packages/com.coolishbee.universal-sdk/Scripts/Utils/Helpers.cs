using System;
using UnityEngine;

namespace Universal.UniversalSDK
{
    public static class Helpers
    {
        public static bool IsInvalidRuntime(string identifier, RuntimePlatform platform)
        {
            if (Application.platform != platform)
            {
                Debug.LogWarning("[UniversalSDK] This RuntimePlatform is not supported. Only iOS and Android devices are supported.");
                var errorJson = @"{""code"":-1, ""message"":""Platform not supported.""}";
                var result = CallbackMessageForUnity.WrapValue(identifier, errorJson);
                UniversalSDK.Ins.OnApiError(result);
                return true;
            }
            return false;
        }

        public static void Log(this object value)
        {
            if (UniversalSDKSettings.DevBuild)
                Debug.Log(value.ToString());
        }
    }
}
