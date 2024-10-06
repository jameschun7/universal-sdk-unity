using System;
using UnityEngine;

namespace Universal.UniversalSDK
{
    public class UniversalCallbackImpl : IUniversalCallback
    {
        private Action<LoginResult> onSuccess;
        private Action<Error> onError;        

        public IUniversalCallback OnSuccess(Action<LoginResult> onSuccess)
        {
            this.onSuccess += onSuccess;
            return this;
        }

        public IUniversalCallback OnError(Action<Error> onError)
        {
            this.onError += onError;
            return this;
        }

        public void CallOk(string s)
        {
            var result = JsonUtility.FromJson<LoginResult>(s);
            onSuccess?.Invoke(result);
        }

        public void CallError(string s)
        {
            var result = JsonUtility.FromJson<Error>(s);
            onError?.Invoke(result);
        }
    }
}
