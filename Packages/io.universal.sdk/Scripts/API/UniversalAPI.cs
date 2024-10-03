using System;
using System.Collections.Generic;
using UnityEngine;

namespace Universal.UniversalSDK
{
    public class UniversalAPI
    {
        public static Dictionary<string, UniversalCallbackImpl> actions =
            new Dictionary<string, UniversalCallbackImpl>();        

        public static IUniversalCallback Login(LoginType loginType)
        {            
            var impl = new UniversalCallbackImpl();
            var identifier = AddAction(impl);
            NativeInterface.Login(identifier, loginType);

            return impl;
        }

        static string AddAction(UniversalCallbackImpl action)
        {
            var identifier = Guid.NewGuid().ToString();
            actions.Add(identifier, action);
            return identifier;
        }        

        static UniversalCallbackImpl PopActionFromPayload(CallbackMessageForUnity payload)
        {
            var identifier = payload.Identifier;            
            if (identifier == null)
            {
                return null;
            }
            UniversalCallbackImpl action = null;
            if (actions.TryGetValue(identifier, out action))
            {
                actions.Remove(identifier);
                return action;
            }
            return null;
        }

        public static void _OnApiOk(string result)
        {
            var payload = CallbackMessageForUnity.FromJson(result);
            var action = PopActionFromPayload(payload);
            if (action != null)
            {
                action.CallOk(payload.Value);
            }
        }

        public static void _OnApiError(string result)
        {
            var payload = CallbackMessageForUnity.FromJson(result);
            var action = PopActionFromPayload(payload);
            if (action != null)
            {
                action.CallError(payload.Value);
            }
        }
    }

}