
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

        public static void SetupSDK(string identifier,
                                    string list)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(null)) { return; }

            object[] param = new object[2];
            param[0] = identifier;
            param[1] = list;

            if (universalSdkWrapper != null)
                universalSdkWrapper.Call("setupSDK", param);
        }

        public static void Login(string identifier,
                                 LoginType loginType,
                                 AccountServiceType serviceType)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            object[] param = new object[3];
            param[0] = identifier;
            param[1] = (int)loginType;
            param[2] = (int)serviceType;

            if (universalSdkWrapper != null)
                universalSdkWrapper.Call("login", param);
        }

        public static void Logout(string identifier)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            object[] param = new object[1];
            param[0] = identifier;            

            if (universalSdkWrapper != null)
                universalSdkWrapper.Call("logout", param);
        }

        public static void InAppPurchase(string identifier,
                                         string pid)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            object[] param = new object[2];
            param[0] = identifier;
            param[1] = pid;

            if (universalSdkWrapper != null)
                universalSdkWrapper.Call("purchaseLaunch", param);
        }

        public static void ImageBanner(string identifier,
                                       string ratioWidth,
                                       string ratioHeight,
                                       string imageUrl)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            object[] param = new object[4];
            param[0] = identifier;
            param[1] = ratioWidth;
            param[2] = ratioHeight;
            param[3] = imageUrl;

            if (universalSdkWrapper != null)
                universalSdkWrapper.Call("imageBanner", param);
        }       

        public static void OpenCustomTabView(string identifier,
                                             string url)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            object[] param = new object[2];
            param[0] = identifier;
            param[1] = url;

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