using System;
using UnityEngine;

namespace UniN.Clipboard
{
    public class AndroidClipboard : IClipboard
    {
        private static class ClassNames
        {
            // Unity
            public const string UnityPlayer = "com.unity3d.player.UnityPlayer";

            // API Level 11
            public const string ClipboardManager = "android.content.ClipboardManager";
            public const string ClipData = "android.content.ClipData";
        }

        private static class StringConstants
        {
            // API Level 1
            public const string ClipboardService = "clipboard"; // API Level 1
        }

        private static class MethodNames
        {
            // Unity
            public const string SCurrentActivity = "currentActivity";

            // Android API Level 1
            public const string GetSystemService = "getSystemService";

            //Android API Level 11
            public const string SetPrimaryClip = "setPrimaryClip";
            public const string GetPrimaryClip = "getPrimaryClip";
            public const string GetItemCount = "getItemCount";
            public const string GetItemAt = "getItemAt";
            public const string CoerceToText = "coerceToText";
            public const string SNewPlainText = "newPlainText";
        }

        private AndroidJavaObject _currentActivity;
        private AndroidJavaObject CurrentActivity
        {
            get
            {
                if (this._currentActivity == null)
                {
                    var unityPlayerClass = new AndroidJavaClass(ClassNames.UnityPlayer);
                    this._currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>(MethodNames.SCurrentActivity);
                }
                return this._currentActivity;
            }
        }

        private AndroidJavaObject _clipboardManager;
        private AndroidJavaObject ClipboardManager
        {
            get
            {
                if (this._clipboardManager == null)
                    this._clipboardManager = this.CurrentActivity.Call<AndroidJavaObject>(MethodNames.GetSystemService, StringConstants.ClipboardService);
                return this._clipboardManager;
            }
        }



        public bool ClipboardAvailable
        {
            get { return true; }
        }

        public string Text
        {
            get { return this.GetText(); }
            set { this.SetText(value); }
        }

        private void SetText(string text)
        {
            var label = "Test label";
            var clipDataClass = new AndroidJavaClass(ClassNames.ClipData);
            var clipDataInstance = clipDataClass.CallStatic<AndroidJavaObject>(MethodNames.SNewPlainText, label, text);
            this._clipboardManager.Call(MethodNames.SetPrimaryClip, clipDataInstance);
        }

        private string GetText()
        {
            var clipDataInstance = this.ClipboardManager.Call<AndroidJavaObject>(MethodNames.GetPrimaryClip);
            if (clipDataInstance == null)
                return null;

            var itemCount = clipDataInstance.Call<int>(MethodNames.GetItemCount);
            if (itemCount == 0)
                return null;

            var clipDataItemInstance = clipDataInstance.Call<AndroidJavaObject>(MethodNames.GetItemAt, 0);
            if (clipDataItemInstance == null)
                return null;

            return clipDataItemInstance.Call<string>(MethodNames.CoerceToText, this.CurrentActivity);
        }
    }
}
