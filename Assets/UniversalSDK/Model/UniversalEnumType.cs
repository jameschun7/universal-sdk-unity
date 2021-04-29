
namespace Universal.UniversalSDK
{
    public enum LoginType
    {
        NONE,
        GOOGLE,
        FACEBOOK,
        GUEST,
        APPLE,
    }

    public enum AccountServiceType
    {
        NONE,
        ACCOUNT_LOGIN,
        ACCOUNT_LINK,
        ACCOUNT_UNLINK,
    }

    public enum ApiResponseCode
    {
        SUCCESS,
        CANCEL,
        NETWORK_ERROR,
        SERVER_ERROR,
        AUTHENTICATION_AGENT_ERROR,
        ALREADY_LOGGED_IN,
        ALREADY_ACCOUNT_USE,
        BLOCK_IP_CHECK,
        SERVICE_MAINTENANCE,
        INTERNAL_ERROR,
        PURCHASE_ERROR,
    }
}
