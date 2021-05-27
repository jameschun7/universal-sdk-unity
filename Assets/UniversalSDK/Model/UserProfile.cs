using System;
using UnityEngine;

namespace Universal.UniversalSDK
{
    [Serializable]
    public class UserProfile
    {
        [SerializeField]
        private string userID = "";
        [SerializeField]
        private string idToken = "";
        [SerializeField]
        private string accessToken = "";
        [SerializeField]
        private string displayName = "";        
        [SerializeField]
        private string email = "";
        [SerializeField]
        private string photoURL = "";
        [SerializeField]
        private string pushToken = "";

        public string UserID { get { return userID; } }
        public string IdToken { get { return idToken; } }
        public string AccessToken { get { return accessToken; } }
        public string DisplayName { get { return displayName; } }        
        public string Email { get { return email; } }
        public string PhotoURL { get { return photoURL; } }
        public string PushToken { get { return pushToken; } }
    }
}