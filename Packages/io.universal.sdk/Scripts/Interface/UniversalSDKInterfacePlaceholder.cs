#if !UNITY_IOS && !UNITY_ANDROID
namespace Universal.UniversalSDK
{
    public class NativeInterface
    {
        public static void SetupSDK() { }
        public static void Login(string identifier, LoginType loginType) { }
        public static void Logout() { }
        public static void OpenCustomTabView(string url) { }
    }
}
#endif