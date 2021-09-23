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

            if (UniversalSDKSettings.GetOrCreateSettings().UseGoogleLogin)
                schemes.AddString(UniversalSDKSettings.GetOrCreateSettings().ReversedClientID);

            if (UniversalSDKSettings.GetOrCreateSettings().UseFacebookLogin)
                schemes.AddString("fb"+ UniversalSDKSettings.GetOrCreateSettings().FacebookAppID);
        }

        static void SetupQueriesSchemes(PlistElementDict rootDict)
        {
            if (UniversalSDKSettings.GetOrCreateSettings().UseFacebookLogin)
            {
                PlistElementArray array = GetOrCreateArray(rootDict, "LSApplicationQueriesSchemes");
                array.AddString("fbapi");
                array.AddString("fbauth2");
            }
        }

        static void SetupGoogleSetting(PlistElementDict rootDict)
        {
            if (UniversalSDKSettings.GetOrCreateSettings().UseGoogleLogin)
                rootDict.SetString("GoogleClientID", UniversalSDKSettings.GetOrCreateSettings().GoogleClientID);            
        }

        static void SetupFacebookSetting(PlistElementDict rootDict)
        {
            if (UniversalSDKSettings.GetOrCreateSettings().UseFacebookLogin)
                rootDict.SetString("FacebookAppID", UniversalSDKSettings.GetOrCreateSettings().FacebookAppID);
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