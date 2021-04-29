using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using Universal.UniversalSDK;

public class MainController : MonoBehaviour
{
#if UNITY_ANDROID    
    string productID_1 = "boxer_unity1000";
    string productID_2 = "boxer_unity2000";
#elif UNITY_IOS    
    string productID_1 = "com.gamepub.unity.inapp1200";
    string productID_2 = "com.gamepub.unity.inapp2500";
#endif

    public Image userImage;
    public Text displayNameText;
    public Text uniqueIdText;    
    public Text emailText;

    public Text rawJsonText;

    string imgUrl = "https://user-images.githubusercontent.com/20632507/108068287-3c637d80-70a5-11eb-94f6-4aa605df1f76.jpeg";
    string helpUrl = "https://support.google.com/?hl=ko";

    void Start()
    {
        LoginResult result = UserInfoManager.Instance.loginResult;
        StartCoroutine(UpdateProfile(result.UserProfile));
        UpdateRawSection(result);
    }

    public void OnLinkWithGoogle()
    {
        UniversalSDK.Ins.Login(LoginType.GOOGLE,
            AccountServiceType.ACCOUNT_LINK, result =>
            {
                result.Match(
                    value =>
                    {
                        UpdateRawSection(value);
                    },
                    error =>
                    {
                        UpdateRawSection(error);
                    });
            });
    }

    public void OnLinkWithFacebook()
    {
        UniversalSDK.Ins.Login(LoginType.FACEBOOK,
            AccountServiceType.ACCOUNT_LINK, result =>
            {
                result.Match(
                    value =>
                    {
                        UpdateRawSection(value);
                    },
                    error =>
                    {
                        UpdateRawSection(error);
                    });
            });
    }

    public void OnUnlinkWithGoogle()
    {
        UniversalSDK.Ins.Login(LoginType.GOOGLE,
            AccountServiceType.ACCOUNT_UNLINK, result =>
            {
                result.Match(
                    value =>
                    {
                        UpdateRawSection(value);
                    },
                    error =>
                    {
                        UpdateRawSection(error);
                    });
            });
    }

    public void OnUnlinkWithFacebook()
    {
        UniversalSDK.Ins.Login(LoginType.FACEBOOK,
            AccountServiceType.ACCOUNT_UNLINK, result =>
            {
                result.Match(
                    value =>
                    {
                        UpdateRawSection(value);
                    },
                    error =>
                    {
                        UpdateRawSection(error);
                    });
            });
    }

    public void OnClickLogout()
    {
        UniversalSDK.Ins.Logout(LoginType.GOOGLE, result =>
        {
            result.Match(
                value =>
                {
                    SceneManager.LoadSceneAsync("Login");
                },
                error =>
                {
                    UpdateRawSection(error);
                });
        });
    }
    public void OnClickInPurchase1200()
    {
        UniversalSDK.Ins.InAppPurchase(productID_1, result =>
        {
            result.Match(
                value =>
                {
                    UpdateRawSection(value);
                },
                error =>
                {
                    UpdateRawSection(error);
                });
        });
    }
    public void OnClickInPurchase2500()
    {
        UniversalSDK.Ins.InAppPurchase(productID_2, result =>
        {
            result.Match(
                value =>
                {
                    UpdateRawSection(value);
                },
                error =>
                {
                    UpdateRawSection(error);
                });
        });
    }
    public void OnClickImageBanner()
    {
        UniversalSDK.Ins.ImageBanner("9", "16", imgUrl, result =>
        {
            result.Match(
                value =>
                {
                    UpdateRawSection(value);
                },
                error =>
                {
                    UpdateRawSection(error);
                });
        });
    }

    public void OnClickOpenCustomTabView()
    {
        UniversalSDK.Ins.OpenCustomTabView(helpUrl, result =>
        {
            result.Match(
                value =>
                {
                    UpdateRawSection(value);
                },
                error =>
                {
                    UpdateRawSection(error);
                });
        });
    }

    public void UpdateRawSection(object obj)
    {
        if (obj == null)
        {
            rawJsonText.text = "null";
            return;
        }
        var text = JsonUtility.ToJson(obj);
        if (text == null)
        {
            rawJsonText.text = "Invalid Object";
            return;
        }
        rawJsonText.text = text + "\n\n" + rawJsonText.text;
        var scrollContentTransform = (RectTransform)rawJsonText.gameObject.transform.parent;
        scrollContentTransform.localPosition = Vector3.zero;
    }

    IEnumerator UpdateProfile(UserProfile profile)
    {
        if (profile.PhotoURL != null)
        {
            var www = UnityWebRequestTexture.GetTexture(profile.PhotoURL);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                var texture = DownloadHandlerTexture.GetContent(www);
                userImage.color = Color.white;
                userImage.sprite = Sprite.Create(
                    texture,
                    new Rect(0, 0, texture.width, texture.height),
                    new Vector2(0, 0));
            }
        }
        else
        {
            yield return null;
        }        
        displayNameText.text = profile.DisplayName;
        uniqueIdText.text = profile.UniqueId;
        emailText.text = profile.Email;
    }
}
