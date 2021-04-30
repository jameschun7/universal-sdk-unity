#if !UNITY_IOS && !UNITY_ANDROID
namespace Universal.UniversalSDK
{
    public class NativeInterface
    {
        public static void SetupSDK(string identifier, string list) { }
        public static void Login(string identifier, LoginType loginType, AccountServiceType serviceType) { }
        public static void Logout(string identifier, LoginType loginType) { }
        public static void InAppPurchase(string identifier, string pid) { }
        public static void ImageBanner(string identifier, string ratioWidth, string ratioHeight, string imageUrl) { }
        public static void OpenCustomTabView(string identifier, string url) { }
    }
}
#endif