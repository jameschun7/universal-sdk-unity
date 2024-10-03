
#if UNITY_IOS

using UnityEngine;
using System;
using System.Runtime.InteropServices;
using AOT;
using System.Reflection;

namespace Universal.UniversalSDK
{
    public class NativeInterface
    {
        static NativeInterface()
        {
            var _ = UniversalSDK.Ins;
        }

        [DllImport("__Internal")]
        private static extern void universal_sdk_setup();
        public static void SetupSDK()
        {            
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(null)) { return; }
            
            universal_sdk_setup();
        }

        [DllImport("__Internal")]
        private static extern void universal_sdk_login(string identifier,
                                                       int loginType);
        public static void Login(string identifier,
                                 LoginType loginType)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            universal_sdk_login(identifier, (int)loginType);
        }

        [DllImport("__Internal")]
        private static extern void universal_sdk_logout();
        public static void Logout()
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(null)) { return; }

            universal_sdk_logout();
        }
        
        [DllImport("__Internal")]
        private static extern void universal_sdk_openSafariView(string url);
        public static void OpenCustomTabView(string url)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(null)) { return; }

            universal_sdk_openSafariView(url);
        }

        private static bool IsInvalidRuntime(string identifier)
        {
            return Helpers.IsInvalidRuntime(identifier, RuntimePlatform.IPhonePlayer);
        }
    }
}
#endif