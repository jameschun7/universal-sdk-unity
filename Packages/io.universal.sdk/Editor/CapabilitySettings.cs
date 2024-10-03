#if UNITY_IOS
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;

namespace Universal.UniversalSDK.Editor
{
    public class CapabilitySettings
    {
        [PostProcessBuildAttribute(4)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            if (target != BuildTarget.iOS)
            {
                return;
            }
            
            string projectPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
            var project = new PBXProject();
            project.ReadFromFile(projectPath);
            string targetGuid = project.GetUnityMainTargetGuid();            

            project.AddFrameworkToProject(targetGuid, "StoreKit.framework", false);
            File.WriteAllText(projectPath, project.WriteToString());

            var manager = new ProjectCapabilityManager(projectPath, "Entitlements.entitlements", null, project.GetUnityMainTargetGuid());
            //manager.AddInAppPurchase();
            //manager.AddPushNotifications(true);
            if(UniversalSDKSettings.UseAppleLogin)
                manager.AddSignInWithApple();
            manager.WriteToFile();            
        }
    }

}
#endif