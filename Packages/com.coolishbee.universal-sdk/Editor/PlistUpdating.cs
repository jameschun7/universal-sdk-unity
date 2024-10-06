#if UNITY_IOS
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;

namespace Universal.UniversalSDK.Editor
{
    public class PlistUpdating
    {
        [PostProcessBuildAttribute(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            if (target != BuildTarget.iOS)
            {
                return;
            }                        
            
            string plistPath = pathToBuiltProject + "/Info.plist";
            PlistDocument plist = new PlistDocument();
            plist.ReadFromString(File.ReadAllText(plistPath));

            PlistElementDict rootDict = plist.root;            

            SetupURLScheme(rootDict);
            SetupQueriesSchemes(rootDict);
            SetupFacebookSetting(rootDict);
            SetupGoogleSetting(rootDict);

            File.WriteAllText(plistPath, plist.WriteToString());
                        
        }

        static void SetupURLScheme(PlistElementDict rootDict)
        {
            PlistElementArray array = GetOrCreateArray(rootDict, "CFBundleURLTypes");
            var lineURLScheme = array.AddDict();
            lineURLScheme.SetString("CFBundleTypeRole", "Editor");
            lineURLScheme.SetString("CFBundleURLName", "Client");
            var schemes = lineURLScheme.CreateArray("CFBundleURLSchemes");

            if (UniversalSDKSettings.UseGoogleLogin)
                schemes.AddString(UniversalSDKSettings.ReversedClientID);

            if (UniversalSDKSettings.UseFacebookLogin)
                schemes.AddString("fb"+ UniversalSDKSettings.FacebookAppID);
        }

        static void SetupQueriesSchemes(PlistElementDict rootDict)
        {
            if (UniversalSDKSettings.UseFacebookLogin)
            {
                PlistElementArray array = GetOrCreateArray(rootDict, "LSApplicationQueriesSchemes");
                array.AddString("fbapi");
                array.AddString("fbauth2");
            }
        }

        static void SetupGoogleSetting(PlistElementDict rootDict)
        {
            if (UniversalSDKSettings.UseGoogleLogin)
            {
                rootDict.SetString("GIDClientID", UniversalSDKSettings.GoogleClientID);
                rootDict.SetString("GIDServerClientID", UniversalSDKSettings.GoogleServerClientID);
            }
        }

        static void SetupFacebookSetting(PlistElementDict rootDict)
        {
            if (UniversalSDKSettings.UseFacebookLogin)
            {
                rootDict.SetString("FacebookAppID", UniversalSDKSettings.FacebookAppID);
                rootDict.SetString("FacebookClientToken", UniversalSDKSettings.FacebookClientToken);
            }                
        }

        static PlistElementArray GetOrCreateArray(PlistElementDict dict, string key)
        {
            PlistElement array = dict[key];
            if (array != null)
            {
                return array.AsArray();
            }
            else
            {
                return dict.CreateArray(key);
            }
        }
    }
}
#endif