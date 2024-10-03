using System;

namespace Universal.UniversalSDK
{
    public interface IUniversalCallback
    {
        IUniversalCallback OnSuccess(Action<LoginResult> onSuccess);
        IUniversalCallback OnError(Action<Error> onError);
    }
}