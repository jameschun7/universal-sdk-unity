using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Universal.UniversalSDK;

public class LoginController : MonoBehaviour
{
    public Text titleText;
    public Text messageText;
    public GameObject popup_panel;

    void Start()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        var scopes = new string[] { "boxer_unity1000", "boxer_unity2000" };
#elif UNITY_IOS
        var scopes = new string[] { "com.gamepub.unity.inapp1200", "com.gamepub.unity.inapp2500" };        
#endif
        UniversalSDK.Ins.SetupSDK(scopes, result =>
        {
            result.Match(
                value =>
                {                    
                    titleText.text = value.Code.ToString();
                    messageText.text = value.Message;
                    popup_panel.SetActive(true);
                },
                error =>
                {                    
                    titleText.text = error.Code.ToString();
                    messageText.text = error.Message;
                    popup_panel.SetActive(true);
                });
        });
    }

    public void OnClickGoogleLogin()
    {
        UniversalSDK.Ins.Login(LoginType.GOOGLE,
            result =>
            {
                result.Match(
                    value =>
                    {
                        UserInfoManager.Instance.loginResult = value;
                        SceneManager.LoadSceneAsync("Main");
                    },
                    error =>
                    {
                        titleText.text = error.Code.ToString();
                        messageText.text = error.Message;
                        popup_panel.SetActive(true);
                    });
            });
    }
    public void OnClickFacebookLogin()
    {
        UniversalSDK.Ins.Login(LoginType.FACEBOOK,
            result =>
            {
                result.Match(
                    value =>
                    {
                        UserInfoManager.Instance.loginResult = value;
                        SceneManager.LoadSceneAsync("Main");
                    },
                    error =>
                    {
                        titleText.text = error.Code.ToString();
                        messageText.text = error.Message;
                        popup_panel.SetActive(true);
                    });
            });
    }    
    public void OnClickAppleLogin()
    {
        UniversalSDK.Ins.Login(LoginType.APPLE,
            result =>
            {
                result.Match(
                    value =>
                    {
                        UserInfoManager.Instance.loginResult = value;
                        SceneManager.LoadSceneAsync("Main");
                    },
                    error =>
                    {
                        titleText.text = error.Code.ToString();
                        messageText.text = error.Message;
                        popup_panel.SetActive(true);
                    });
            });
    }

    public void OnClickClosePopup()
    {
        popup_panel.SetActive(false);
    }
    
}
