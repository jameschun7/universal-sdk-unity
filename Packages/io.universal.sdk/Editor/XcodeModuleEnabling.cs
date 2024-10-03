#if UNITY_IOS
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;

namespace Universal.UniversalSDK.Editor
{
    public class XcodeBuildConfigUpdating
    {
        [PostProcessBuildAttribute(3)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            if (target != BuildTarget.iOS)
            {
                return;
            }

            string projPath = Path.Combine(pathToBuiltProject, "Unity-iPhone.xcodeproj/project.pbxproj");
            PBXProject proj = new PBXProject();
            proj.ReadFromFile(projPath);

            var appTarget = proj.GetUnityFrameworkTargetGuid();

            proj.SetBuildProperty(appTarget, "CLANG_ENABLE_MODULES", "YES");
           
            File.WriteAllText(projPath, proj.WriteToString());
        }
    }
}
#endif