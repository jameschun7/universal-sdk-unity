
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
        private static extern void universal_sdk_setup(string identifier,
                                                       string list);
        public static void SetupSDK(string identifier,
                                    string list)
        {
            Debug.Log(list);
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(null)) { return; }
            
            universal_sdk_setup(identifier, list);
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
        private static extern void universal_sdk_logout(string identifier);
        public static void Logout(string identifier)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            universal_sdk_logout(identifier);
        }

        [DllImport("__Internal")]
        private static extern void universal_sdk_inAppPurchase(string identifier,
                                                               string pid);
        public static void InAppPurchase(string identifier,
                                         string pid)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            universal_sdk_inAppPurchase(identifier, pid);
        }              
        
        [DllImport("__Internal")]
        private static extern void universal_sdk_openSafariView(string identifier,
                                                                string url);
        public static void OpenCustomTabView(string identifier, string url)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            universal_sdk_openSafariView(identifier, url);
        }        

        private static bool IsInvalidRuntime(string identifier)
        {
            return Helpers.IsInvalidRuntime(identifier, RuntimePlatform.IPhonePlayer);
        }
    }
}
#endif