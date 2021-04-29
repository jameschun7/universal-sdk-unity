using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.IO;

namespace Universal.UniversalSDK.Editor
{
    class UniversalSDKSettings : ScriptableObject
    {
        const string assetPath = "Assets/Editor/UniversalSDK/UniversalSDKSettings.asset";

        internal static string[] dependencyManagerOptions = new string[] { "CocoaPods" };

        [SerializeField]
        private string iOSDependencyManager;
        [SerializeField]
        private bool appleLogin;
        [SerializeField]
        private bool facebookLogin;
        [SerializeField]
        private string facebookAppID;

        internal static int DependencySelectedIndex(string selected)
        {
            return Array.IndexOf(dependencyManagerOptions, selected);
        }

        internal bool UseCocoaPods { get { return iOSDependencyManager.Equals("CocoaPods"); } }
        internal string FacebookAppID { get { return facebookAppID; } }
        internal bool UseAppleLogin { get { return appleLogin; } }
        internal bool UseFacebookLogin { get { return facebookLogin; } }

        internal static UniversalSDKSettings GetOrCreateSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<UniversalSDKSettings>(assetPath);
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<UniversalSDKSettings>();
                settings.iOSDependencyManager = "CocoaPods";
                settings.appleLogin = false;
                settings.facebookLogin = false;

                Directory.CreateDirectory("Assets/Editor/UniversalSDK/");

                AssetDatabase.CreateAsset(settings, assetPath);
                AssetDatabase.SaveAssets();
            }
            return settings;
        }

        internal static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }
    }

    static class PubSDKSettingsProvider
    {

        static SerializedObject settings;

        private class Provider : SettingsProvider
        {
            public Provider(string path, SettingsScope scope = SettingsScope.User) : base(path, scope) { }
            public override void OnGUI(string searchContext)
            {
                DrawPref();
            }
        }
        [SettingsProvider]
        static SettingsProvider MyNewPrefCode()
        {
            return new Provider("Preferences/UniversalSDK");
        }

        static void DrawPref()
        {
            if (settings == null)
            {
                settings = UniversalSDKSettings.GetSerializedSettings();
            }
            settings.Update();
            EditorGUI.BeginChangeCheck();

            var property = settings.FindProperty("iOSDependencyManager");
            var selected = UniversalSDKSettings.DependencySelectedIndex(property.stringValue);

            var propertyAppleLogin = settings.FindProperty("appleLogin");
            var enableAppleLogin = propertyAppleLogin.boolValue;

            var propertyFacebookLogin = settings.FindProperty("facebookLogin");
            var enableFacebookLogin = propertyFacebookLogin.boolValue;

            var propertyFacebookAppId = settings.FindProperty("facebookAppID");
            var facebookAppId = propertyFacebookAppId.stringValue;

            selected = EditorGUILayout.Popup("iOS Dependency Manager", selected, UniversalSDKSettings.dependencyManagerOptions);
            enableAppleLogin = EditorGUILayout.Toggle("Apple Login Enable", enableAppleLogin);

            GUILayout.Space(20);
            GUI.skin.label.fontSize = 17;
            GUILayout.Label("Facebook", GUILayout.Width(200), GUILayout.Height(30));
            enableFacebookLogin = EditorGUILayout.BeginToggleGroup("Facebook Login Enable", enableFacebookLogin);
            facebookAppId = EditorGUILayout.TextField("Facebook App ID", facebookAppId);
            EditorGUILayout.EndToggleGroup();

            if (selected < 0)
            {
                selected = 0;
            }
            property.stringValue = UniversalSDKSettings.dependencyManagerOptions[selected];
            propertyAppleLogin.boolValue = enableAppleLogin;
            propertyFacebookLogin.boolValue = enableFacebookLogin;
            propertyFacebookAppId.stringValue = facebookAppId;

            if (EditorGUI.EndChangeCheck())
            {
                settings.ApplyModifiedProperties();
                AssetDatabase.SaveAssets();
            }

        }
    }
}