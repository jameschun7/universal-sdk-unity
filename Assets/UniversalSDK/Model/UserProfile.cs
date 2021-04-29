using System;
using UnityEngine;

namespace Universal.UniversalSDK
{
    [Serializable]
    public class UserProfile
    {
        [SerializeField]
        private string uniqueId = "";
        [SerializeField]
        private string displayName = "";        
        [SerializeField]
        private string email = "";
        [SerializeField]
        private string photoURL = "";
        [SerializeField]
        private string pushToken = "";

        public string UniqueId { get { return uniqueId; } }
        public string DisplayName { get { return displayName; } }        
        public string Email { get { return email; } }
        public string PhotoURL { get { return photoURL; } }
        public string PushToken { get { return pushToken; } }
    }
}