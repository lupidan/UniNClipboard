using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace UniN.UniNClipboard
{
    public class IOSClipboard : MonoBehaviour, IClipboard
    {
        private bool _clipboardDidChange;

        public bool ClipboardAvailable
        {
            get { return true; }
        }

        public string Text
        {
            get { return IOSUniNClipboardGetText(); }
            set { IOSUniNClipboardSetText(value); }
        }

        public event Action OnClipboardChanged
        {
            add
            {
                if (_onClipboardChanged == null)
                    SetupNativeCallback();

                _onClipboardChanged += value;
            }
            remove
            {
                _onClipboardChanged -= value;

                if (_onClipboardChanged == null)
                    RemoveNativeCallback();
            }
        }

        private event Action _onClipboardChanged;

        private void Update()
        {
            if (!_clipboardDidChange)
                return;

            _clipboardDidChange = false;
            if (_onClipboardChanged != null)
                _onClipboardChanged.Invoke();
        }

        private void SetupNativeCallback()
        {
            onNativeClipboardChanged += ActivateChangedFlag;
            IOSUniNClipboardSetChangedCallback(UniNClipboardHelperChangedCallback);
        }

        private void RemoveNativeCallback()
        {
            IOSUniNClipboardSetChangedCallback(null);
            onNativeClipboardChanged -= ActivateChangedFlag;
        }

        private void ActivateChangedFlag()
        {
            _clipboardDidChange = true;
        }

        [DllImport("__Internal")]
        private static extern string IOSUniNClipboardGetText();

        [DllImport("__Internal")]
        private static extern void IOSUniNClipboardSetText(string text);

        [DllImport("__Internal")]
        private static extern void IOSUniNClipboardSetChangedCallback(UniNClipboardHelperChangedDelegate changedCallback);

        private static event Action onNativeClipboardChanged;

        [MonoPInvokeCallback(typeof(UniNClipboardHelperChangedDelegate))]
        private static void UniNClipboardHelperChangedCallback()
        {
            if (onNativeClipboardChanged != null)
                onNativeClipboardChanged.Invoke();
        }

        private delegate void UniNClipboardHelperChangedDelegate();
    }
}