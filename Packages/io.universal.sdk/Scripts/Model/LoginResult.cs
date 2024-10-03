using System;
using UnityEngine;

namespace Universal.UniversalSDK
{
    [Serializable]
    public class LoginResult
    {
        [SerializeField]
        private string userID = "";
        [SerializeField]
        private string idToken = "";
        [SerializeField]
        private string name = "";
        [SerializeField]
        private string email = "";
        [SerializeField]
        private string imageURL = "";
        [SerializeField]
        private string authCode = "";

        public string UserID { get { return userID; } }
        public string IdToken { get { return idToken; } }
        public string Name { get { return name; } }
        public string Email { get { return email; } }
        public string ImageURL { get { return imageURL; } }
        public string AuthCode { get { return authCode; } }
    }
}
