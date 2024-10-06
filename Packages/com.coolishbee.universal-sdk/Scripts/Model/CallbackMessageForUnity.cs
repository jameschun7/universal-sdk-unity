using System;
using UnityEngine;

namespace Universal.UniversalSDK
{
    [Serializable]
    public class CallbackMessageForUnity
    {
        [SerializeField]
        private string identifier;
        [SerializeField]
        private string value;

        public string Identifier { get { return identifier; } }
        public string Value { get { return value; } }

        public static CallbackMessageForUnity FromJson(string json)
        {
            return JsonUtility.FromJson<CallbackMessageForUnity>(json);
        }

        CallbackMessageForUnity(string identifier, string value)
        {
            this.identifier = identifier;
            this.value = value;
        }

        string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        public static string WrapValue(string identifier, string value)
        {
            var payload = new CallbackMessageForUnity(identifier, value);
            return payload.ToJson();
        }
    }
}