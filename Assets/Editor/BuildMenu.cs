using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using Universal.UniversalSDK.Editor;

public class BuildMenu : ScriptableObject
{
    static string[] SCENES = FindEnabledEditorScenes();

    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();

        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }

        return EditorScenes.ToArray();
    }

    static string GetBuildID()
    {
        string[] args = System.Environment.GetCommandLineArgs();
        string buildid = null;
        for (int i = 0; i < args.Length - 1; i++)
        {
            if ("-buildid" == args[i])
            {
                buildid = args[i + 1];
                break;
            }
        }
        if (buildid == null)
        {
            buildid = string.Format(@"{0:yyyy-MM-dd-hh-mm}", DateTime.Now);
        }

        return buildid;
    }

    static string GetBuildNumber()
    {
        string[] args = System.Environment.GetCommandLineArgs();
        string buildid = "10";
        for (int i = 0; i < args.Length - 1; i++)
        {
            if ("-buildno" == args[i])
            {
                buildid = args[i + 1];
                break;
            }
        }

        return buildid;
    }

    static string GetAndroidOutputFile()
    {
        string output = string.Format("{0}/{1}_{2}.apk", "Android Build",
                                                     "SDKUnityDemo",
                                                     GetBuildID());
        return output;
    }

    static string GetIOSOutputFile()
    {
        string output = string.Format("{0}/{1}_{2}", "iOS Build",
                                                     "SDKUnityDemo",
                                                     GetBuildID());
        return output;
    }

    [UnityEditor.MenuItem("BuildMenu/Android", false, 3001)]
    static void Buildmachine_Android()
    {
        PlayerSettings.productName = "SDKUnityDemo";
        PrepareAndroidBuild("com.universal.android.sample");

        PerformAndroidBuild(GetAndroidOutputFile(), "", false);
    }

    [UnityEditor.MenuItem("BuildMenu/Android Build And Run", false, 3002)]
    static void Buildmachine_AndroidAuto()
    {
        PlayerSettings.productName = "UnityDemo";
        PrepareAndroidBuild("com.universal.android.sample");

        PerformAndroidBuild(GetAndroidOutputFile(), "", true);
    }

    [UnityEditor.MenuItem("BuildMenu/iOS", false, 3004)]
    static void Buildmachine_iOS()
    {
        PlayerSettings.productName = "UnityDemo";
        PrepareiOSBuild("com.gamepub.ios.testapp");

        PerformiOSBuild(GetIOSOutputFile(), "", false);
    }

    [UnityEditor.MenuItem("BuildMenu/TEST", false, 3003)]
    static void Buildmachine_iOSAuto()
    {
        
    }

    static void PrepareAndroidBuild(string bundleID)
    {
        PlayerSettings.Android.keystoreName = "universal.keystore";
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, bundleID);
        PlayerSettings.Android.keyaliasName = "universal";
        PlayerSettings.keystorePass = "15441010";
        PlayerSettings.keyaliasPass = "15441010";

        //PlayerSettings.bundleVersion = "";
        //PlayerSettings.Android.bundleVersionCode = 20;
    }

    static void PrepareiOSBuild(string bundleID)
    {
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, bundleID);
        //PlayerSettings.bundleVersion = "";
        //PlayerSettings.iOS.buildNumber = "";
        PlayerSettings.iOS.appleEnableAutomaticSigning = true;
        PlayerSettings.iOS.appleDeveloperTeamID = "PRPC4C837N";
    }

    static string PerformAndroidBuild(string output, string define, bool isDev)
    {
        Debug.Log("PerformAndroidBuild()");
        Debug.Log(output);

        //PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, define);

        BuildPipeline.BuildPlayer(SCENES, output, BuildTarget.Android, isDev ? BuildOptions.AutoRunPlayer : BuildOptions.None);
        return output;
    }

    static void PerformiOSBuild(string output, string define, bool isDev)
    {
        BuildPlayerOptions opt = new BuildPlayerOptions();
        opt.scenes = FindEnabledEditorScenes();
        opt.locationPathName = output;
        opt.target = BuildTarget.iOS;
        opt.options = BuildOptions.None;


        //PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, define);               

        BuildPipeline.BuildPlayer(opt);
    }
}
