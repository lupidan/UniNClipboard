using System;
using System.Runtime.InteropServices;
using AOT;

namespace UniN.UniNClipboard
{
    public class IOSClipboard : IClipboard
    {
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
                if (onNativeClipboardChanged == null)
                    IOSUniNClipboardSetChangedCallback(UniNClipboardHelperChangedCallback);

                onNativeClipboardChanged += value;
            }
            remove
            {
                onNativeClipboardChanged -= value;

                if (onNativeClipboardChanged == null)
                    IOSUniNClipboardSetChangedCallback(null);
            }
        }

        [DllImport("__Internal")]
        private static extern string IOSUniNClipboardGetText();

        [DllImport("__Internal")]
        private static extern void IOSUniNClipboardSetText(string text);

        [DllImport("__Internal")]
        private static extern void IOSUniNClipboardSetChangedCallback(UniNClipboardHelperChangedDelegate changedCallback);

        private static event Action onNativeClipboardChanged;

        private delegate void UniNClipboardHelperChangedDelegate();

        [MonoPInvokeCallback(typeof(UniNClipboardHelperChangedDelegate))]
        private static void UniNClipboardHelperChangedCallback()
        {
            if (onNativeClipboardChanged != null)
                onNativeClipboardChanged.Invoke();
        }
    }
}