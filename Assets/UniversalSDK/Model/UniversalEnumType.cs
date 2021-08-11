
namespace Universal.UniversalSDK
{
    public enum LoginType
    {
        NONE,
        GOOGLE,
        FACEBOOK,        
        APPLE,
    }

    public enum ApiResponseCode
    {
        SUCCESS = 1000,
        CANCEL,        
        AUTHENTICATION_AGENT_ERROR,        
        INTERNAL_ERROR,
        PURCHASE_ERROR,
    }
}
