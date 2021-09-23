using System;
using System.Collections.Generic;
using UnityEngine;

namespace Universal.UniversalSDK
{
    public class UniversalAPI
    {
        public static Dictionary<string, FlattenAction> actions =
            new Dictionary<string, FlattenAction>();

        public static void SetupSDK(string[] list, Action<Result<UniversalUnit>> action)
        {
            var identifier = AddAction(FlattenAction.JsonFlatten<UniversalUnit>(action));

            NativeInterface.SetupSDK(identifier, string.Join(" ", list));
        }

        public static void Login(LoginType loginType,                                 
                                 Action<Result<LoginResult>> action)
        {
            var identifier = AddAction(FlattenAction.JsonFlatten<LoginResult>(action));

            NativeInterface.Login(identifier, loginType);
        }

        public static void Logout(Action<Result<UniversalUnit>> action)
        {
            var identifier = AddAction(FlattenAction.JsonFlatten<UniversalUnit>(action));
            NativeInterface.Logout(identifier);
        }        

        public static void InAppPurchase(string pid,                                         
                                         Action<Result<PurchaseData>> action)
        {
            var identifier = AddAction(FlattenAction.JsonFlatten<PurchaseData>(action));
            NativeInterface.InAppPurchase(identifier, pid);
        }        

        public static void OpenCustomTabView(string url,
                                             Action<Result<UniversalUnit>> action)
        {
            var identifier = AddAction(FlattenAction.JsonFlatten<UniversalUnit>(action));
            NativeInterface.OpenCustomTabView(identifier, url);
        }        

        static string AddAction(FlattenAction action)
        {
            var identifier = Guid.NewGuid().ToString();
            actions.Add(identifier, action);
            return identifier;
        }

        static FlattenAction PopActionFromPayload(CallbackMessageForUnity payload)
        {
            var identifier = payload.Identifier;
            if (identifier == null)
            {
                return null;
            }
            FlattenAction action = null;
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