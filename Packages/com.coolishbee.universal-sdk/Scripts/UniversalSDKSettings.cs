using System;
using UnityEngine;

namespace Universal.UniversalSDK
{
    public class UniversalSDKSettings : ScriptableObject
    {
        public const string settingsAssetName = "UniversalSDKSettings";
        public const string settingsPath = "UniversalSDK/Resources";
        public const string settingsAssetExtension = ".asset";
        public const string sdkVersion = "1.3.4";

        private static UniversalSDKSettings instance;

        public static void SetInstance(UniversalSDKSettings settings)
        {
            instance = settings;
        }

        public static UniversalSDKSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load(settingsAssetName) as UniversalSDKSettings;
                    if (instance == null)
                    {
                        instance = CreateInstance<UniversalSDKSettings>();
                    }
                }
                return instance;
            }
        }

        [SerializeField]
        private bool devBuild = true;

        [SerializeField]
        private bool iOSAppleLogin = false;
        [SerializeField]
        private bool iOSFacebookLogin = false;
        [SerializeField]
        private bool iOSGoogleLogin = false;

        [SerializeField]
        private string iOSFacebookAppID = "";
        [SerializeField]
        private string iOSFacebookClientToken = "";
        [SerializeField]
        private string iOSGoogleClientID = "";
        [SerializeField]
        private string iOSReversedClientID = "";
        [SerializeField]
        private string iOSGoogleServerClientID = "";

        public static bool DevBuild
        {
            get { return Instance.devBuild; }
            set { Instance.devBuild = value; }
        }

        public static bool UseAppleLogin
        {
            get { return Instance.iOSAppleLogin; }
            set { Instance.iOSAppleLogin = value; }
        }

        public static bool UseFacebookLogin
        {
            get { return Instance.iOSFacebookLogin; }
            set { Instance.iOSFacebookLogin = value; }
        }

        public static bool UseGoogleLogin
        {
            get { return Instance.iOSGoogleLogin; }
            set { Instance.iOSGoogleLogin = value; }
        }

        public static string FacebookAppID
        {
            get { return Instance.iOSFacebookAppID; }
            set { Instance.iOSFacebookAppID = value; }
        }

        public static string FacebookClientToken
        {
            get { return Instance.iOSFacebookClientToken; }
            set { Instance.iOSFacebookClientToken = value; }
        }

        public static string GoogleClientID
        {
            get { return Instance.iOSGoogleClientID; }
            set { Instance.iOSGoogleClientID = value; }
        }

        public static string ReversedClientID
        {
            get { return Instance.iOSReversedClientID; }
            set { Instance.iOSReversedClientID = value; }
        }

        public static string GoogleServerClientID
        {
            get { return Instance.iOSGoogleServerClientID; }
            set { Instance.iOSGoogleServerClientID = value; }
        }
    }
}