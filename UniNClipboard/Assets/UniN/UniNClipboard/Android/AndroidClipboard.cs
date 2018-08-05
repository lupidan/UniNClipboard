using System;
using System.Collections.Generic;
using UnityEngine;

namespace UniN.UniNClipboard
{
    public class AndroidClipboard : MonoBehaviour, IClipboard
    {
        private readonly List<Action> _updateActionsQueue = new List<Action>();

        private AndroidJavaObject _clipboardManager;
        private AndroidJavaObject _currentActivity;
        private OnPrimaryClipChangedListener _nativeListener;

        private AndroidJavaObject CurrentActivity
        {
            get
            {
                if (_currentActivity == null)
                {
                    var unityPlayerClass = new AndroidJavaClass(ClassNames.UnityPlayer);
                    _currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>(MethodNames.SCurrentActivity);
                }

                return _currentActivity;
            }
        }

        private AndroidJavaObject ClipboardManager
        {
            get
            {
                if (_clipboardManager == null)
                    _clipboardManager = CurrentActivity.Call<AndroidJavaObject>(MethodNames.GetSystemService, StringConstants.ClipboardService);
                return _clipboardManager;
            }
        }

        public bool ClipboardAvailable
        {
            get { return true; }
        }

        public string Text
        {
            get { return GetText(); }
            set { SetText(value); }
        }

        public event Action OnClipboardChanged
        {
            add
            {
                if (_nativeListener == null)
                    SetupClipboardChangedListener();

                _onClipboardChanged += value;
            }
            remove
            {
                _onClipboardChanged -= value;

                if (_onClipboardChanged == null)
                    RemoveClipboardChangedListener();
            }
        }

        private event Action _onClipboardChanged;

        private void Update()
        {
            UpdateActionQueue();
        }

        private void SetText(string text)
        {
            var clipDataClass = new AndroidJavaClass(ClassNames.ClipData);
            var clipDataInstance = clipDataClass.CallStatic<AndroidJavaObject>(MethodNames.SNewPlainText, "UniNCliboard", text);
            ClipboardManager.Call(MethodNames.SetPrimaryClip, clipDataInstance);
        }

        private string GetText()
        {
            var clipDataInstance = ClipboardManager.Call<AndroidJavaObject>(MethodNames.GetPrimaryClip);
            if (clipDataInstance == null)
                return null;

            var itemCount = clipDataInstance.Call<int>(MethodNames.GetItemCount);
            if (itemCount == 0)
                return null;

            var clipDataItemInstance = clipDataInstance.Call<AndroidJavaObject>(MethodNames.GetItemAt, 0);
            if (clipDataItemInstance == null)
                return null;

            return clipDataItemInstance.Call<string>(MethodNames.CoerceToText, CurrentActivity);
        }

        private void SetupClipboardChangedListener()
        {
            _nativeListener = new OnPrimaryClipChangedListener(this);
            ClipboardManager.Call(MethodNames.AddPrimaryClipChangedListener, _nativeListener);
        }

        private void RemoveClipboardChangedListener()
        {
            ClipboardManager.Call(MethodNames.RemovePrimaryClipChangedListener, _nativeListener);
            _nativeListener = null;
        }

        private void QueueActionForNextUpdate(Action queueAction)
        {
            _updateActionsQueue.Add(queueAction);
        }

        private void UpdateActionQueue()
        {
            if (_updateActionsQueue.Count == 0)
                return;

            for (var i = 0; i < _updateActionsQueue.Count; i++)
                if (_updateActionsQueue[i] != null)
                    _updateActionsQueue[i].Invoke();

            _updateActionsQueue.Clear();
        }

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
            public const string AddPrimaryClipChangedListener = "addPrimaryClipChangedListener";
            public const string RemovePrimaryClipChangedListener = "removePrimaryClipChangedListener";
            public const string SNewPlainText = "newPlainText";
        }

        private class OnPrimaryClipChangedListener : AndroidJavaProxy
        {
            private readonly AndroidClipboard _clipboard;

            //Android API Level 11
            public OnPrimaryClipChangedListener(AndroidClipboard clipboard) :
                base("android.content.ClipboardManager$OnPrimaryClipChangedListener")
            {
                _clipboard = clipboard;
            }

            private void onPrimaryClipChanged()
            {
                _clipboard.QueueActionForNextUpdate(() =>
                {
                    if (_clipboard._onClipboardChanged != null)
                        _clipboard._onClipboardChanged.Invoke();
                });
            }
        }
    }
}