using System;
using System.Collections.Generic;
using UnityEngine;

namespace Universal.UniversalSDK
{
    public class UniversalSDK : MonoBehaviour
    {
        static UniversalSDK instance;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);            
        }

        public static UniversalSDK Ins
        {
            get
            {
                if (instance == null)
                {
                    GameObject go = new GameObject("UniversalSDK");
                    instance = go.AddComponent<UniversalSDK>();
                }
                return instance;
            }
        }

        public void SetupSDK(string[] scopes,
                             Action<Result<UniversalUnit>> action)
        {
            UniversalAPI.SetupSDK(scopes, action);
        }

        public void Login(LoginType loginType,
                          Action<Result<LoginResult>> action)
        {
            UniversalAPI.Login(loginType, action);
        }

        public void Logout(LoginType loginType,
                           Action<Result<UniversalUnit>> action)
        {
            UniversalAPI.Logout(loginType, action);
        }

        public void InAppPurchase(string pid,                                  
                                  Action<Result<PurchaseData>> action)
        {
            UniversalAPI.InAppPurchase(pid, action);
        }
        
        public void OpenCustomTabView(string url,
                                      Action<Result<UniversalUnit>> action)
        {
            UniversalAPI.OpenCustomTabView(url, action);
        }        

        public void OnApiOk(string result)
        {
            Debug.Log("OnApiOk : " + result);
            UniversalAPI._OnApiOk(result);
        }

        public void OnApiError(string result)
        {
            Debug.Log("OnApiError : " + result);
            UniversalAPI._OnApiError(result);
        }
    }
}
