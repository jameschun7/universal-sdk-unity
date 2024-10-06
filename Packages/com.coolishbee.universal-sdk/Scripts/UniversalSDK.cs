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
            SetupSDK();
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

        void SetupSDK()
        {            
            NativeInterface.SetupSDK();
        }

        public IUniversalCallback Login(LoginType loginType)
        {
            return UniversalAPI.Login(loginType);
        }

        public void Logout()
        {
            NativeInterface.Logout();
        }        
        
        public void OpenCustomTabView(string url)
        {
            NativeInterface.OpenCustomTabView(url);            
        }        

        public void OnApiOk(string result)
        {            
            result.Log();
            UniversalAPI._OnApiOk(result);
        }

        public void OnApiError(string result)
        {            
            result.Log();
            UniversalAPI._OnApiError(result);
        }
    }
}
