
#if UNITY_ANDROID
using UnityEngine;

namespace Universal.UniversalSDK
{
    public class NativeInterface
    {
#if UNITY_EDITOR
        static AndroidJavaObject universalSdkWrapper = null;
#else
        static AndroidJavaObject universalSdkWrapper = new AndroidJavaObject("com.universal.sdk.unitywrapper.UniversalSdkWrapper");
#endif
        static NativeInterface()
        {
            var _ = UniversalSDK.Ins;
        }

        public static void SetupSDK()
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(null)) { return; }

            if (universalSdkWrapper != null)
                universalSdkWrapper.Call("setupSDK");
        }

        public static void Login(string identifier,
                                 LoginType loginType)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            object[] param = new object[2];
            param[0] = identifier;
            param[1] = (int)loginType;

            if (universalSdkWrapper != null)
                universalSdkWrapper.Call("login", param);
        }

        public static void Logout()
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(null)) { return; }            

            if (universalSdkWrapper != null)
                universalSdkWrapper.Call("logout");
        }

        public static void OpenCustomTabView(string url)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(null)) { return; }

            object[] param = new object[1];
            param[0] = url;            

            if (universalSdkWrapper != null)
                universalSdkWrapper.Call("openCustomTabView", param);
        }       

        private static bool IsInvalidRuntime(string identifier)
        {
            return Helpers.IsInvalidRuntime(identifier, RuntimePlatform.Android);
        }
    }
}

#endif