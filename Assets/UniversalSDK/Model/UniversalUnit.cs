using System;
using UnityEngine;

namespace Universal.UniversalSDK
{
    [Serializable]
    public class UniversalUnit
    {
        [SerializeField]
        private int code = 0;
        [SerializeField]
        private string message = "";

        public int Code { get { return code; } }
        public string Message { get { return message; } }
    }
}